using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    private Dictionary<TileBase, TileConfig> tileDict = new Dictionary<TileBase, TileConfig>();
    private Tilemap curMap;

    public MapManager(AllTileConfig allTileConfig)
    {
        TileConfig[] tileConfigs = allTileConfig.TileConfigs;

        foreach (TileConfig tileConfig in tileConfigs)
        {
            foreach (TileBase tile in tileConfig.Tiles) 
            {
                tileDict.Add(tile, tileConfig);
            }
        }

        curMap = null;
    }

    public void SetUp(GameObject mapObj)
    {
        if (curMap != null) 
        {
            GameObject.Destroy(curMap.gameObject);
        }

        GameObject curMapGameObj = GameObject.Instantiate(mapObj, AllManager.Instance().GridTrans);
        curMapGameObj.transform.localPosition = Vector3.zero;
        curMap = curMapGameObj.GetComponent<Tilemap>();
    }

    public void MyUpdate()
    {
        
    }

    /*private void Move()
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
            body.velocity = new Vector2(xAxis * config.Speed, body.velocity.y);
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
        if (Input.GetButton("Jump"))
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
    }*/
}
