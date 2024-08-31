using UnityEngine;

[CreateAssetMenu(fileName = "ShooterEnemyConfig", menuName = "Config/EnemyConfig/ShooterEnemy")]
public class ShooterEnemyConfig : EnemyConfig
{
    public override void Active(Enemy enemy)
    {
        enemy.timeFromLastAttack += Time.deltaTime;

        if (enemy.timeFromLastAttack > AttackCD)
        {
            enemy.anim.SetTrigger("Attack");
            enemy.timeFromLastAttack = 0;
        }
    }
}