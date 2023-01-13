using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sideMenuTower_Stat_Online : MonoBehaviour
{
    [SerializeField]
    public Text damage, range, atkSpeed, effect;

    public string d
    {
        set
        {
            damage.text = value;
        }
    }
    public string r
    {
        set
        {
            range.text = value;
        }
    }
    public string aS
    {
        set
        {
            atkSpeed.text = value;
        }
    }
    public string e
    {
        set
        {
            effect.text = value;
        }
    }

    public void set_value(Tower_Online tower)
    {
        d = String.Format("{0:0.00}", tower.damage);
        r = String.Format("{0:0.00}", tower.actualRange);
        aS = String.Format("{0:0.00}", tower.AtkSpeed);
        e = String.Format("{0:0.00}", tower.effect);
    }
}
