using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sideMenuTower : Singleton<sideMenuTower>
{
    [SerializeField]
    private sideMenuTower_Stat Stats_field;

    [SerializeField]
    private sideMenuTower_UpgradenSell upgrade_field;

    [SerializeField]
    private sideMenuTower_header header;

    [SerializeField]
    private Text description;

    [SerializeField]
    private GameObject upgradeButton;

    public Animator animator { get; private set; }

    public bool isOpen { get; private set; }

    public Tower current_tower { get; private set; }

    public TowerConfig current_tower_config;

    private float Double_click_interval = 0.2f;

    private float lastClickTime;

    private void Start()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleEscape();
    }

    public void GetTowerInfo(Tower tower, TowerConfig config, bool isRefresh)
    {
        Stats_field.DisplayerForUpgrade.SetActive(false);

        if (isRefresh == true)
        {
            open_sideMenu();
        }

        current_tower = tower;
        current_tower_config = config;

        header.setValue(tower, config);
        Stats_field.set_value(tower);
        upgrade_field.set_value(tower);

        if (GameManager.Instance.Currency < current_tower.upgrade_price)
        {
            upgradeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            upgradeButton.GetComponent<Button>().interactable = true;
        }
    }

    public void open_sideMenu()
    {
        if (isOpen)
        {
            animator.SetBool("isRefresh", true);
            StartCoroutine(turnOffIsRefresh());
            return;
        }
        animator.SetBool("isOpen", true);
        isOpen = true;
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
        if (!(current_tower is null))
        {
            if (current_tower.isSelected)
            { 
                current_tower.isSelected = false; 
            }
        }
    }

    public void GetIncrementInfo()
    {
        Stats_field.set_incrementValue(current_tower, current_tower_config);
    }


    public void upgrade()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= Double_click_interval)
        {
            if (GameManager.Instance.Currency > current_tower.upgrade_price)
            {
                GameManager.Instance.Currency -= (int)current_tower.upgrade_price;
                current_tower.upgrading();
                Stats_field.DisplayerForUpgrade.SetActive(false);
            }
        }
        lastClickTime = Time.time;
    }

    public void sell(GameObject button)
    {

        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= Double_click_interval)
        {
            GameManager.Instance.allTower.Remove(current_tower);
            try
            {
                Destroy(current_tower.gameObject);
            }
            catch (Exception e)
            { 
            }

            GameManager.Instance.Currency += (int)current_tower.sell_price;
            current_tower.current_tile.GetComponent<BoxCollider2D>().enabled = true;
            current_tower.current_tile.IsWalkable = true;
            current_tower.current_tile.TileIsEmpty = true;
            GameManager.Instance.isMapChanged = true;
            close_sideMenu();
            button.GetComponent<Button>().interactable = false;

            StartCoroutine(activateSellButton(button));
        }

        lastClickTime = Time.time;
    }

    IEnumerator activateSellButton(GameObject button) 
    {
        yield return new WaitForSeconds(0.4f);
        button.GetComponent<Button>().interactable = true;
    }
}
