using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private string towerConfigPath;

    protected float[][] playerOnTowerUpgrade;

    protected float[][] playerOnGeneralUpgrade;

    public virtual string TowerConfigPath { get { return towerConfigPath; } }

    [SerializeField]
    private GameObject RadiusPrefab;

    [SerializeField]
    public GameObject rangePrefab;

    public bool isSelected = false;

    public bool isClosingRadius = false;

    private sideMenuTower sideMenuTower;

    public int sortingOrder
    {
        get { return GetComponent<SpriteRenderer>().sortingOrder; }
    }

    public Tile current_tile;

    private GameManager gameManager;

    protected int isGamePause { get { return gameManager.IsPausedInt; } }

    protected Vector2 CenterOfTowerInWorldPosition
    {
        get
        {
            return new Vector2((transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2),
                               (transform.position.y - GetComponent<SpriteRenderer>().bounds.size.x / 2)); //0.04, 0.10
        }
    }

    #region gameplay related

    public TowerConfig config { get { return ConfigLoader.LoadTextFile<TowerConfig>(towerConfigPath); } }

    [SerializeField]
    public List<GameObject> targetList = new List<GameObject>();

    private GameObject radius;

    public GameObject rangeIndicator;

    public int towerId { get { return config.towerId; } }

    public int level { get; private set; }

    public virtual int setLevel { get { return level; } set => level = value; }

    /*
     dictionary in the order of damage range atkspeed effect
     */

    public Dictionary<int, float> incrementFromSupportTower = new Dictionary<int, float> { { 0, 1f }, { 1, 1f }, { 2, 1f }, { 3, 1f } };

    public float damage { get { return stateCalculator.baseStat(config.damage, playerOnTowerUpgrade[1][1], playerOnGeneralUpgrade[0][1], config.damageIncrement, level) * incrementFromSupportTower[0]; } }

    public float range { get { return (stateCalculator.baseStat(config.range, playerOnTowerUpgrade[2][1], playerOnGeneralUpgrade[1][1], config.rangeIncrement, level) * incrementFromSupportTower[1]) / 2; } }

    public float actualRange { get { return stateCalculator.baseStat(config.range, playerOnTowerUpgrade[2][1], playerOnGeneralUpgrade[1][1], config.rangeIncrement, level) * incrementFromSupportTower[1]; } }

    public float AtkSpeed { get { return stateCalculator.baseStat(config.atk_speed, playerOnTowerUpgrade[3][1], playerOnGeneralUpgrade[2][1], config.AtkSpeedIncrement, level) * incrementFromSupportTower[2] * isGamePause; } }

    public float effect { get { return stateCalculator.baseStat(config.effect, playerOnTowerUpgrade[4][1], playerOnGeneralUpgrade[3][1], config.effectIncrement, level) * incrementFromSupportTower[3]; } }

    #region forshow
    public float damage_nextlevel { get { return stateCalculator.baseStat(config.damage, playerOnTowerUpgrade[1][1], playerOnGeneralUpgrade[0][1], config.damageIncrement, level + 1) * incrementFromSupportTower[0]; } }

    public float actualRange_nextlevel { get { return stateCalculator.baseStat(config.range, playerOnTowerUpgrade[2][1], playerOnGeneralUpgrade[1][1], config.rangeIncrement, level + 1) * incrementFromSupportTower[1]; } }

    public float AtkSpeed_nextlevel { get { return stateCalculator.baseStat(config.atk_speed, playerOnTowerUpgrade[3][1], playerOnGeneralUpgrade[2][1], config.AtkSpeedIncrement, level + 1) * incrementFromSupportTower[2]; } }

    public float effect_nextlevel { get { return stateCalculator.baseStat(config.effect, playerOnTowerUpgrade[4][1], playerOnGeneralUpgrade[3][1], config.effectIncrement, level + 1) * incrementFromSupportTower[3]; } }

    #endregion
    public float upgrade_price { get { return config.upgrade_price * (level + 1) * 3; } }

    public float sell_price { get { return config.sell_price * (level + 1) * 2; } }

    public Vector3 Tower_range { get { return new Vector3(actualRange, actualRange, actualRange); }}

    #endregion

    private void Awake()
    {
        gameManager = GameManager.Instance;
        //playerOnGeneralUpgrade = gameManager.player.trees[1];
        //playerOnTowerUpgrade = gameManager.player.trees[towerId + 2];

        playerOnGeneralUpgrade = gameManager.player.trees[1];
        playerOnTowerUpgrade = gameManager.player.trees[towerId + 2];

        rangeIndicator = Instantiate(rangePrefab, CenterOfTowerInWorldPosition, Quaternion.identity);
        rangeIndicator.GetComponent<SpriteRenderer>().transform.SetParent(transform);
        rangeIndicator.GetComponent<CircleCollider2D>().radius = range;

        radius = Instantiate(RadiusPrefab, CenterOfTowerInWorldPosition, Quaternion.identity);
        radius.transform.SetParent(transform);

        sideMenuTower = sideMenuTower.Instance;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        conditionForAction();

        checkSideMenu();
    }

    #region radius check

    private IEnumerator draw_radius()
    {
        radius.GetComponent<SpriteRenderer>().sortingOrder = this.GetComponent<SpriteRenderer>().sortingOrder - 1;
        radius.transform.localScale = Vector3.MoveTowards(radius.transform.localScale, Tower_range, Time.deltaTime * 30);
        yield return null;
    }

    private IEnumerator close_radius()
    {
        radius.transform.localScale = Vector3.MoveTowards(radius.transform.localScale,
                                                          new Vector3(0f, 0f, 0f),
                                                          Time.deltaTime * 30);
        yield return 0;
    }
    private void closeRadius()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseOver();
            if (!isSelected)
            {
                isClosingRadius = true;
            }
        }
    }

    #endregion

    #region sideMenu check

    private void checkSideMenu()
    {
        if (isSelected)
        {
            StartCoroutine(draw_radius());

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                closeRadius();
            }
        }

        else if (!isSelected && isClosingRadius)
        {
            if (!GameManager.Instance.IsPointerOnTower)
            {
                sideMenuTower.close_sideMenu();
            }
            StartCoroutine(close_radius());
        }
    }

    #endregion


    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            this.isSelected = true;
            GameManager.Instance.IsPointerOnTower = true;
            sideMenuTower.GetTowerInfo(this, config, true);
            isClosingRadius = true;
        }
    }

    private void MouseOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Tower>() == null)
            {
                GameManager.Instance.IsPointerOnTower = false;
                isSelected = false;
            }
            else if (hit.collider.GetComponent<Tower>() == this)
            {
                GameManager.Instance.IsPointerOnTower = true;
                isSelected = true;
            }
            else
            {
                GameManager.Instance.IsPointerOnTower = true;
                isSelected = false;
            }
        }
    }

    protected virtual void conditionForAction()
    {

    }

    public virtual void upgrading()
    {
        level++;
        radius.transform.localScale = Vector3.MoveTowards(radius.transform.localScale, Tower_range, Time.deltaTime * 30);
        rangeIndicator.GetComponent<CircleCollider2D>().radius = range;
        sideMenuTower.GetTowerInfo(this, config, false);
    }

    public void getNewStateFromSupport()
    {
        rangeIndicator.GetComponent<CircleCollider2D>().radius = range;
    }
}
