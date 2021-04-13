using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner_Online : Singleton<Spawner_Online>
{

    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private Transform Enemies;

    public GameObject[] allHolders = new GameObject[20];

    [SerializeField]
    public List<Enemy_Online> EnemiesList = new List<Enemy_Online>();

    public List<Node> path;

    private GameManager_Online gameManager;

    private GameObject spawningEnemy;

    [SerializeField]
    private int currentEnemy;

    public int CurrentEnemy { get => currentEnemy; set => currentEnemy = value; }


    // Start is called before the first frame update
    void Start()
    {
        currentEnemy = -1;
        gameManager = GameManager_Online.Instance;

        Dictionary<int, int> temp = gameManager.player.allCapturedEnemy;

        foreach (KeyValuePair<int, int> enemy in temp)
        {
            gameManager.allEnemiesTemp.Add(enemy.Key, enemy.Value);
        }
    }

    public void spawnEnemy()
    {
     
        if (EnemiesList.Count < 20 && currentEnemy != -1)
        {
            int enemyAmount = gameManager.allEnemiesTemp[currentEnemy];

            enemyAmount--;

            if (enemyAmount < 1)
            {
                allHolders[currentEnemy].GetComponent<Button>().interactable = false;
            }

            if (enemyAmount >= 0)
            {
                allHolders[currentEnemy].transform.GetChild(1).GetComponent<Text>().text = enemyAmount.ToString();

                initEnemy(currentEnemy);

                gameManager.allEnemiesTemp[currentEnemy]--;
                
            }
        }
        
    }

    private void initEnemy(int i)
    {
        spawningEnemy = enemyPrefabs[i];
        Enemy_Online new_enemy = Instantiate(spawningEnemy).GetComponent<Enemy_Online>();
        new_enemy.Setup(Enemies, currentEnemy.ToString(), path);
        EnemiesList.Add(new_enemy);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
