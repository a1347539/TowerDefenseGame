using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sideMenuTower_Online : Singleton<sideMenuTower_Online>
{
    [SerializeField]
    private sideMenuTower_Stat_Online Stats_field;

    [SerializeField]
    private sideMenuTower_header_Online header;

    [SerializeField]
    private Text description;

    public Animator animator { get; private set; }

    public bool isOpen { get; private set; }

    public Tower_Online current_tower { get; private set; }

    public TowerConfig current_tower_config;

    private void Start()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleEscape();
    }

    public void GetTowerInfo(Tower_Online tower, TowerConfig config, bool isRefresh)
    {

        if (isRefresh == true)
        {
            open_sideMenu();
        }

        current_tower = tower;
        current_tower_config = config;

        header.setValue(tower, config);
        Stats_field.set_value(tower);
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
        if (current_tower.isSelected)
        { current_tower.isSelected = false; }
    }

}
