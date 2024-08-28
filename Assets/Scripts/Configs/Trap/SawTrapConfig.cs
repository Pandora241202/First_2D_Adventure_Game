using UnityEngine;

[CreateAssetMenu(fileName = "SawTrapConfig", menuName = "Config/TrapConfig/SawTrap")]
public class SawTrapConfig : TrapConfig
{
    [SerializeField] float speed;
    [SerializeField] float range;

    public override void Active(Trap trap)
    {
        if ((trap.trans.position - trap.centerPos).magnitude >= range/2)
        {
            trap.trans.localScale = new Vector3(-trap.trans.localScale.x, trap.trans.localScale.y, trap.trans.localScale.z);
        }
        trap.trans.Translate(new Vector3(speed * Time.deltaTime * (trap.trans.localScale.x / Mathf.Abs(trap.trans.localScale.x)), 0, 0));
    }
}