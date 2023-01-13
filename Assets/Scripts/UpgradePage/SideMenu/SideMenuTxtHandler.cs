using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMenuTxtHandler : MonoBehaviour
{
    [SerializeField]
    private Text UpgradeName, specificName, UpgradeLevel, specificName2, upgradeIncrement, upgradeCurrent, price;

    private string UN { set { UpgradeName.text = value; } }
    private string sN { set { specificName.text = value; } }
    private string UL { set { UpgradeLevel.text = value; } }
    private string sN2 { set { specificName2.text = value; } }
    private string UI { set { upgradeIncrement.text = value; } }
    private string UC { set { upgradeCurrent.text = value; } }
    private string pr { set { price.text = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue(int displayerType, string un, string sn, float ul, float ui, float uc, float p)
    {
        if (displayerType == 0)
        {
            UN = un;
            sN = sN2 = sn;
            UL = ul.ToString();
            UI = String.Format("{0:0.0} %", ui * 100);
            UC = String.Format("{0:0.0} %", uc * 100);
            pr = ((int)p).ToString();
        }
        else if (displayerType == 1)
        {
            UN = un;
            sN = sN2 = sn;
            UL = ul.ToString();
            UI = Math.Round(ui).ToString();
            UC = Math.Round(uc).ToString();
            pr = ((int)p).ToString();
        }
        else if (displayerType == 2)
        {
            UN = un;
            sN = sn;
            if (ul == 0) { UL = "locked"; }
            else { UL = "unlocked"; }
            sN2 = "";
            UI = "";
            UC = "";
            pr = ((int)p).ToString();
        }
    }

    public void resetField()
    {
        UN = "";
        sN = "";
        UL = "";
        UI = "";
        UC = "";
        pr = "";
    }
}
