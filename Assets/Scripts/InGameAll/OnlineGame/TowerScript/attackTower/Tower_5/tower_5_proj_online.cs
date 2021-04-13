using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_5_proj_online : MonoBehaviour
{
    private ParticleSystem particle;

    private GameObject target;
    private Tower_Online parent_tower;
    public Tower_Online Parent_tower { get { return parent_tower; } }
    private GameManager_Online gameManager;
    private Vector3 targetFinalPosition;
    private float scalar;
    private Vector3 destination;

    private int speed { get { return 12 * gameManager.IsPausedInt; } }

    private void Awake()
    {
        gameManager = GameManager_Online.Instance;

        particle = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();

        transform.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;

    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            parent_tower = transform.parent.gameObject.GetComponent<Tower_Online>();
            target = parent_tower.targetList[0];
            targetFinalPosition = target.transform.position;
            Vector3 temp = (targetFinalPosition - transform.position).normalized;

            destination = target.transform.position + new Vector3(temp.x * parent_tower.actualRange / 4, temp.y * parent_tower.actualRange / 4);
        }
        catch (Exception e)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (transform.position == destination)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.tag == "GroundEnemy")
            {
                collision.GetComponent<Enemy_Online>().OnCollision(parent_tower.damage);
            }
        }
    }
}
