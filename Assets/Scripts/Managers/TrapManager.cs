using System.Collections.Generic;
using UnityEngine;

public class Trap
{
    public TrapConfig config;
    public Transform trans;
    public TrapManager.TrapType type;
    public Animator anim;
    public Vector3 centerPos;

    public Trap(TrapManager.TrapType type, TrapConfig config, GameObject trapObj)
    { 
        this.config = config;
        anim = trapObj.GetComponent<Animator>();
        this.trans = trapObj.transform;
        this.type = type;
        this.centerPos = trapObj.transform.position;
    }

    public void Active()
    {
        config.Active(this);
    }
}

public class TrapManager
{
    private Dictionary<int, Trap> trapDict = new Dictionary<int, Trap>();
    private TrapConfig[] trapConfigs;

    public enum TrapType
    {
        SawTrap,
    }

    public TrapManager(AllTrapConfig allTrapConfig)
    {
        trapConfigs = allTrapConfig.TrapConfigs;
    }

    public void SpawnByType(TrapType type, Vector3 pos)
    {
        TrapConfig config = trapConfigs[(int)type];
        GameObject trapObj = GameObject.Instantiate(config.TrapPrefab, pos, Quaternion.identity);
        trapDict.Add(trapObj.GetInstanceID(), new Trap(type, config, trapObj));
    }

    public void MyUpdate()
    {
        foreach (Trap trap in trapDict.Values)
        {
            trap.Active();
        }
    }

    public int GetTrapDmgById(int id)
    {
        return trapDict[id].config.Dmg;
    }
}
