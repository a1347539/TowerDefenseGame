using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lootable : MonoBehaviour
{
    [SerializeField]
    private Text gold, life;

    public int Gold
    {
        set { gold.text = value.ToString(); }
    }

    public int Life
    {
        set { life.text = value.ToString(); }
    }
}
