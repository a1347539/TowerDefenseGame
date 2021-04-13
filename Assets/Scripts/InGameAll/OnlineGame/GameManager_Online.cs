using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Online : Singleton<GameManager_Online>
{
    [SerializeField]
    public Looted Looted;

    [SerializeField]
    public Lootable Lootable;

    [SerializeField]
    private sup_menu_Online sup_menu;

    [SerializeField]
    private endSessionMenu_Online endSessionMenu;

    public int goldPerLoot { get; private set; }

    private int goldLooted;

    private int opponentGold;

    private int opponentHealth;

    public int GoldLooted 
    { 
        get { return goldLooted; }
        private set { goldLooted = value; } 
    }

    public PlayerConfig player;

    public PlayerConfig opponent;

    public Spawner_Online spawer { get { return GetComponent<Spawner_Online>(); } }

    [SerializeField]
    private GameObject StateMenu;

    [SerializeField]
    public Transform damageTagContainer;

    private bool isPointerOnTower = false;

    public bool IsPointerOnTower { get => isPointerOnTower; set => isPointerOnTower = value; }

    private bool isPointerOnTile = false;

    public bool IsPointerOnTile { get => isPointerOnTile; set => isPointerOnTile = value; }


    #region PathFinding

    private List<Node> path = new List<Node>();

    public List<Node> Path { get => path; }

    public Point StartPosition
    {
        get { return LevelManager_Online.Instance.spawnPoint; }
    }

    public Point EndPosition
    {
        get { return LevelManager_Online.Instance.desPoint; }
    }

    private bool pathIsValid = true;

    #endregion


    #region gameplay related

    public List<Tower_Online> allTower = new List<Tower_Online>();

    private string[] allWaves;

    public string[] AllWaves { get => allWaves; set => allWaves = value; }

    private int currency;

    private int health;

    private float LootableGold = 0;

    private bool saved;

    #endregion


    private bool isPaused = false;

    public bool IsPaused { get => isPaused; set => isPaused = value; }

    public int IsPausedInt = 1;

    public Dictionary<int, int> allEnemiesTemp { get; private set; }


    private void Awake()
    {
        allEnemiesTemp = new Dictionary<int, int>();
        player = ConfigLoader.Load(staticTransition.userID, staticTransition.userPass);
        print(staticTransition.opponentConfig);
        opponent = staticTransition.opponentConfig; 
    }

    // Start is called before the first frame update
    void Start()
    {
        opponentGold = (int)opponent.gameConfig[0];
        opponentHealth = (int)opponent.gameConfig[3];

        Lootable.Gold = opponentGold / 2;
        Lootable.Life = opponentHealth;
        LootableGold = opponentGold / 2;
        print(opponentGold/2);
        print(opponentGold / 20);
        goldPerLoot = opponentGold / 20;
        StartCoroutine(oneFrameToInitPath());

        saved = false;
    }

    IEnumerator oneFrameToInitPath()
    {
        yield return new WaitForEndOfFrame();
        Init_Path();
    }

    public void Init_Path()
    {
        if (PathFinding.FindPath(StartPosition, EndPosition, 1))
        {
            path = PathFinding.Path;

            /*foreach (Node node in path)
            {
                print(node.tilePosition);
            }*/
           

            spawer.path = path;

            pathIsValid = true;
        }
        else
        {
            pathIsValid = false;
        }
    }

    public void isPauseToggler()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        { IsPausedInt = 0; }
        else
        { IsPausedInt = 1; }

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

    public void takeLife()
    {
        opponentHealth--;
        float tempOpponentGold = LootableGold;
        print(goldPerLoot);
        tempOpponentGold -= goldPerLoot;

        if (opponentHealth <= 0 || tempOpponentGold <= 0)
        {
            endSessionMenu.gameObject.SetActive(true);
            if (!saved)
            { 
                saveData();
                saved = true;
            }
           
            return;
        }

        LootableGold -= goldPerLoot;

        GoldLooted += goldPerLoot;

        Lootable.Life = opponentHealth;

        Lootable.Gold = (int)LootableGold;

        Looted.Gold = GoldLooted;

    }

    public void saveData()
    {
        player.gold += GoldLooted;
        opponent.gameConfig[0] -= GoldLooted;

        /*
        for (int i = 0; i < allEnemiesTemp.Count; i++)
        {
            print(player.allCapturedEnemy[i]+" "+ allEnemiesTemp[i]);
            player.allCapturedEnemy[i] = allEnemiesTemp[i];
        }
        */
        player.allCapturedEnemy = allEnemiesTemp;
        ConfigSaver.save(staticTransition.userID, staticTransition.userPass, player);
        ConfigSaver.saveOpponent(staticTransition.userID, staticTransition.userPass, player, opponent);
    }
}
