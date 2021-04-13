using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    [SerializeField]
    private GameObject proj1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space key was pressed");
            getProj1();
        }
    }

    private void getProj1()
    {
        Instantiate(proj1, new Vector2(0, 0), Quaternion.identity);
    }
}
