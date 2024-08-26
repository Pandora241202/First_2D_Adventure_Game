using UnityEngine;
using UnityEngine.Tilemaps;

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

    public PlayerManager(PlayerConfig config)
    {
        this.config = config;
        Spawn(new Vector3(0, 7, 0));
        jumpKeyHoldDuration = config.MaxJumpKeyHoldDuration;
        jumpWallDuration = 0;
        timeFromLastAttack = 0;
        wallJumping = false;
    }

    public void Spawn(Vector3 pos)
    {
        if (body != null)
        {
            GameObject.Destroy(body.gameObject);  
        }
        
        GameObject playerObj = GameObject.Instantiate(config.PlayerPrefab, pos, Quaternion.identity);
        body = playerObj.GetComponent<Rigidbody2D>();
        anim = playerObj.GetComponent<Animator>();
        boxCol = playerObj.GetComponent<BoxCollider2D>();
        trans = playerObj.transform;

        speedBuft = 0;
    }

    public void MyUpdate()
    {
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
        else
        {
            anim.ResetTrigger("Attack");
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
}
