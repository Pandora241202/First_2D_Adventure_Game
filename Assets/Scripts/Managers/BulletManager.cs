using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bullet
{
    public BulletConfig config;
    public float timeFromActivate;
    public float timeFromStartExplode;
    public Transform trans;
    public BulletManager.BulletType type;
    public Animator anim;

    public Bullet(BulletManager.BulletType type, BulletConfig config)
    { 
        this.config = config;
        timeFromActivate = 0;
        GameObject bulletObj = GameObject.Instantiate(config.BulletPrefab, Vector3.zero, Quaternion.identity);
        anim = bulletObj.GetComponent<Animator>();
        bulletObj.SetActive(false);
        this.trans = bulletObj.transform;
        this.type = type;
    }

    public void Set(Vector3 srcPos, int xScaleSign)
    {
        trans.gameObject.SetActive(true);
        timeFromActivate = 0;
        timeFromStartExplode = -1;
        config.Set(this, srcPos, xScaleSign);
    }

    public void UnSet()
    {
        trans.gameObject.SetActive(false);
    }

    public void Move()
    {
        config.Move(this);
    }

    public void Explode()
    {
        if (timeFromStartExplode == -1)
        {
            anim.SetTrigger("Explode");
            timeFromStartExplode = 0;
            return;
        }

        timeFromStartExplode += Time.deltaTime;
        if (timeFromStartExplode > config.TimeToExplode)
        {
            AllManager.Instance().bulletManager.DeactivateBulletById(trans.gameObject.GetInstanceID());
        }
    }
}

public class BulletManager
{
    private Dictionary<int, Bullet> bulletActiveDict = new Dictionary<int, Bullet>();
    private List<Bullet>[] bulletNotActiveListByType = new List<Bullet>[1];
    private List<int> bulletIdsToDeactivate = new List<int>();
    private BulletConfig[] bulletConfigs;

    public enum BulletType
    {
        PlayerBullet,
    }

    public BulletManager(AllBulletConfig allBulletConfigs)
    {
        bulletConfigs = allBulletConfigs.BulletConfigs;

        for (int i = 0; i < bulletNotActiveListByType.Length; i++)
        {
            bulletNotActiveListByType[i] = new List<Bullet>();
        }

        for (BulletType type = BulletType.PlayerBullet; type <= BulletType.PlayerBullet; type++)
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnByType(type);
            }
        }
    }

    public void SpawnByType(BulletType type)
    {
        bulletNotActiveListByType[(int)type].Add(new Bullet(type, bulletConfigs[(int)type]));
    }

    public void ActivateBulletByType(BulletType type, Vector3 srcPos, int xScaleSign)
    {
        if (bulletNotActiveListByType[(int)type].Count == 0)
        {
            SpawnByType(type);
        }

        Bullet bulletToActivate = bulletNotActiveListByType[(int)type][0];
        bulletNotActiveListByType[(int)type].RemoveAt(0);
        bulletToActivate.Set(srcPos, xScaleSign);
        bulletActiveDict.Add(bulletToActivate.trans.gameObject.GetInstanceID(), bulletToActivate);
    }

    public void MyUpdate()
    {
        foreach (Bullet bullet in bulletActiveDict.Values)
        {
            if (bullet.timeFromStartExplode >= 0)
            {
                bullet.Explode();
                continue;
            }

            bullet.Move();
            bullet.timeFromActivate += Time.deltaTime;
            if (bullet.timeFromActivate >= bullet.config.TimeToLive)
            {
                bullet.Explode();
            } 
        }
    }

    public void MyLateUpdate()
    {
        foreach (int id in bulletIdsToDeactivate)
        {
            Bullet bulletToDeactivate = bulletActiveDict[id];
            bulletToDeactivate.UnSet();
            bulletNotActiveListByType[(int) bulletToDeactivate.type].Add(bulletToDeactivate);
            bulletActiveDict.Remove(id);
        }
        bulletIdsToDeactivate.Clear();
    }

    public void DeactivateBulletById(int id)
    {
        bulletIdsToDeactivate.Add(id);
    }
}
