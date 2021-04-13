using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class stateCalculator
{
    public static float baseStat(float stat, float generalUpgrade, float towerUpgrade, float rate, float level)
    {
        return stat * Mathf.Pow((1 + rate / 360), (360 * level)) * (1 + generalUpgrade) * (1 + towerUpgrade);
    }

    public static float upgradeStat(float stat, float rate, float level)
    {
        return stat * Mathf.Pow((1 + rate / 360), (360 * level));
    }

    public static float d_stat(float level)
    {
        return 0;
    }
}
