using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundRange_Online : MonoBehaviour
{
    private List<GameObject> targetList;

    // Start is called before the first frame update
    void Start()
    {
        targetList = transform.parent.gameObject.GetComponent<Tower_Online>().targetList;
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GroundEnemy")
        {
            targetList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GroundEnemy")
        {
            targetList.Remove(collision.gameObject); 
        }
    }
}
