using UnityEngine;

[CreateAssetMenu(fileName = "AllTileConfig", menuName = "Config/TileConfig/AllTileConfig")]
public class AllTileConfig : ScriptableObject
{
    [SerializeField] TileConfig[] tileConfigs;

    public TileConfig[] TileConfigs => tileConfigs;
}
