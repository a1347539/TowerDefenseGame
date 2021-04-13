using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundRange : rangeSuper
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GroundEnemy")
        {
            base.targetList.Add(collision.gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GroundEnemy")
        {
            base.targetList.Remove(collision.gameObject);
        }
    }
}
