using UnityEngine;

public class EnemyConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    
    [SerializeField] int dmg;

    [SerializeField] float speed;

    [SerializeField] float attackCD;

    [SerializeField] int maxHealth;

    public GameObject EnemyPrefab => enemyPrefab;

    public int Dmg => dmg;

    public float Speed => speed;

    public float AttackCD => attackCD;

    public int MaxHealth => maxHealth;

    public void Patrol(Enemy enemy)
    {
        enemy.anim.SetTrigger("Patrol");
        if (Mathf.Abs(enemy.trans.position.x - enemy.centerPos.x) >= enemy.patrolRange / 2)
        {
            enemy.trans.localScale = new Vector3(-enemy.trans.localScale.x, enemy.trans.localScale.y, enemy.trans.localScale.z);
        }
        float direct = enemy.trans.localScale.x / Mathf.Abs(enemy.trans.localScale.x);
        enemy.trans.Translate(new Vector3(direct * Speed * Time.deltaTime, 0, 0));
    }

    public virtual void Active(Enemy enemy) { }
}