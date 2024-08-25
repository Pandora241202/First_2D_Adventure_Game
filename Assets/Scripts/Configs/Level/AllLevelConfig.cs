using UnityEngine;

[System.Serializable]
public class CreepSpawnInfo
{
    [SerializeField] int creepTypeInt;
    [SerializeField] Vector3 pos;
}

[System.Serializable]
public class PickUpSpawnInfo
{
    [SerializeField] int pickUpTypeInt;
    [SerializeField] Vector3 pos;
}

[System.Serializable]
public class LevelConfig
{
    [SerializeField] GameObject map;

    [SerializeField] Vector3 playerSpawnPos;

    [SerializeField] CreepSpawnInfo[] enemyPosArr;

    [SerializeField] PickUpSpawnInfo[] pickUpPosArr;

    public GameObject Map => map;

    public Vector3 PlayerSpawnPos => playerSpawnPos;

    public CreepSpawnInfo[] EnemyPosArr => enemyPosArr;

    public PickUpSpawnInfo[] PickUpPosArr => pickUpPosArr;
}

[CreateAssetMenu(fileName = "AllLevelConfig", menuName = "Config/LevelConfig/AllLevel")]
public class AllLevelConfig : ScriptableObject
{
    [SerializeField] LevelConfig[] levelConfigs;

    public LevelConfig[] LevelConfigs => levelConfigs;
}
