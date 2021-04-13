using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_5 : Tower
{
    [SerializeField]
    private tower_5_proj projectile;

    private tower_5_cannon cannon;

    private float actionTimer;

    protected override void Start()
    {
        cannon = transform.GetChild(0).GetChild(0).gameObject.GetComponent<tower_5_cannon>();
    }

    protected override void conditionForAction()
    {
        actionTimer -= Time.deltaTime * AtkSpeed;

        if (targetList.Count != 0)
        {
            if (actionTimer < 0)
            {
                if (cannon.CannonInPosition)
                { action(); }
            }
        }
    }

    protected void action()
    {
        attack();
        actionTimer = 1;
    }

    protected void attack()
    {
        tower_5_proj new_projectile = Instantiate(projectile, CenterOfTowerInWorldPosition, cannon.transform.rotation).GetComponent<tower_5_proj>();
        new_projectile.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 29;
        new_projectile.transform.SetParent(transform);
    }
}
