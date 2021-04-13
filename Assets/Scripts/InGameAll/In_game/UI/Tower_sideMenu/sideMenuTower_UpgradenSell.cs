using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sideMenuTower_UpgradenSell : MonoBehaviour
{
    [SerializeField]
    private Text upgrade, sell;

    private int u
    {
        set 
        {
            upgrade.text = value.ToString();
        }
    }

    private int s
    {
        set
        {
            sell.text = value.ToString();
        }
    }

    public void set_value(Tower tower)
    {
        u = (int)tower.upgrade_price;
        s = (int)tower.sell_price;
    }
}
