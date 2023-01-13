using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class sideMenu : Singleton<sideMenu>
{
    public Animator animator { get; set; }

    public bool isOpen { get; set; }

    [SerializeField]
    private Text header;

    [SerializeField]
    private GameObject TowerButtonsScroll;

    [SerializeField]
    public GameObject Stats_field;

    [SerializeField]
    private GameObject frameForTowerSelection;

    [SerializeField]
    public GameObject button_field;

    private PlayerConfig playerConfig { get { return GameManager.Instance.player; } }

    public List<Enemy> EnemiesList = new List<Enemy>();

    public Tile current_tile;

    private bool towerPlaceable;

    public bool TowerPlaceable { get => towerPlaceable; set => towerPlaceable = value; }

    private bool enemyInTile;

    public bool EnemyInTile { get => enemyInTile; set => enemyInTile = value; }

    private bool PathIsBlocked;

    private int numbersOfTower;

    private int numberOfTowerExceeded = 0;

    public string Header 
    {
        set 
        { 
            header.GetComponent<Text>().text = value.ToString(); 
        } 
    }
    private void Awake()
    {
        

    }

    private void Start()
    {
        numbersOfTower = playerConfig.trees.Count - 2;

        isOpen = false;
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        HandleEscape();

        if (isOpen && !towerPlaceable)
        { 
            checkTowerPlacable();
        }
    }

    public void getTileInfo(Tile tile)
    {
        open_sideMenu();

        current_tile = tile;

        checkPathBlocked();

        //if (tile.gameObject.tag == "Road") { current_tile.IsWalkable = true; }

        string tempTag = tile.tag;

        Header = tempTag;

        if (tempTag != "Road")
        {
            TowerButtonsScroll.SetActive(false);
        }
        else
        {
            TowerButtonsScroll.SetActive(true);
        }
    }

    public void getTowerInfo(int buttonIndex)
    {
        towerButton grandchildren_towerButton = TowerButtonsScroll.transform.GetChild(buttonIndex).GetComponent<towerButton>();

        TowerConfig config = grandchildren_towerButton.config;

        Stats_field.GetComponent<sideMenu_Stat>().set_value(config);
    }


    public void open_sideMenu()
    {
        
        if (isOpen)
        {
            animator.SetBool("isRefresh", true);
            StartCoroutine(turnOffIsRefresh());
            return;
        }
        animator.SetBool("isOpen", !isOpen);
        isOpen = !isOpen;
    }
    IEnumerator turnOffIsRefresh()
    {
        yield return 0;
        animator.SetBool("isRefresh", false);
    }

    public void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            close_sideMenu();
        }
    }

    public void close_sideMenu()
    {
        animator.SetBool("isOpen", false);
        isOpen = false;
        current_tile.ColorTile(Color.white);
        //current_tile.IsWalkable = true;

        if (!current_tile.isSelected)
        { current_tile.isSelected = false; }
    }

    #region checkPlacable

    private void checkTowerPlacable()
    {
        if (PathIsBlocked)
        {
            //TowerPlaceable = false;
        }
        else
        { 
           // TowerPlaceable = true;
        }
        //current_tile.IsWalkable = true;
    }

    private void checkPathBlocked()
    {
        StartCoroutine(checkCorotine());
    }

    IEnumerator checkCorotine()
    {
        yield return new WaitForSeconds(0.1f);
        bool temp = PathFinding.FindPath(GameManager.Instance.StartPosition, GameManager.Instance.EndPosition);
        if (!temp)
        {
            PathIsBlocked = true;
            TowerPlaceable = false;
            yield break;
        }

        //StartCoroutine(checkCorotine());
        PathIsBlocked = false;
        TowerPlaceable = true;
    }
    #endregion
}

