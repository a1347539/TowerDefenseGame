using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class RNG
{
    static System.Random r = new System.Random();

    public static int GetRandomNumber(int min, int max)
    {
        return r.Next(min, max+1);
    }

    public static int sigmoid(int x)
    {
        return (int)(70f / (0.3f + 10f * Mathf.Exp(-0.03f * x)));
    }

    public static float gaussian(int enemyID, int waveNumber)
    {
        return Mathf.Exp(-Mathf.Pow(0.3f * waveNumber - enemyID, 2)) + 0.0001f;
    }
}
