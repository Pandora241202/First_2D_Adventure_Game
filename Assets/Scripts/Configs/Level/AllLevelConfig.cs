using UnityEngine;

[System.Serializable]
public class CreepSpawnInfo
{
    [SerializeField] public int creepTypeInt;
    [SerializeField] public Vector3 pos;
}

[System.Serializable]
public class PickUpSpawnInfo
{
    [SerializeField] public int pickUpTypeInt;
    [SerializeField] public Vector3 pos;
}

[System.Serializable]
public class TrapSpawnInfo
{
    [SerializeField] public TrapManager.TrapType type;
    [SerializeField] public Vector3 rotate;
    [SerializeField] public Vector3 pos;
}

[System.Serializable]
public class LevelConfig
{
    [SerializeField] GameObject map;

    [SerializeField] Vector3 playerSpawnPos;

    [SerializeField] CreepSpawnInfo[] enemyPosArr;

    [SerializeField] TrapSpawnInfo[] trapPosArr;

    [SerializeField] PickUpSpawnInfo[] pickUpPosArr;

    public GameObject Map => map;

    public Vector3 PlayerSpawnPos => playerSpawnPos;

    public CreepSpawnInfo[] EnemyPosArr => enemyPosArr;

    public PickUpSpawnInfo[] PickUpPosArr => pickUpPosArr;

    public TrapSpawnInfo[] TrapPosArr => trapPosArr;
}

[CreateAssetMenu(fileName = "AllLevelConfig", menuName = "Config/LevelConfig/AllLevel")]
public class AllLevelConfig : ScriptableObject
{
    [SerializeField] LevelConfig[] levelConfigs;

    public LevelConfig[] LevelConfigs => levelConfigs;
}
