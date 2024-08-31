using UnityEngine;

[CreateAssetMenu(fileName = "RangeEnemyConfig", menuName = "Config/EnemyConfig/RangeEnemy")]
public class RangeEnemyConfig : EnemyConfig
{
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] float attackRange;

    public override void Active(Enemy enemy)
    {
        if (enemy.timeFromLastAttack != -1)
        {
            enemy.timeFromLastAttack += Time.deltaTime;
        }
        if (enemy.timeFromLastAttack > AttackCD)
        {
            enemy.timeFromLastAttack = -1;
        }

        BoxCollider2D boxCol = enemy.trans.gameObject.GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, new Vector3(enemy.trans.localScale.x / Mathf.Abs(enemy.trans.localScale.x), 0, 0), attackRange, playerLayerMask);
        bool playerInRange = hit.collider != null;

        if (!playerInRange)
        {
            AnimatorStateInfo stateInfo = enemy.anim.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Attack"))
            {
                Patrol(enemy);
            }
            return;
        }

        if (enemy.timeFromLastAttack == -1)
        {
            enemy.anim.SetTrigger("Attack");
            enemy.anim.ResetTrigger("Patrol");
            enemy.timeFromLastAttack = 0;
        }
    }
}