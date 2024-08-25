using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class levelConfig : ScriptableObject
{
    [SerializeField] Tilemap map;

    [SerializeField] Vector3 playerSpawnPos;

    [SerializeField] Tuple<int, Vector3>[] enemyPosArr;

    [SerializeField] Tuple<int, Vector3>[] pickUpPosArr;

    public Tilemap Map => map;

    public Vector3 PlayerSpawnPos => playerSpawnPos;

    public Tuple<int, Vector3>[] EnemyPosArr => enemyPosArr;

    public Tuple<int, Vector3>[] PickUpPosArr => pickUpPosArr;
}