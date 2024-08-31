using UnityEngine;

[CreateAssetMenu(fileName = "RangeEnemyBulletConfig", menuName = "Config/BulletConfig/RangeEnemyBullet")]
public class RangeEnemyBulletConfig : BulletMoveStraightConfig
{
    public override void Set(Bullet bullet, Vector3 srcPos, int xScaleSign)
    {
        base.Set(bullet, srcPos, xScaleSign);
        bullet.trans.position = new Vector3(srcPos.x, srcPos.y - 0.1f, srcPos.z);
    }

    public override void Explode(Bullet bullet)
    {
        AllManager.Instance().bulletManager.DeactivateBulletById(bullet.trans.gameObject.GetInstanceID());
    }
}