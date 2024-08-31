using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    public BulletConfig config;
    public float timeFromActivate;
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
        trans.gameObject.GetComponent<Collider2D>().enabled = true;
        timeFromActivate = 0;
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
        config.Explode(this);
    }
}

public class BulletManager
{
    private Dictionary<int, Bullet> bulletActiveDict = new Dictionary<int, Bullet>();
    private List<Bullet>[] bulletNotActiveListByType = new List<Bullet>[Enum.GetValues(typeof(BulletType)).Length];
    private List<int> bulletIdsToDeactivate = new List<int>();
    private BulletConfig[] bulletConfigs;

    public enum BulletType
    {
        PlayerBullet,
        RangeEnemyBullet,
        ShooterEnemyBullet
    }

    public BulletManager(AllBulletConfig allBulletConfig)
    {
        bulletConfigs = allBulletConfig.BulletConfigs;

        for (int i = 0; i < bulletNotActiveListByType.Length; i++)
        {
            bulletNotActiveListByType[i] = new List<Bullet>();
        }

        foreach (BulletType type in Enum.GetValues(typeof(BulletType)))
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnByType(type);
            }
        }
    }

    private void SpawnByType(BulletType type)
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

    public int GetBulletDmgById(int id)
    {
        return bulletActiveDict[id].config.Dmg;
    }
}
