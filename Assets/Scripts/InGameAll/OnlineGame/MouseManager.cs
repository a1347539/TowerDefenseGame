using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{

    private void FixedUpdate()
    {

        

    }

    public bool clicked(GameObject obj)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.gameObject == obj)
            {
                return true;
            }
        }            

        return false;
    }
}