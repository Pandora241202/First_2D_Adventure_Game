using UnityEngine;

public class AllManager : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] AllBulletConfig allBulletConfig;
    [SerializeField] AllTileConfig allTileConfig;
    [SerializeField] AllLevelConfig allLevelConfig;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform gridTrans;

    public LayerMask GroundLayerMask => groundLayerMask;
    public Transform GridTrans => gridTrans;

    public PlayerManager playerManager;
    public BulletManager bulletManager;
    public MapManager mapManager;
    public LevelManager levelManager;

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
        mapManager = new MapManager(allTileConfig);
        levelManager = new LevelManager(allLevelConfig);
        
        levelManager.LoadLevelByNum(0);
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
