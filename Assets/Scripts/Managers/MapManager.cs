using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager
{
    private Dictionary<TileBase, TileConfig> tileDict = new Dictionary<TileBase, TileConfig>();
    private Tilemap curMap;
    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float botEdge;

    public float LeftEdge => leftEdge;
    public float RightEdge => rightEdge;
    public float TopEdge => topEdge;
    public float BotEdge => botEdge;

    public MapManager(AllTileConfig allTileConfig)
    {
        TileConfig[] tileConfigs = allTileConfig.TileConfigs;

        foreach (TileConfig tileConfig in tileConfigs)
        {
            foreach (TileBase tile in tileConfig.Tiles) 
            {
                tileDict.Add(tile, tileConfig);
            }
        }

        curMap = null;
    }

    public void SetUp(GameObject mapObj)
    {
        if (curMap != null) 
        {
            GameObject.Destroy(curMap.gameObject);
        }

        GameObject curMapGameObj = GameObject.Instantiate(mapObj, AllManager.Instance().GridTrans);
        curMapGameObj.transform.localPosition = Vector3.zero;
        curMap = curMapGameObj.GetComponent<Tilemap>();

        BoundsInt cellBounds = curMap.cellBounds;

        Vector3Int minCellPosition = cellBounds.min;
        Vector3Int maxCellPosition = cellBounds.max;

        Vector3 minWorldPosition = curMap.CellToWorld(minCellPosition);
        Vector3 maxWorldPosition = curMap.CellToWorld(maxCellPosition);

        leftEdge = minWorldPosition.x;
        rightEdge = maxWorldPosition.x;
        botEdge = minWorldPosition.y;
        topEdge = maxWorldPosition.y;
    }

    public void ApplyEffectForPlayerOnGround(Vector3 collidePoint, PlayerManager playerManager)
    {
        Vector3Int tilePosition = curMap.WorldToCell(collidePoint);
        TileBase tile = curMap.GetTile(tilePosition);
        if (tile != null)
        {
            TileConfig tileConfig;
            if (tileDict.TryGetValue(tile, out tileConfig))
            {
                tileConfig.ApplyEffect(playerManager);
            }
        }
    }
}
