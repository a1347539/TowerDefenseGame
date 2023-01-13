using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_3_proj : MonoBehaviour
{
    private ParticleSystem particle;

    private GameObject target;
    private Tower parent_tower;
    public Tower Parent_tower { get { return parent_tower; } }
    private GameManager gameManager;
    private Vector3 targetFinalPosition;
    private bool inDestination = false;
    private bool alreadyHit = false;

    private Color tempColor;

    //to be set
    private int speed { get { return 6 * gameManager.IsPausedInt; } }

    private void Awake()
    {
        gameManager = GameManager.Instance;

        particle = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();

        transform.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        parent_tower = transform.parent.gameObject.GetComponent<Tower>();
        particle.Pause();
        parent_tower = transform.parent.gameObject.GetComponent<Tower>();
        target = parent_tower.targetList[0];
        targetFinalPosition = target.transform.position;

        Color tempColor = GetComponent<SpriteRenderer>().color;
    }

    private void FixedUpdate()
    {
        if (target != null && transform.position != targetFinalPosition)
        { move(); }
        else
        {
            StartCoroutine(destroyObject());
        }
    }

    private void move()
    {
        if (!alreadyHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetFinalPosition, speed * Time.deltaTime);

            if (transform.position == targetFinalPosition)
            {
                didNotHitTarget();
            }
        }
    }

    private void didNotHitTarget()
    {
        inDestination = true;
        toTransparent();
        getParticle();
    }

    private void hitTarget(Collider2D collision)
    {
        try
        {
            collision.GetComponent<Enemy>().OnCollision(parent_tower.damage);
        }
        catch (Exception e)
        {

        }
        finally
        {
            toTransparent();
            getParticle();
        }
    }

    private void toTransparent()
    {
        tempColor.a = 0f;
        GetComponent<SpriteRenderer>().color = tempColor;
    }

    private void getParticle()
    {
        particle.Play();
        StartCoroutine(destroyObject());
    }

    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.tag == "GroundEnemy" && !inDestination)
            {
                alreadyHit = true;

                hitTarget(collision);
            }
        }
    }
}
