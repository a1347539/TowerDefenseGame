using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathChanger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(List<Enemy> enemies)
    {
        StartCoroutine(changeEnemyPath(enemies));
    }

    IEnumerator changeEnemyPath(List<Enemy> enemies)
    {
        foreach (Enemy enemy in new List<Enemy>(enemies))
        {
            //enemy.isMapChanged = true;
            if (enemy != null)
            {
                enemy.getNewPath();
            }


            yield return new WaitForSeconds(0.005f);
        }
        GameManager.Instance.pathChangerList.Remove(this);
        Destroy(this.gameObject);
    }
}
