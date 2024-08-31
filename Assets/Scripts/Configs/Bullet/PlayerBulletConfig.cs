using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletConfig", menuName = "Config/BulletConfig/PlayerBullet")]
public class PlayerBulletConfig : BulletMoveStraightConfig
{
    public override void Set(Bullet bullet, Vector3 srcPos, int xScaleSign)
    {
        base.Set(bullet, srcPos, xScaleSign);
        bullet.trans.position = new Vector3(srcPos.x, srcPos.y - 0.3f, 1);
    }

    public override void Explode(Bullet bullet)
    {
        Animator anim = bullet.trans.gameObject.GetComponent<Animator>();
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Explode"))
        {
            if (stateInfo.normalizedTime >= 1.0f)
            {
                AllManager.Instance().bulletManager.DeactivateBulletById(bullet.trans.gameObject.GetInstanceID());
            }
        }
        else
        {
            anim.SetTrigger("Explode");
            bullet.trans.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public override void Move(Bullet bullet)
    {
        Animator anim = bullet.trans.gameObject.GetComponent<Animator>();
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Explode"))
        {
            return;
        }

        base.Move(bullet);
    }
}