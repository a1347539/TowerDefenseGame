using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_4 : Tower
{
    private ParticleSystem particle;

    private float actionTimer;

    private List<GameObject> isDebuffed = new List<GameObject>();

    private int targetNum { get { return targetList.Count; } }

    protected override void Start()
    {
        transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        particle.Pause();
    }

    protected override void conditionForAction()
    {
        actionTimer -= Time.deltaTime * AtkSpeed;
        if (targetList.Count != 0)
        {
            if (actionTimer < 0)
            {
                particle.Play();
                action();
            }
        }
    }

    protected void action()
    {
        debuff();
        actionTimer = 1;
    }

    protected void debuff()
    {
        for (int i = 0; i < targetNum; i++)
        {
            if (targetList[i] == null)
            {
                targetList.RemoveAt(i);
                continue;
            }

            Enemy enemy = targetList[i].GetComponent<Enemy>();
            if (!enemy.isSlowed)
            {
                StartCoroutine(becomeSlower(enemy));
            }
        }
    }

    IEnumerator becomeSlower(Enemy enemy)
    {
        enemy.SpeedDebuff = 0.3f;
        enemy.isSlowed = true;
        yield return new WaitForSeconds(1f);
        if (enemy != null)
        {
            enemy.SpeedDebuff = 1f;
            enemy.isSlowed = false;
        }
    }
}
