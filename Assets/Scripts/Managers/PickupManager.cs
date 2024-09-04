using System.Collections.Generic;
using UnityEngine;

public class Pickup
{
    public PickupConfig config;
    public GameObject obj;
    public Animator anim;

    public Pickup(PickupConfig config, GameObject pickupObj)
    {
        this.config = config;
        anim = pickupObj.GetComponent<Animator>();
        this.obj = pickupObj;
    }

    public void Active()
    {
        config.Active();
    }
}

public class PickupManager
{
    private Dictionary<int, Pickup> pickupDict = new Dictionary<int, Pickup>();
    private List<int> pickupIdToDestroyList = new List<int>();
    private PickupConfig[] pickupConfigs;

    public enum PickupType
    {
        StrawberryPickup,
        CherriesPickup
    }

    public PickupManager(AllPickupConfig allPickupConfig)
    {
        pickupConfigs = allPickupConfig.PickupConfigs;
    }

    public void SpawnByType(PickupType type, Vector3 pos)
    {
        PickupConfig config = pickupConfigs[(int)type];
        GameObject pickupObj = GameObject.Instantiate(config.PickupPrefab, pos, Quaternion.identity);
        pickupDict.Add(pickupObj.GetInstanceID(), new Pickup(config, pickupObj));
    }

    public void MyUpdate()
    {
        List<int> destroyPickupList = new List<int>();

        foreach (var pair in pickupDict)
        {
            AnimatorStateInfo stateInfo = pair.Value.anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Picked") && stateInfo.normalizedTime >= 1f)
            {
                destroyPickupList.Add(pair.Key);
                GameObject.Destroy(pair.Value.obj);
            }
        }

        foreach (int id in destroyPickupList)
        {
            pickupDict.Remove(id);
        }
    }

    public void MyLateUpdate()
    {
        foreach (int id in pickupIdToDestroyList)
        {
            pickupDict[id].anim.SetTrigger("Picked");
            pickupDict[id].obj.GetComponent<Collider2D>().enabled = false;
        }
        pickupIdToDestroyList.Clear();
    }

    private void DestroyPickupById(int id)
    {
        pickupIdToDestroyList.Add(id);
    }

    public void ProcessPickedByPlayer(int pickupId)
    {
        pickupDict[pickupId].Active();
        DestroyPickupById(pickupId);
    }
}
