using UnityEngine;

[CreateAssetMenu(fileName = "SpikeTrapConfig", menuName = "Config/TrapConfig/SpikeTrap")]
public class SpikeTrapConfig : TrapConfig
{
    public override void Set(Trap trap)
    {
        SpriteRenderer spriteRend = trap.trans.gameObject.GetComponent<SpriteRenderer>();
        BoxCollider2D boxCol = trap.trans.gameObject.GetComponent<BoxCollider2D>();
        spriteRend.size = new Vector2(trap.range, spriteRend.size.y);
        boxCol.size = new Vector2(trap.range, boxCol.size.y);
    }
}