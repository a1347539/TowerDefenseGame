using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sideMenuTower_header : MonoBehaviour
{

    public Text level, towerName;


    public int Level
    {
        set
        {
            level.text = "Lv. " + value.ToString();
        }
    }

    public string TowerName
    {
        set
        {
            towerName.text = value;
        }
    }


    public void setValue(Tower tower, TowerConfig config)
    {
        Level = tower.level + 1;
        TowerName = config.towerName;
    }
}
