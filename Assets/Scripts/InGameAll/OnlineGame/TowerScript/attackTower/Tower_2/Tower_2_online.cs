using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_2_online : Tower_Online
{
    [SerializeField]
    private tower_2_proj_online projectile;

    private tower_2_cannon_online cannon;

    private float actionTimer;

    protected override void Start()
    {
        cannon = transform.GetChild(0).GetChild(0).gameObject.GetComponent<tower_2_cannon_online>();
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
        tower_2_proj_online new_projectile = Instantiate(projectile, CenterOfTowerInWorldPosition, Quaternion.identity).GetComponent<tower_2_proj_online>();
        new_projectile.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 29;
        new_projectile.transform.SetParent(transform);
    }
}
