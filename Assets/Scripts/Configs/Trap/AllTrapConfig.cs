using UnityEngine;

[CreateAssetMenu(fileName = "AllTrapConfig", menuName = "Config/TrapConfig/AllTrap")]
public class AllTrapConfig : ScriptableObject
{
    [SerializeField] TrapConfig[] trapConfigs;

    public TrapConfig[] TrapConfigs => trapConfigs;
}