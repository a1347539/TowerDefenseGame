using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private top_menu top_menu;

    [SerializeField]
    private sup_menu sup_menu;

    [SerializeField]
    private Pause_Menu pause_menu;

    public PlayerConfig player { get; private set; }

    public Dictionary<int, int> capturedEnemy = new Dictionary<int, int>();

    public int endState { get; private set; }

    private float rewardRate_win = 0.9f;

    public float RewardRate_win { get { return rewardRate_win; } }

    private float rewardRate_lost = 0.5f;

    public float RewardRate_lost { get { return rewardRate_lost; } }

    [SerializeField]
    public List<Enemy> enemyList = new List<Enemy>();

    [SerializeField]
    private GameObject endSessionMenu;

    [SerializeField]
    public Transform damageTagContainer;

    public bool isMapChanged = true;

    private bool isPointerOnTower = false;

    public bool IsPointerOnTower { get => isPointerOnTower; set => isPointerOnTower = value; }

    private bool isPointerOnTile = false;

    public bool IsPointerOnTile { get => isPointerOnTile; set => isPointerOnTile = value; }

    #region PathFinding

    private List<Node> path = new List<Node>();

    public List<Node> Path { get => path; }

    public Point StartPosition
    {
        get { return LevelManager.Instance.spawnPoint; }
    }

    public Point EndPosition
    {
        get { return LevelManager.Instance.desPoint; }
    }

    private bool pathIsValid = true;

    #endregion

    #region gameplay related

    public List<Tower> allTower = new List<Tower>();

    private string[] allWaves;

    public string[] AllWaves { get => allWaves; set => allWaves = value; }

    private float spawnTimer = 31f;

    private int current_wave;

    private int currency;

    private int health;

    private int numOfCapturedEnemy;

    #endregion

    #region text handler

    public int Current_wave
    {
        get => current_wave;
        set 
        {
            current_wave = value;
            top_menu.waveTxt = value;
        }
    }

    public int Currency 
    {
        get => currency;
        set 
        {
            currency = value;
            top_menu.currencyTxt = value;
        }
    }

    public int Health
    {
        get => health;
        set 
        {
            if (value <= 0)
            {
                health = value;
                top_menu.healthTxt = value;
                onLost(); 
            }
            else
            {
                health = value;
                top_menu.healthTxt = value;
            }
        }
    }

    public int NumOfCapturedEnemy
    {
        get { return numOfCapturedEnemy; }

        set
        {
            numOfCapturedEnemy = value;
            top_menu.capturedEnemyTxt = value;
        }
    }


    #endregion

    #region Endstate

    private bool isLost = false;

    private bool isWon = false;

    #endregion

    private bool isPaused = false;

    public bool IsPaused { get => isPaused; set => isPaused = value; }

    //0-> paused; 1-> not paused;
    public int isPausedInt = 1;

    public int IsPausedInt { get { return isPausedInt; } }

    private Sprite[] pause_resume;

    [SerializeField]
    private GameObject enemyPathChanger;

    public List<pathChanger> pathChangerList = new List<pathChanger>();

    private void Awake()
    {
        player = ConfigLoader.Load(staticTransition.userID, staticTransition.userPass);
        NumOfCapturedEnemy = 0;
        Current_wave = 0;
        Currency = 500 + (int)player.trees[0][1][1];
        Health = 100 + (int)player.trees[0][0][1];
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Spawner>().getBossWave(allWaves);

        player.gameConfig = new float[] { Currency, NumOfCapturedEnemy, Current_wave, Health };
        ConfigSaver.save(staticTransition.userID, staticTransition.userPass, player);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMapChanged) 
        {
            Init_Path();

            List<Enemy> temp = enemyList;

            pathChanger pathChanger = Instantiate(enemyPathChanger).GetComponent<pathChanger>();
            pathChanger.init(temp);
            pathChangerList.Add(pathChanger);
            

            isMapChanged = false;

        }

        spawnTimer -= Time.deltaTime * IsPausedInt;
        if (spawnTimer < 0)
        {
            StartWave();
        }

        sup_menu.Timer = (int)spawnTimer;
    }

    IEnumerator changeEnemyPath(List<Enemy> enemies) 
    {
        
        foreach (Enemy enemy in new List<Enemy>(enemies))
        {
            //enemy.isMapChanged = true;
            if (enemy != null)
            {
                enemy.getNewPath();
            }
            

            yield return new WaitForSeconds(0.04f);
        }
    }



    public void BuyTower(Tile current_tile, int price)
    {

        Currency -= price;

        current_tile.IsWalkable = false;
        current_tile.TileIsEmpty = false;
        isMapChanged = true;

    }


    public void StartWave()
    {
        Current_wave++;

        GetComponent<Spawner>().SpawnWave(Current_wave);
        spawnTimer = 31;
    }

    public void Init_Path()
    {
        if (PathFinding.FindPath(StartPosition, EndPosition))
        {
            path = PathFinding.Path;

            GetComponent<Spawner>().path = path;

            pathIsValid = true;
        }
        else
        {
            pathIsValid = false;
        }
    }

    private void onLost()
    {
        pause_menu.getGameInfo();

        Time.timeScale = 0;
        endSessionMenu.SetActive(true);
    }

    public void isPauseToggler()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        { isPausedInt = 0; }
        else
        { isPausedInt = 1; }

        sup_menu.pause_resume.GetComponent<PauseResume>().setValue(IsPaused);
    }

    public void InGameSpeed()
    {
        Time.timeScale += 1.0f;

        if (Time.timeScale == 5.0f)
        { 
            Time.timeScale = 1.0f;
        }
        sup_menu.InGameSpeed = (int)Time.timeScale;
    }

    public void initOngoingGame()
    {
        Currency = (int)player.gameConfig[0];
        NumOfCapturedEnemy = (int)player.gameConfig[1];
        Current_wave = (int)player.gameConfig[2];
        Health = (int)player.gameConfig[3];
        capturedEnemy = player.capturedEnemy_duringSession;
    }
    void OnApplicationQuit()
    {
        Pause_Menu.Instance.endApplication(allTower, Currency, NumOfCapturedEnemy, Current_wave, Health, capturedEnemy);
    }
}
