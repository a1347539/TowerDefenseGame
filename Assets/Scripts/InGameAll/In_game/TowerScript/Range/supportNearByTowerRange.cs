using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supportNearByTowerRange : rangeSuper
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AttackTower")
        {
            base.targetList.Add(collision.gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "AttackTower")
        {
            base.targetList.Remove(collision.gameObject);
        }
    }


}
