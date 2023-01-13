using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sideMenu_Stat : MonoBehaviour
{
    public Text damage, range, atkSpeed, effect, price;

    private PlayerConfig playerConfig { get { return GameManager.Instance.player; } }


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
    public string p
    {
        set
        {
            price.text = value;
        }
    }

    public void set_value(TowerConfig config)
    {
        int towerId = config.towerId;

        float[][] playerOnGeneralUpgrade = playerConfig.trees[1];
        float[][] playerOnTowerUpgrade = playerConfig.trees[towerId + 2];

        d = String.Format("{0:0.00}", stateCalculator.baseStat(config.damage, playerOnTowerUpgrade[1][1], playerOnGeneralUpgrade[0][1], config.damageIncrement, 0));
        r = String.Format("{0:0.00}", stateCalculator.baseStat(config.range, playerOnTowerUpgrade[2][1], playerOnGeneralUpgrade[1][1], config.rangeIncrement, 0));
        aS = String.Format("{0:0.00}", stateCalculator.baseStat(config.atk_speed, playerOnTowerUpgrade[3][1], playerOnGeneralUpgrade[2][1], config.AtkSpeedIncrement, 0));
        e = String.Format("{0:0.00}", stateCalculator.baseStat(config.effect, playerOnTowerUpgrade[4][1], playerOnGeneralUpgrade[3][1], config.effectIncrement, 0));
        p = ((int)config.price).ToString();
    }

    public void Reset_field()
    {
        d = r = aS = e = p = "- - -";      
    }
}
