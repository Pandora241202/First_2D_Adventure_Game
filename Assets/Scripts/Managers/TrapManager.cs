using System.Collections.Generic;
using UnityEngine;

public class Trap
{
    public TrapConfig config;
    public Transform trans;
    public TrapManager.TrapType type;
    public Animator anim;
    public Vector3 centerPos;
    public float timeCount;
    public float range;

    public Trap(TrapManager.TrapType type, TrapConfig config, GameObject trapObj, float range)
    { 
        this.config = config;
        anim = trapObj.GetComponent<Animator>();
        this.trans = trapObj.transform;
        this.type = type;
        this.centerPos = trapObj.transform.position;
        this.timeCount = 0;
        this.range = range;
        config.Set(this);
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
        SpikeTrap,
        FireTrap
    }

    public TrapManager(AllTrapConfig allTrapConfig)
    {
        trapConfigs = allTrapConfig.TrapConfigs;
    }

    public void SpawnByType(TrapType type, Vector3 pos, Vector3 rotate, float range)
    {
        TrapConfig config = trapConfigs[(int)type];
        GameObject trapObj = GameObject.Instantiate(config.TrapPrefab, pos, Quaternion.Euler(rotate));
        trapDict.Add(trapObj.GetInstanceID(), new Trap(type, config, trapObj, range));
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
