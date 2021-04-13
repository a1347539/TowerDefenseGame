using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageDisplay : MonoBehaviour
{
    [SerializeField]
    private Text damage;

    private int d
    {
        set 
        {
            damage.text = value.ToString();
        }
    }
    private void Start()
    {
        
    }

    public void setup(Transform transform, float damage)
    {
        gameObject.transform.SetParent(transform);
        d = (int)damage;
    }
}
