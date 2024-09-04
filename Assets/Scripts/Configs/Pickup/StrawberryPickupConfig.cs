using UnityEngine;

[CreateAssetMenu(fileName = "StrawberryPickupConfig", menuName = "Config/PickupConfig/StrawberryPickup")]
public class StrawberryPickupConfig : PickupConfig
{
    public override void Active()
    {
        AllManager.Instance().playerManager.CurHealth++;      
    }
}