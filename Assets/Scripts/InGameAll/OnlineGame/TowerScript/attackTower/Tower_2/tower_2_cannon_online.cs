using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_2_cannon_online : MonoBehaviour
{
    private List<GameObject> allTarget { get { return parent_tower.targetList; } }
    private Tower_Online parent_tower;
    private GameManager_Online gameManager;
    private GameObject target;
    private bool cannonInPosition;

    public bool CannonInPosition { get { return cannonInPosition; } }

    float degree = 0;

    private GameObject parent;

    private void Awake()
    {
        cannonInPosition = false;
        gameManager = GameManager_Online.Instance;
        parent_tower = transform.parent.parent.gameObject.GetComponent<Tower_Online>();
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = parent_tower.GetComponent<Tower_Online>().sortingOrder + 30;
    }



    void FixedUpdate()
    {
        if (allTarget.Count != 0)
        {
            rotate();
        }
        else
        {
            cannonInPosition = false;
            degree = 0;
        }
    }

    private void rotate()
    {
        target = allTarget[0];

        if (target == null)
        {
            allTarget.RemoveAt(0);
            return;
        }

        Quaternion origin = transform.rotation;

        Vector2 distance = target.transform.position - transform.position;

        Quaternion degree = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + 90));

        if (gameManager.IsPausedInt != 0)
        {
            transform.rotation = Quaternion.Lerp(origin, degree, 0.1f);
            if (Mathf.Abs(degree.eulerAngles.z - origin.eulerAngles.z) < 10)
            {
                cannonInPosition = true;
            }
        }
    }
}
