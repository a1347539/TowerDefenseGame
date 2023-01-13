using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    [SerializeField]
    private Text username, currency;

    public string Username
    {
        set { username.text = value; }
    }

    public int Currency
    {
        set { currency.text = value.ToString(); }
    }

    public void setValue(string u, float c)
    {
        Username = u;
        Currency = (int)c;
    }
}
