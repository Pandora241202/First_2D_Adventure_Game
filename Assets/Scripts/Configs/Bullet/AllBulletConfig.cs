using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemyConfig", menuName = "Config/EnemyConfig/AllEnemy")]
public class AllEnemyConfig : ScriptableObject
{
    [SerializeField] EnemyConfig[] enemyConfigs;

    public EnemyConfig[] EnemyConfigs => enemyConfigs;
}