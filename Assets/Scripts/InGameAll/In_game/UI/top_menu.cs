using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class top_menu: MonoBehaviour
{
    public Text currency, capturedEnemy, wave, health;

    public int currencyTxt
    {
        set
        {
            currency.text = value.ToString();
        }
    }
     
    public int waveTxt
    {
        set 
        {
            wave.text = value.ToString();
        }
    }

    public int healthTxt
    {
        set
        {
            health.text = value.ToString();
        }
    }

    public int capturedEnemyTxt
    {
        set 
        {
            capturedEnemy.text = value.ToString();
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
