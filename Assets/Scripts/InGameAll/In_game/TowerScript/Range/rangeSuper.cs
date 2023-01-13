using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeSuper : MonoBehaviour
{
    protected List<GameObject> targetList;

    void Start()
    {
        targetList = transform.parent.gameObject.GetComponent<Tower>().targetList;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {

    }
}
