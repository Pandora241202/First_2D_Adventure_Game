using UnityEngine;

public class AllManager : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] AllBulletConfig allBulletConfig;
    [SerializeField] AllTileConfig allTileConfig;
    [SerializeField] AllLevelConfig allLevelConfig;
    [SerializeField] AllTrapConfig allTrapConfig;
    [SerializeField] AllEnemyConfig allEnemyConfig;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform gridTrans;
    [SerializeField] Camera cam;

    public LayerMask GroundLayerMask => groundLayerMask;
    public Transform GridTrans => gridTrans;

    public PlayerManager playerManager;
    public BulletManager bulletManager;
    public MapManager mapManager;
    public LevelManager levelManager;
    public TrapManager trapManager;
    public EnemyManager enemyManager;
    public CamManager camManager;

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
        trapManager = new TrapManager(allTrapConfig);
        enemyManager = new EnemyManager(allEnemyConfig);
        camManager = new CamManager(cam);
        
        levelManager.LoadLevelByNum(0);
    }

    void Update()
    {
        playerManager.MyUpdate();
        bulletManager.MyUpdate();
        camManager.MyUpdate();
        trapManager.MyUpdate();
        enemyManager.MyUpdate();
    }

    private void LateUpdate()
    {
        bulletManager.MyLateUpdate();
        enemyManager.MyLateUpdate();
    }
}
