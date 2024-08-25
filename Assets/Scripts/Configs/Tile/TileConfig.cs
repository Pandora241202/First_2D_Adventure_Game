using UnityEngine;
using UnityEngine.Tilemaps;

public class TileConfig: ScriptableObject
{
    [SerializeField] TileBase[] tiles;

    [SerializeField] float speedDebuft;

    public TileBase[] Tiles => tiles;

    public float SpeedDebuft => speedDebuft;

    public virtual void ApplyEffect(PlayerManager playerManager) 
    {
        playerManager.AddSpeedBuft(SpeedDebuft);
    }
}