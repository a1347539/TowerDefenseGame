using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_0_online : Tower_Online
{
    [SerializeField]
    private tower_0_proj_online projectile;

    private float actionTimer;

    protected override void conditionForAction()
    {
        actionTimer -= Time.deltaTime * AtkSpeed;

        if (targetList.Count != 0)
        {
            if (actionTimer < 0)
            {
                action();
            }
        }
    }

    protected void action()
    {
        attack();
        actionTimer = 1;
    }

    protected virtual void attack()
    {
        tower_0_proj_online new_projectile = Instantiate(projectile, CenterOfTowerInWorldPosition, Quaternion.identity).GetComponent<tower_0_proj_online>();
        new_projectile.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 29;
        new_projectile.transform.SetParent(transform);
    }
}
