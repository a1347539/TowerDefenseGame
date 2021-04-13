using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_2_proj : MonoBehaviour
{
    private GameObject target;
    private Tower parent_tower;
    private GameManager gameManager;

    //to be set
    private int speed { get { return 5 * gameManager.IsPausedInt; } }

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        parent_tower = transform.parent.gameObject.GetComponent<Tower>();
        target = parent_tower.targetList[0];

    }

    private void FixedUpdate()
    {
        if (target != null)
        { move(); }
        else 
        { Destroy(this.gameObject); }
    }

    private void move()
    {
        Vector2 distance = target.transform.position - transform.position;

        float degree = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(Vector3.forward * degree);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject == target.gameObject)
            {
                Destroy(this.gameObject);

                target.GetComponent<Enemy>().OnCollision(parent_tower.damage);
            }
        }
    }
}
