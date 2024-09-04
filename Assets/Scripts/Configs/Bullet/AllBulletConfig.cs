using UnityEngine;

[CreateAssetMenu(fileName = "AllBulletConfig", menuName = "Config/BulletConfig/AllBullet")]
public class AllBulletConfig : ScriptableObject
{
    [SerializeField] BulletConfig[] bulletConfigs;

    public BulletConfig[] BulletConfigs => bulletConfigs;
}