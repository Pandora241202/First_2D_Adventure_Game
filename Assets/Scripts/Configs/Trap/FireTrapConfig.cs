using UnityEditor.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "FireTrapConfig", menuName = "Config/TrapConfig/FireTrap")]
public class FireTrapConfig : TrapConfig
{
    [SerializeField] float fireRange;
    [SerializeField] float fireCD;
    [SerializeField] float fireDuration;
    [SerializeField] LayerMask playerLayerMask;

    public override void Active(Trap trap)
    {
        trap.timeCount += Time.deltaTime;
        if (trap.timeCount > fireCD && trap.timeCount <= fireDuration + fireCD)
        {
            trap.anim.SetBool("On", true);
            BoxCollider2D boxCol = trap.trans.gameObject.GetComponent<BoxCollider2D>();
            RaycastHit2D hit = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0, trap.trans.up, 1, playerLayerMask);
            if (hit.collider != null)
            {
                AllManager.Instance().playerManager.CurHealth -= Dmg;
            }
        } 
        else if (trap.timeCount > fireDuration + fireCD)
        {
            trap.timeCount = 0;
            trap.anim.SetBool("On", false);
        }
    }
}