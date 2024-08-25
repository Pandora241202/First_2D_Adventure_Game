using Unity.VisualScripting;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] AllBulletConfig allBulletConfig;

    public LayerMask GroundLayerMask => groundLayerMask;

    public PlayerManager playerManager;
    public BulletManager bulletManager;

    private AllManager() {}

    private static AllManager _instance;

    public static AllManager Instance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<AllManager>();
        }
        return _instance;
    }

    void Start()
    {
        playerManager = new PlayerManager(playerConfig);
        bulletManager = new BulletManager(allBulletConfig);
    }

    void Update()
    {
        playerManager.MyUpdate();
        bulletManager.MyUpdate();
    }

    private void LateUpdate()
    {
        bulletManager.MyLateUpdate();
    }
}
