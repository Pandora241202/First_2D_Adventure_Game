using UnityEngine;

[CreateAssetMenu(fileName = "CherriesPickupConfig", menuName = "Config/PickupConfig/CherriesPickup")]
public class CherriesPickupConfig : PickupConfig
{
    public override void Active()
    {
        AllManager.Instance().playerManager.MaxHealth++;
        AllManager.Instance().playerManager.CurHealth++;
    }
}