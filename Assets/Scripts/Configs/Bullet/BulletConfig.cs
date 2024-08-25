using UnityEngine;

public class BulletConfig : ScriptableObject
{
    [SerializeField] GameObject bulletPrefab;
    
    [SerializeField] float dmg;

    [SerializeField] float speed;

    [SerializeField] float timeToLive;

    [SerializeField] float timeToExplode;

    public GameObject BulletPrefab => bulletPrefab;

    public float Dmg => dmg;

    public float TimeToLive => timeToLive;

    public float Speed => speed;

    public float TimeToExplode => timeToExplode;

    public virtual void Move(Bullet bullet) { }

    public virtual void Set(Bullet bullet, Vector3 srcPos, int xScaleSign) 
    {
        bullet.trans.localScale = new Vector3(xScaleSign * Mathf.Abs(bullet.trans.localScale.x), 1, 0);
    }
}