﻿using UnityEngine;

public class PlayerManager
{
    private PlayerConfig config;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCol;
    private Transform trans;

    private float jumpKeyHoldDuration;
    private float jumpWallDuration;
    private float timeFromLastAttack;
    private bool wallJumping;

    private float speedBuft;
    private int curHealth;
    private int maxHealth;
    private float invulTime;
    private bool isDead;

    public int CurHealth
    {
        get
        {
            return curHealth;
        }
        set
        {
            if (curHealth > value)
            {
                if (IsInvul())
                {
                    return;
                }

                anim.SetTrigger("Hurt");
                SetInvul();
            }

            curHealth = value < 0 ? 0 : value > maxHealth ? maxHealth : value;

            if (curHealth == 0)
            {
                Die();
            }

            UIManager.Instance().healthBar.SetCurHealth(curHealth);
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value > 9 ? 9 : value;
            UIManager.Instance().healthBar.SetMaxHealth(maxHealth);
        }
    }

    public PlayerManager(PlayerConfig config)
    {
        this.config = config;
        GameObject playerObj = GameObject.Instantiate(config.PlayerPrefab, new Vector3(0, 7, 0), Quaternion.identity);
        body = playerObj.GetComponent<Rigidbody2D>();
        anim = playerObj.GetComponent<Animator>();
        boxCol = playerObj.GetComponent<BoxCollider2D>();
        trans = playerObj.transform;
        Spawn(new Vector3(0, 7, 0));
    }

    public void Spawn(Vector3 pos)
    {
        trans.gameObject.SetActive(true);
        boxCol.enabled = true;
        body.gravityScale = 2;
        body.simulated = true;
        isDead = false;

        trans.position = pos;
        jumpKeyHoldDuration = config.MaxJumpKeyHoldDuration;
        jumpWallDuration = 0;
        timeFromLastAttack = 0;
        wallJumping = false;
        invulTime = -1;
        speedBuft = 0;

        curHealth = config.InitHealth;
        maxHealth = config.InitMaxHealth;
        UIManager.Instance().healthBar.SetCurHealth(curHealth);
        UIManager.Instance().healthBar.SetMaxHealth(maxHealth);
    }

    public void MyUpdate()
    {
        if (isDead)
        {
            return;
        }

        InvulApply();
        Move();
        Attack();
    }

    private void Move()
    {
        bool isOnWall = IsOnWall();
        bool isGrounded = IsGrounded();

        float xAxis = Input.GetAxis("Horizontal");

        // Wall jumping
        if (wallJumping)
        {
            jumpWallDuration += Time.deltaTime;
            if (isOnWall || xAxis * trans.localScale.x <= 0 || jumpWallDuration >= config.MaxJumpWallDuration)
            {
                wallJumping = false;
            }
        }

        // Walk
        if (!wallJumping && (!isOnWall || xAxis * trans.localScale.x < 0))
        {
            body.velocity = new Vector2(xAxis * GetSpeed(), body.velocity.y);
        }
        else
        {
            xAxis = 0;
        }

        // Flip
        if (xAxis > 0.01f)
        {
            trans.localScale = Vector3.one;
        }
        if (xAxis < -0.01f)
        {
            trans.localScale = new Vector3(-1, 1, 1);
        }

        // Jump
        if (Input.GetButton("Jump") && !IsHeadCollide())
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (isOnWall)
            {
                JumpOnWall();
            }
            else if (jumpKeyHoldDuration < config.MaxJumpKeyHoldDuration)
            {
                JumpHigher();
            }
        }
        else
        {
            jumpKeyHoldDuration = config.MaxJumpKeyHoldDuration;
        }

        // Slide on wall
        if (isOnWall)
        {
            body.gravityScale = 0.5f;
        }
        else
        {
            body.gravityScale = 2;
        }

        // Anim
        anim.SetBool("Walk", xAxis > 0.01f || xAxis < -0.01f);
        anim.SetBool("Jump", !isGrounded && !isOnWall);
    }

    private void Jump()
    {
        jumpKeyHoldDuration = 0;
        body.velocity = new Vector2(body.velocity.x, config.JumpHigh);
    }

    private void JumpHigher()
    {
        jumpKeyHoldDuration += Time.deltaTime;
        body.velocity = new Vector2(body.velocity.x, config.JumpHigh);
    }

    private void JumpOnWall()
    {
        jumpKeyHoldDuration = 0;
        wallJumping = true;
        jumpWallDuration = 0;
        body.velocity = new Vector2(-trans.localScale.x * config.Speed, config.JumpHigh);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, Vector3.down, 0.1f, AllManager.Instance().GroundLayerMask);
        if (hit.collider != null)
        {
            AllManager.Instance().mapManager.ApplyEffectForPlayerOnGround(hit.point, this);
            return true;
        }
        return false;
    }

    private bool IsOnWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, new Vector3(trans.localScale.x / Mathf.Abs(trans.localScale.x), 0, 0), 0.1f, AllManager.Instance().GroundLayerMask);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private bool IsHeadCollide()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, Vector3.up, 0.1f, AllManager.Instance().GroundLayerMask);
        if (hit.collider != null)
        {      
            return true;
        }
        return false;
    }

    private void Attack()
    {
        timeFromLastAttack += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (timeFromLastAttack >= config.AttackCD)
            {
                anim.SetTrigger("Attack");
                timeFromLastAttack = 0;
                AllManager.Instance().bulletManager.ActivateBulletByType(BulletManager.BulletType.PlayerBullet, trans.position, trans.localScale.x < 0 ? -1 : 1);
            }
        }
    }

    public void SetSpeedBuft(float buft)
    {
        speedBuft = buft;
    }

    public float GetSpeed()
    {
        return config.Speed * (1 + speedBuft) < 0 ? 0 : config.Speed * (1 + speedBuft);
    }

    public Vector3 GetPos()
    {
        return trans.position;
    }

    public void ProcessCollideTrap(int trapId)
    {
        CurHealth -= AllManager.Instance().trapManager.GetTrapDmgById(trapId);
    }

    public bool IsInvul()
    {
        
        return invulTime >= 0;
    }

    public void SetInvul()
    {
        invulTime = 0;
        Color color = trans.gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0.5f;
        trans.gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    private void InvulApply()
    {
        if (IsInvul()) 
        {
            invulTime += Time.deltaTime;
            if (invulTime > config.MaxInvulTime)
            {
                invulTime = -1;
                Color color = trans.gameObject.GetComponent<SpriteRenderer>().color;
                color.a = 1;
                trans.gameObject.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        boxCol.enabled = false;
        body.gravityScale = 0;
        body.simulated = false;
        isDead = true;
    }
}
