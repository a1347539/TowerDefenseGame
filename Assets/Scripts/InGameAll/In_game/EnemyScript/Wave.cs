using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public Dictionary<int, float> individualOutput = new Dictionary<int, float>();

    private Spawner spawner;

    private int enemyPrefabLength;

    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void initWave(int currentWaveNumber, int enemyPrefabLength, GameObject waveFolder, Spawner spawner)
    {
        this.enemyPrefabLength = enemyPrefabLength;
        this.transform.SetParent(waveFolder.transform);
        this.spawner = spawner;
        spawnNormallyCoroutine(currentWaveNumber);
    }

    private void spawnNormallyCoroutine(int currentWaveNumber)
    {

        float allEnemyOutputSum = 0;

        float temp;

        for (int j = 0; j < enemyPrefabLength; j++)
        {
            temp = RNG.gaussian(j, currentWaveNumber - 1);

            if (individualOutput.ContainsKey(j))
            {
                individualOutput[j] = temp;
            }
            else
            {
                individualOutput.Add(j, temp);
            }

            allEnemyOutputSum += temp;
        }

        StartCoroutine(spawnNormally(currentWaveNumber, allEnemyOutputSum));
    }



    private IEnumerator spawnNormally(int currentWaveNumber, float allEnemyOutputSum)
    {

        for (int i = 0; i < RNG.sigmoid(currentWaveNumber); i++)
        {
            int enemyID = 0;

            float individualRate;

            float normal_r = new System.Random().Next(100) / 100f;

            float iterSum = 0;

            foreach (KeyValuePair<int, float> item in individualOutput)
            {
                individualRate = item.Value / allEnemyOutputSum;

                //print(normal_r + " <? " + " " + individualRate + " + " + iterSum + " = " + (individualRate + iterSum));

                if (normal_r <= individualRate + iterSum)
                {
                    enemyID = item.Key;
                    //print("---------------------------------- spawn: " + enemyID + "----------------------------------");
                    break;
                }
                iterSum += individualRate;
                
            }

            spawner.spawnEnemy(enemyID);

            yield return new WaitForSeconds(1);
        }
        Destroy(this.gameObject);

    }
}
