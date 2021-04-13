using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sideMenuTower_Stat : MonoBehaviour
{
    [SerializeField]
    public Text damage, range, atkSpeed, effect;

    [SerializeField]
    public Text DIncrement, RIncrement, aSIncrement, EIncrement;

    [SerializeField]
    public GameObject DisplayerForUpgrade;


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

    public string dI
    {
        set
        {
            DIncrement.text = value;
        }
    }
    public string rI
    {
        set
        {
            RIncrement.text = value;
        }
    }
    public string aSI
    {
        set
        {
            aSIncrement.text = value;
        }
    }
    public string eI
    {
        set
        {
            EIncrement.text = value;
        }
    }

    public void set_value(Tower tower)
    {
        d = String.Format("{0:0.00}", tower.damage);
        r = String.Format("{0:0.00}", tower.actualRange);
        aS = String.Format("{0:0.00}", tower.AtkSpeed);
        e = String.Format("{0:0.00}", tower.effect);
    }

    public void set_incrementValue(Tower tower, TowerConfig config)
    {
        DisplayerForUpgrade.SetActive(true);
        dI = String.Format("{0:0.00}", (tower.damage_nextlevel - tower.damage));
        rI = String.Format("{0:0.00}", (tower.actualRange_nextlevel - tower.actualRange));
        aSI = String.Format("{0:0.00}", (tower.AtkSpeed_nextlevel - tower.AtkSpeed));
        eI = String.Format("{0:0.00}", (tower.effect_nextlevel - tower.effect));
    }
}
