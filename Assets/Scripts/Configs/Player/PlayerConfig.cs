using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/Player")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] float speed;

    [SerializeField] float jumpHigh;

    [SerializeField] float maxJumpKeyHoldDuration;

    [SerializeField] float maxJumpWallDuration;

    [SerializeField] float attackCD;

    [SerializeField] GameObject playerPrefab;

    public float Speed => speed;

    public float JumpHigh => jumpHigh;

    public float MaxJumpKeyHoldDuration => maxJumpKeyHoldDuration;

    public float MaxJumpWallDuration => maxJumpWallDuration;

    public float AttackCD => attackCD;

    public GameObject PlayerPrefab => playerPrefab;
}