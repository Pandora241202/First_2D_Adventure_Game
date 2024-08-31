using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void FireBullet(BulletManager.BulletType type)
    {
        AllManager.Instance().bulletManager.ActivateBulletByType(type, transform.position, transform.localScale.x < 0 ? -1 : 1);
    }
}
