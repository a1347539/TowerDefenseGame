using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 12;

    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
        if (transform.position == destination)
        {
            Destroy(gameObject);
        }
    }
}
