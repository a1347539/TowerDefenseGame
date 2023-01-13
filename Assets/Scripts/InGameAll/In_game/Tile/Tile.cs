using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{

    public Color32 current_color { get; private set; }

    public SpriteRenderer spriteRenderer { get; set; }

    public Point GridPosition { get; private set; }

    [SerializeField]
    private bool isWalkable;

    [SerializeField]
    private bool isWalkableFinal;
    
    public bool IsWalkable { get => isWalkable; set => isWalkable = value; }

    public bool IsWalkableFinal { get => isWalkableFinal; }

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

    private sideMenu sideMenu;


    // Start is called before the first frame update
    void Start()
    {
        sideMenu = sideMenu.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0) && !MouseOver())
            {
                if (IsWalkableFinal && TileIsEmpty) { IsWalkable = true;  }
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (!GameManager.Instance.IsPointerOnTile)
                    {
                        sideMenu.close_sideMenu();
                    }
                    sideMenu.Stats_field.GetComponent<sideMenu_Stat>().Reset_field();
                    ColorTile(Color.white);
                    isSelected = false;
                    
                }
            }

        }
    }

    public void Setup(Point gridPos, Vector3 Pos, bool walkable, Transform parent)
    {
        
        if (walkable) { IsWalkable = true; isWalkableFinal = true; }
        else { IsWalkable = false; isWalkableFinal = false; }

        TileIsEmpty = true;

        this.GridPosition = gridPos;

        transform.position = Pos;

        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && TileIsEmpty)
            {
                sideMenu.EnemyInTile = false;
                IsWalkable = false;
                isSelected = true;
                sideMenu.getTileInfo(this);
                GameManager.Instance.IsPointerOnTile = true;
                if (isWalkableFinal) { ColorTile(new Color32(63, 240, 36, 250)); }
                else { ColorTile(new Color32(235, 88, 52, 250)); }
                
            }
        }
    }


    public void PlaceTower(GameObject towerPrefab, int price, int level)
    {
        ColorTile(Color.white);
        isSelected = false;

        GetComponent<BoxCollider2D>().enabled = false;

        GameObject tower = Instantiate(towerPrefab,
                                       new Vector3(transform.position.x + ((GetComponent<SpriteRenderer>().bounds.size.x - towerPrefab.GetComponent<SpriteRenderer>().bounds.size.x)/2),
                                                   transform.position.y - ((GetComponent<SpriteRenderer>().bounds.size.y - towerPrefab.GetComponent<SpriteRenderer>().bounds.size.y)/2),
                                                   0),
                                       Quaternion.identity);
        tower.GetComponent<Tower>().current_tile = this;

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.y;

        tower.transform.SetParent(transform);

        tower.GetComponent<Tower>().setLevel = level;

        GameManager.Instance.allTower.Add(tower.GetComponent<Tower>());

        GameManager.Instance.BuyTower(this, price);

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
            if (hit.collider.GetComponent<Tile>() == null)
            {
                GameManager.Instance.IsPointerOnTile = false;
            }
            else if (hit.collider.GetComponent<Tile>() == this)
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
            if (obj.tag == "GroundEnemy")
            { sideMenu.EnemyInTile = true; }
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (isSelected)
        {
            if (obj.tag == "GroundEnemy")
            { sideMenu.EnemyInTile = false; }
        }
    }
}
