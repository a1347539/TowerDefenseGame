using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class towerButton : MonoBehaviour
{

    [SerializeField]
    private GameObject towerPrefab;

    private string path;

    private Tile current_tile { get { return sidemenu.current_tile; } }

    public TowerConfig config;

    private sideMenu sidemenu;

    private bool buyable = false;

    private float Double_click_interval = 0.2f;

    private float lastClickTime;

    private bool isLocked;

    private void Awake()
    {
        path = towerPrefab.GetComponent<Tower>().TowerConfigPath;
        config = ConfigLoader.LoadTextFile<TowerConfig>(path);
    }
    // Start is called before the first frame update
    void Start()
    {
        sidemenu = transform.parent.parent.parent.parent.GetComponent<sideMenu>();
        
        if (GameManager.Instance.player.trees[config.towerId + 2][0][0] == 0)
        {
            isLocked = true;
            this.gameObject.GetComponent<Button>().interactable = false;
            this.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            if (GameManager.Instance.Currency < config.price || !sidemenu.TowerPlaceable)
            {
                GetComponent<Button>().interactable = false;
                gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
                gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void pickTower()
    {

        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= Double_click_interval)
        {
            current_tile.PlaceTower(towerPrefab, config.price, 0);
            current_tile.isSelected = false;
            sidemenu.Stats_field.GetComponent<sideMenu_Stat>().Reset_field();
            sidemenu.isOpen = false;
            sidemenu.animator.SetBool("isOpen", false);
        }

        lastClickTime = Time.time;
    }
}
