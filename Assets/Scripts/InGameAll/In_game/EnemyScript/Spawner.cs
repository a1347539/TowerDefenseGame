using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private Transform Enemies;

    [SerializeField]
    public List<Enemy> EnemiesList = new List<Enemy>();

    [SerializeField]
    private GameObject waveForSpawning, waveFolder;
    
    public List<Node> path;

    private Button NextWaveButton;

    private sideMenu sidemenu;

    private GameObject spawningEnemy;

    private float interval;

    private int isGamePause { get { return GameManager.Instance.IsPausedInt; } }

    private Dictionary<string, string[]> bossWaves = new Dictionary<string, string[]>();

    // Start is called before the first frame update
    void Start()
    {
        NextWaveButton = GameObject.Find("/Canvas/sup_menu/NextWave").GetComponent<Button>();
        sidemenu = GameObject.Find("Canvas/sideMenu").GetComponent<sideMenu>();
    }

    public void getBossWave(string[] allWave)
    {

        for (int i = 0; i < allWave.Length; i++)
        {

            string[] current_wave = allWave[i].Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            bossWaves.Add(current_wave[0], current_wave.Skip(1).ToArray());
        }
    }

    public void SpawnWave(int currentWaveNumber)
    {
        if (bossWaves.ContainsKey(currentWaveNumber.ToString()))
        {
            StartCoroutine(spawnBossWave(bossWaves[currentWaveNumber.ToString()]));
        }
        else 
        {
            Wave wave = Instantiate(waveForSpawning).GetComponent<Wave>();
            wave.initWave(currentWaveNumber, enemyPrefabs.Length, waveFolder, this);
        }
    }

    public void spawnEnemy(int enemyID)
    {
        Enemy new_enemy = Instantiate(enemyPrefabs[enemyID]).GetComponent<Enemy>();

        new_enemy.Setup(Enemies, enemyID.ToString(), path);

        EnemiesList.Add(new_enemy);
        GameManager.Instance.enemyList.Add(new_enemy);

        sidemenu.EnemiesList = EnemiesList;

        if (EnemiesList.Count > 20)
        {
            NextWaveButton.interactable = false;
        }
        else if (!NextWaveButton.interactable)
        {
            NextWaveButton.interactable = true;
        }
    }

    private IEnumerator spawnBossWave(string[] current_wave)
    {
        Dictionary<string, string> current_wave_in_dict = current_wave.Select(item => item.Split('=')).ToDictionary(s => s[0], s => s[1]);

        interval = float.Parse(current_wave_in_dict["interval"], CultureInfo.InvariantCulture.NumberFormat);

        foreach (KeyValuePair<string, string> current in current_wave_in_dict)
        {
            if (current.Key == "interval")
            { break; }
            for (int i = 0; i < int.Parse(current.Value); i++)
            {
                
                if (isGamePause == 0)
                {
                    if (i != 0) { i--; }
                    yield return null;
                }
                else
                {
                    if (spawningEnemy != null)
                    {
                        foreach (GameObject enemy in enemyPrefabs)
                        {
                            if (enemy.name == current.Key)
                            {
                                spawningEnemy = enemy;
                                break;
                            }
                        }
                        Enemy new_enemy = Instantiate(spawningEnemy).GetComponent<Enemy>();
                        new_enemy.Setup(Enemies, current.Key, path);
                        EnemiesList.Add(new_enemy);
                        GameManager.Instance.enemyList.Add(new_enemy);

                        sidemenu.EnemiesList = EnemiesList;
                    }

                   
                    if (EnemiesList.Count > 20)
                    {
                        NextWaveButton.interactable = false;
                    }


                    yield return new WaitForSeconds(interval);
                }
            }
        }
    }
}