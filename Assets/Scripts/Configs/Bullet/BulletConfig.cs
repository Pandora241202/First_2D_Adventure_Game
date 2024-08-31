using UnityEngine;

public class BulletConfig : ScriptableObject
{
    [SerializeField] GameObject bulletPrefab;
    
    [SerializeField] int dmg;

    [SerializeField] float speed;

    [SerializeField] float timeToLive;

    public GameObject BulletPrefab => bulletPrefab;

    public int Dmg => dmg;

    public float TimeToLive => timeToLive;

    public float Speed => speed;

    public virtual void Move(Bullet bullet) { }

    public virtual void Set(Bullet bullet, Vector3 srcPos, int xScaleSign) 
    {
        bullet.trans.localScale = new Vector3(xScaleSign * Mathf.Abs(bullet.trans.localScale.x), 1, 0);
    }

    public virtual void Explode(Bullet bullet) { }
}