using UnityEngine;

[CreateAssetMenu(fileName = "SawTrapConfig", menuName = "Config/TrapConfig/SawTrap")]
public class SawTrapConfig : TrapConfig
{
    [SerializeField] float speed;

    public override void Active(Trap trap)
    {
        float direct = trap.trans.localScale.x / Mathf.Abs(trap.trans.localScale.x);
        if (direct * (trap.trans.position.x - trap.centerPos.x) > 0 && Mathf.Abs(trap.trans.position.x - trap.centerPos.x) >= trap.range/2)
        {
            trap.trans.localScale = new Vector3(-trap.trans.localScale.x, trap.trans.localScale.y, trap.trans.localScale.z);
            direct = -direct;
        }
        trap.trans.Translate(new Vector3(speed * Time.deltaTime * direct, 0, 0));
    }
}