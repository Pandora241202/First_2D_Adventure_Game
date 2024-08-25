using UnityEngine;

public class BulletMoveStraightConfig : BulletConfig
{
    public override void Move(Bullet bullet)
    {
        bullet.trans.Translate(new Vector3(Mathf.Sign(bullet.trans.localScale.x) * Speed * Time.deltaTime, 0, 0));
    }
}