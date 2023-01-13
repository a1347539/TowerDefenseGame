using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile_Online : MonoBehaviour
{

    public Color32 current_color { get; private set; }

    public SpriteRenderer spriteRenderer { get; set; }

    public Point GridPosition { get; private set; }

    [SerializeField]
    private bool isWalkable;
    
    public bool IsWalkable { get => isWalkable; set => isWalkable = value; }

    public bool TileIsEmpty { get; set; }

    public bool debugging { get; set; }

    public bool isSelected = false;

    public Vector3 CenterOfGridInWorldPosition
    {
        get
        {
            return new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2,
                               transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2);
        }
    }

    //private sideMenu sideMenu;


    // Start is called before the first frame update
    void Start()
    {
        //sideMenu = sideMenu.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0) && !MouseOver())
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (!GameManager_Online.Instance.IsPointerOnTile)
                    {
                        //sideMenu.close_sideMenu();
                    }
                    //sideMenu.Stats_field.GetComponent<sideMenu_Stat>().Reset_field();
                    ColorTile(Color.white);
                    isSelected = false;
                }
            }

        }
    }

    public void Setup(Point gridPos, Vector3 Pos, bool walkable, Transform parent)
    {
        
        if (walkable) { IsWalkable = true; }
        else { IsWalkable = false; }

        TileIsEmpty = true;

        this.GridPosition = gridPos;

        transform.position = Pos;

        transform.SetParent(parent);

        LevelManager_Online.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && TileIsEmpty)
            {
                GameManager_Online.Instance.IsPointerOnTile = true;
                if (isWalkable) { ColorTile(new Color32(63, 240, 36, 250)); }
                else { ColorTile(new Color32(235, 88, 52, 250)); }
                isSelected = true;
            }
        }
    }


    public void PlaceTower(GameObject towerPrefab, int price, float[] towerInfo)
    {
        ColorTile(Color.white);
        isSelected = false;

        GetComponent<BoxCollider2D>().enabled = false;

        GameObject tower = Instantiate(towerPrefab,
                                       new Vector3(transform.position.x + ((GetComponent<SpriteRenderer>().bounds.size.x - towerPrefab.GetComponent<SpriteRenderer>().bounds.size.x)/2),
                                                   transform.position.y - ((GetComponent<SpriteRenderer>().bounds.size.y - towerPrefab.GetComponent<SpriteRenderer>().bounds.size.y)/2),
                                                   0),
                                       Quaternion.identity);
        tower.GetComponent<Tower_Online>().current_tile = this;

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.y;

        tower.transform.SetParent(transform);

        tower.GetComponent<Tower_Online>().setLevel = (int)towerInfo[3];

        tower.GetComponent<Tower_Online>().setDamage = towerInfo[4];

        tower.GetComponent<Tower_Online>().rangeIndicator.GetComponent<CircleCollider2D>().radius = towerInfo[5]/2;

        tower.GetComponent<Tower_Online>().setActualRange = towerInfo[5];

        tower.GetComponent<Tower_Online>().setAtkSpeed = towerInfo[6];

        tower.GetComponent<Tower_Online>().setEffect = towerInfo[7];

        GameManager_Online.Instance.allTower.Add(tower.GetComponent<Tower_Online>());

        this.IsWalkable = false;

        this.TileIsEmpty = false;

    }


    public void ColorTile(Color Color)
    {
        GetComponent<SpriteRenderer>().color = Color;
    }

    private bool MouseOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit)
        {
            if (hit.collider.GetComponent<Tile_Online>() == null)
            {
                GameManager_Online.Instance.IsPointerOnTile = false;
            }
            else if (hit.collider.GetComponent<Tile_Online>() == this)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (isSelected)
        {
            if (obj.tag == "GroundEnemy") { }
            //{ sideMenu.EnemyInTile = true; }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (isSelected)
        {
            if (obj.tag == "GroundEnemy") { }
            //{ sideMenu.EnemyInTile = false; }
        }
    }
}
