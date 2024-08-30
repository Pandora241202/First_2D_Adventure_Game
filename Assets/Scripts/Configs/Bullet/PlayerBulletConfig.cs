using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBulletConfig", menuName = "Config/BulletConfig/PlayerBullet")]
public class PlayerBulletConfig : BulletMoveStraightConfig
{
    public override void Set(Bullet bullet, Vector3 srcPos, int xScaleSign)
    {
        base.Set(bullet, srcPos, xScaleSign);
        bullet.trans.position = new Vector3(srcPos.x + xScaleSign * 0.5f, srcPos.y - 0.25f, srcPos.z);
    }
}