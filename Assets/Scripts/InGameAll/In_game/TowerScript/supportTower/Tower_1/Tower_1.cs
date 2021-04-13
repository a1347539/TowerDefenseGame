using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_1 : Tower
{
    private bool levelChanged = true;

    private int numberOfTarget = 0;

    private int currentNumberOfTarget { get { return targetList.Count; } }

    private float previousEffect = 0;

    public override int setLevel { 
        set 
        { 
            base.setLevel = value;
            levelChanged = true;
        } 
    }

    public override void upgrading()
    {
        levelChanged = true;
        base.upgrading();
    }


    protected override void conditionForAction()
    {
        if (levelChanged)
        {
            action();
            levelChanged = false;
            numberOfTarget = currentNumberOfTarget;
        }
        else if (numberOfTarget != currentNumberOfTarget)
        {
            supportForNewTower();
            numberOfTarget = currentNumberOfTarget;
        }
    }

    protected void action()
    {
        support();
    }

    protected void support()
    {
        
        foreach (GameObject tower in targetList)
        {
            //support of damage
            tower.GetComponent<Tower>().incrementFromSupportTower[1] -= previousEffect;
            tower.GetComponent<Tower>().incrementFromSupportTower[1] += effect / 100;
            tower.GetComponent<Tower>().getNewStateFromSupport();

        }
        
        previousEffect = effect / 100;

    }

    protected void supportForNewTower()
    {

        targetList[targetList.Count - 1].GetComponent<Tower>().incrementFromSupportTower[1] += previousEffect;

    }
}
