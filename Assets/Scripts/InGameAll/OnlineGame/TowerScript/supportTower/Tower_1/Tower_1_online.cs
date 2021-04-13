using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_1_online : Tower_Online
{
    private bool levelChanged = true;

    private int numberOfTarget = 0;

    private int currentNumberOfTarget { get { return targetList.Count; } }

    private float previousEffect = 0;

    public override int setLevel
    {
        set
        {
            base.setLevel = value;
            levelChanged = true;
        }
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

    }

    protected void supportForNewTower()
    {
        targetList[targetList.Count - 1].GetComponent<Tower_Online>().incrementFromSupportTower[1] += previousEffect;
    }
}
