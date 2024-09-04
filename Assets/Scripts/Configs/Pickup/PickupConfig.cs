using UnityEngine;

public class PickupConfig : ScriptableObject
{
    [SerializeField] GameObject pickupPrefab;

    public GameObject PickupPrefab => pickupPrefab;

    public virtual void Active() { }
}