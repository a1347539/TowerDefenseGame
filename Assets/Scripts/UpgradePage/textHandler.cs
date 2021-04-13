using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textHandler : MonoBehaviour
{
    [SerializeField]
    private Text currency;

    public int Currency { get => int.Parse(currency.text); set => currency.text = value.ToString(); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValue(float c)
    {
        Currency = (int)c;
    }
}
