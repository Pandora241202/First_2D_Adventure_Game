using UnityEngine;

[CreateAssetMenu(fileName = "ShooterEnemyBulletConfig", menuName = "Config/BulletConfig/ShooterEnemyBullet")]
public class ShooterEnemyBulletConfig : BulletMoveStraightConfig
{
    public override void Set(Bullet bullet, Vector3 srcPos, int xScaleSign)
    {
        base.Set(bullet, srcPos, xScaleSign);
        bullet.trans.position = new Vector3(srcPos.x, srcPos.y + 0.15f, 1);
    }

    public override void Explode(Bullet bullet)
    {
        AllManager.Instance().bulletManager.DeactivateBulletById(bullet.trans.gameObject.GetInstanceID());
    }
}