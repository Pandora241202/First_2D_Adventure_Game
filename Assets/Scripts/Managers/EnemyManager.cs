using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public EnemyConfig config;
    public Transform trans;
    public EnemyManager.EnemyType type;
    public Animator anim;
    public Vector3 centerPos;
    public float patrolRange;
    public float timeFromLastAttack;
    
    private int health;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (health > value)
            {
                anim.SetTrigger("Hurt");
            }
            health = value < 0 ? 0 : value;
            if (health == 0)
            {
                //SetDie();
            }
        }
    }

    public Enemy(EnemyManager.EnemyType type, EnemyConfig config, GameObject enemyObj, float patrolRange)
    {
        this.config = config;
        anim = enemyObj.GetComponent<Animator>();
        this.trans = enemyObj.transform;
        this.type = type;
        this.health = config.MaxHealth;
        this.centerPos = enemyObj.transform.position;
        this.timeFromLastAttack = config.AttackCD;
        this.patrolRange = patrolRange;
    }

    public void Active()
    {
        config.Active(this);
    }
}

public class EnemyManager
{
    private Dictionary<int, Enemy> enemyDict = new Dictionary<int, Enemy>();
    private List<int> enemyIdToDestroyList = new List<int>();
    private EnemyConfig[] enemyConfigs;

    public enum EnemyType
    {
        MeleeEnemy,
        RangeEnemy
    }

    public EnemyManager(AllEnemyConfig allEnemyConfig)
    {
        enemyConfigs = allEnemyConfig.EnemyConfigs;
    }

    public void SpawnByType(EnemyType type, Vector3 pos, float patrolRange)
    {
        EnemyConfig config = enemyConfigs[(int)type];
        GameObject enemyObj = GameObject.Instantiate(config.EnemyPrefab, pos, Quaternion.identity);
        enemyDict.Add(enemyObj.GetInstanceID(), new Enemy(type, config, enemyObj, patrolRange));
    }

    public int GetEnemyDmgById(int id)
    {
        return enemyDict[id].config.Dmg;
    }

    public void MyUpdate()
    {
        foreach (Enemy enemy in enemyDict.Values)
        {
            enemy.Active();
        }
    }

    public void MyLateUpdate()
    {
        foreach (int id in enemyIdToDestroyList)
        {
            enemyDict.Remove(id);
        }
        enemyIdToDestroyList.Clear();
    }

    public void DestroyEnemyById(int id)
    {
        enemyIdToDestroyList.Add(id);
    }

    public void ProcessCollidePlayerBullet(int bulletId)
    {
        enemyDict[bulletId].Health -= AllManager.Instance().bulletManager.GetBulletDmgById(bulletId);
    }
}
