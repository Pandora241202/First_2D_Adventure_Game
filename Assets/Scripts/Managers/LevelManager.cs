﻿using UnityEngine;

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
        LoadLevelByNum(curLevelNum == levelConfigs.Length - 1 ? 0 : curLevelNum + 1);
    }

    public void LoadLevelByNum(int levelNum)
    {
        curLevelNum = levelNum;
        LevelConfig levelConfig = levelConfigs[curLevelNum];
        
        // Set up map
        AllManager.Instance().mapManager.SetUp(levelConfig.Map);

        // Spawn player
        AllManager.Instance().playerManager.Spawn(levelConfig.PlayerSpawnPos);

        // Set up trap
        foreach (TrapSpawnInfo info in levelConfig.TrapPosArr)
        {
            AllManager.Instance().trapManager.SpawnByType((TrapManager.TrapType)info.trapTypeInt, info.pos);
        }

        // Set up enemy

        // Set up Pickup
    }
}
