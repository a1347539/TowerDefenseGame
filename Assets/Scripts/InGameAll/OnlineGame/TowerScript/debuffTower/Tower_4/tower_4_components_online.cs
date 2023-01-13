using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_4_components_online : MonoBehaviour
{
    [SerializeField]
    private GameObject tower_4_1, tower_4_2;

    private void Start()
    {
        int layer = GetComponent<SpriteRenderer>().sortingOrder;
        tower_4_1.GetComponent<SpriteRenderer>().sortingOrder = layer + 1;
        tower_4_2.GetComponent<SpriteRenderer>().sortingOrder = layer + 2;
    }

    private void FixedUpdate()
    {
        tower_4_1.transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);
        tower_4_2.transform.Rotate(new Vector3(0, 0, -120) * Time.deltaTime);
    }
}
