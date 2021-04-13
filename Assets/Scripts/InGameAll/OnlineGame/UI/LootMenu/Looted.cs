using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looted : MonoBehaviour
{
    [SerializeField]
    private Text gold;

    public int Gold
    {
        set { gold.text = value.ToString(); }
    }
}
