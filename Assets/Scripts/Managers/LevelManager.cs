using UnityEngine;

public class LevelManager
{
    private int curLevelNum;
    private LevelConfig[] levelConfigs;

    public LevelManager(AllLevelConfig allLevelConfig)
    {
        levelConfigs = allLevelConfig.LevelConfigs;
        curLevelNum = 0;
    }

    public void LoadNextLevel()
    {
        curLevelNum++;
        if (curLevelNum == levelConfigs.Length)
        {
            curLevelNum = 0;
        }

        LevelConfig levelConfig = levelConfigs[curLevelNum];

        // Set up map
        AllManager.Instance().mapManager.SetUp(levelConfig.Map);

        // Spawn player
        AllManager.Instance().playerManager.Spawn(levelConfig.PlayerSpawnPos);

        // Set up enemy

        // Set up Pickup
    }

    public void LoadLevelByNum(int levelNum)
    {
        curLevelNum = levelNum;
        LevelConfig levelConfig = levelConfigs[curLevelNum];
        AllManager.Instance().mapManager.SetUp(levelConfig.Map);
        AllManager.Instance().playerManager.Spawn(levelConfig.PlayerSpawnPos);
    }
}
