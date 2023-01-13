using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower_Online : MonoBehaviour
{
    [SerializeField]
    private string towerConfigPath;

    public virtual string TowerConfigPath { get { return towerConfigPath; } }

    [SerializeField]
    private GameObject RadiusPrefab;

    [SerializeField]
    public GameObject rangePrefab;

    public int sortingOrder
    {
        get { return GetComponent<SpriteRenderer>().sortingOrder; }
    }

    public bool isSelected = false;

    public bool isClosingRadius = false;

    private sideMenuTower_Online sideMenuTower;

    public Tile_Online current_tile;

    private GameManager_Online gameManager;

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

    public List<GameObject> targetList = new List<GameObject>();

    private GameObject radius;

    public GameObject rangeIndicator;

    public int towerId { get { return config.towerId; } }

    public int level { get; private set; }

    public virtual int setLevel { set => level = value; }

    public Dictionary<int, float> incrementFromSupportTower = new Dictionary<int, float> { { 0, 1f }, { 1, 1f }, { 2, 1f }, { 3, 1f } };

    public float damage { get; private set; }

    public virtual float setDamage { set => damage = value; }

    public float radiusOfCircle { get; private set; }

    public virtual float setRadiusOfCircle { set => radiusOfCircle = value; }

    public float actualRange { get; private set; }

    public virtual float setActualRange { set => actualRange = value; }

    public float AtkSpeed { get; private set; }

    public virtual float setAtkSpeed { set => AtkSpeed = value; }

    public float effect { get; private set; }

    public virtual float setEffect { set => effect = value; }


    public Vector3 Tower_range { get { return new Vector3(actualRange, actualRange, actualRange); } }

    #endregion

    private void Awake()
    {
        gameManager = GameManager_Online.Instance;

        rangeIndicator = Instantiate(rangePrefab, CenterOfTowerInWorldPosition, Quaternion.identity);
        rangeIndicator.GetComponent<SpriteRenderer>().transform.SetParent(transform);
        rangeIndicator.GetComponent<CircleCollider2D>().radius = radiusOfCircle;

        radius = Instantiate(RadiusPrefab, CenterOfTowerInWorldPosition, Quaternion.identity);
        radius.transform.SetParent(transform);

        sideMenuTower = sideMenuTower_Online.Instance;
    }


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
            if (!GameManager_Online.Instance.IsPointerOnTower)
            {
                sideMenuTower.close_sideMenu();
            }
            StartCoroutine(close_radius());
        }
    }

    #endregion

    private void OnMouseDown()
    {
        this.isSelected = true;
        GameManager_Online.Instance.IsPointerOnTower = true;
        sideMenuTower.GetTowerInfo(this, config, true);
        isClosingRadius = true;
    }

    private void MouseOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Tower_Online>() == null)
            {
                GameManager_Online.Instance.IsPointerOnTower = false;
                isSelected = false;
            }
            else if (hit.collider.GetComponent<Tower_Online>() == this)
            {
                GameManager_Online.Instance.IsPointerOnTower = true;
                isSelected = true;
            }
            else
            {
                GameManager_Online.Instance.IsPointerOnTower = true;
                isSelected = false;
            }
        }
    }

    protected virtual void conditionForAction()
    {

    }
}
