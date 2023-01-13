using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntrance : MonoBehaviour
{


    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MouseManager.Instance.clicked(this.gameObject))
            {
                Spawner_Online.Instance.spawnEnemy();
            }
            
        }
    }

}
