using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void FireBullet()
    {
        AllManager.Instance().bulletManager.ActivateBulletByType(BulletManager.BulletType.RangeEnemyBullet, transform.position, transform.localScale.x < 0 ? -1 : 1);
    }
}
