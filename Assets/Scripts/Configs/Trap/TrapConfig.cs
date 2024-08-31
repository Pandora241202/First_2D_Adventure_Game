using UnityEngine;

public class TrapConfig : ScriptableObject
{
    [SerializeField] GameObject trapPrefab;

    [SerializeField] int dmg;

    public GameObject TrapPrefab => trapPrefab;

    public int Dmg => dmg;

    public virtual void Active(Trap trap) { }

    public virtual void Set(Trap trap) { }
}