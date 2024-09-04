using UnityEngine;

[CreateAssetMenu(fileName = "AllPickupConfig", menuName = "Config/PickupConfig/AllPickup")]
public class AllPickupConfig : ScriptableObject
{
    [SerializeField] PickupConfig[] pickupConfigs;

    public PickupConfig[] PickupConfigs => pickupConfigs;
}