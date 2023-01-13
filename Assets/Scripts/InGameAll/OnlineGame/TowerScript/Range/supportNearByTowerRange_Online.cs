using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supportNearByTowerRange_Online : MonoBehaviour
{

    private List<GameObject> targetList;

    void Start()
    {
        targetList = transform.parent.gameObject.GetComponent<Tower_Online>().targetList;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AttackTower")
        {
            targetList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "AttackTower")
        {
            targetList.Remove(collision.gameObject);
        }
    }


}
