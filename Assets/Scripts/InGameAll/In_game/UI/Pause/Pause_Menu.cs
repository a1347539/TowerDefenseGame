using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : Singleton<Pause_Menu>
{

    [SerializeField]
    private GameObject returnToFrontpagePrompt;

    [SerializeField]
    private GameObject endSessionPrompt;

    private List<Tower> allTowers;

    private PlayerConfig playerConfig { get { return GameManager.Instance.player; } }

    public float Gold { get; private set; }

    public float numCapturedEnemy { get; private set; }

    public float Wave { get; private set; }

    public float Health { get; private set; }

    public Dictionary<int, int> capturedEnemy { get; private set; }


    private void Awake()
    {
        allTowers = GameManager.Instance.allTower;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getGameInfo()
    {
        Gold = GameManager.Instance.Currency;
        numCapturedEnemy = GameManager.Instance.NumOfCapturedEnemy;
        Wave = GameManager.Instance.Current_wave;
        Health = GameManager.Instance.Health;
        capturedEnemy = GameManager.Instance.capturedEnemy;
    }

    public void ToFrontPage()
    {
        returnToFrontpagePrompt.SetActive(true);
    }

    public void ToFrontPageFinal()
    {
        setTimeBack();
        playerConfig.userMapConfig = getMapConfig();
        playerConfig.userMapPath = staticTransition.pathToLevel;
        print(Gold);
        playerConfig.gameConfig = new float[] { Gold, numCapturedEnemy, Wave, Health };
        playerConfig.capturedEnemy_duringSession = capturedEnemy;

        ConfigSaver.save(staticTransition.userID, staticTransition.userPass, playerConfig);

        SceneManager.LoadScene("FrontPage");
    }

    public void endApplication(List<Tower> towers, float Gold, float numCapturedEnemy, float Wave, float Health, Dictionary<int, int> capturedEnemy)
    {
        playerConfig.userMapConfig = getMapConfig(towers);
        playerConfig.userMapPath = staticTransition.pathToLevel;
        playerConfig.gameConfig = new float[] { Gold, numCapturedEnemy, Wave, Health };
        playerConfig.capturedEnemy_duringSession = capturedEnemy;

        ConfigSaver.save(staticTransition.userID, staticTransition.userPass, playerConfig);

        SceneManager.LoadScene("FrontPage");
    }

    public void endSession()
    {
        endSessionPrompt.SetActive(true);
    }

    public void endSessionFinal()
    {

        playerConfig.isInGame = false;
        playerConfig.gold += Gold;
        addRewardToUser();
        ConfigSaver.save(staticTransition.userID, staticTransition.userPass, playerConfig);

        SceneManager.LoadScene("FrontPage");
    }

    public void setTimeBack()
    {
        Time.timeScale = 1;
    }
    public void pauseTime()
    {
        Time.timeScale = 0;
    }

    private List<float[]> getMapConfig()
    {
        List<float[]> tempArray = new List<float[]>();

        foreach (Tower tower in allTowers)
        {
            tempArray.Add(new float[] {tower.current_tile.GridPosition.x,
                                       tower.current_tile.GridPosition.y,
                                       tower.towerId,
                                       tower.level,
                                       tower.damage,
                                       tower.actualRange,
                                       tower.AtkSpeed,
                                       tower.effect
            });
        }

        return tempArray;
    }

    private List<float[]> getMapConfig(List<Tower> towers)
    {
        List<float[]> tempArray = new List<float[]>();

        foreach (Tower tower in towers)
        {
            tempArray.Add(new float[] {tower.current_tile.GridPosition.x,
                                       tower.current_tile.GridPosition.y,
                                       tower.towerId,
                                       tower.level,
                                       tower.damage,
                                       tower.actualRange,
                                       tower.AtkSpeed,
                                       tower.effect
            });
        }

        return tempArray;
    }

    private void addRewardToUser()
    {

        foreach (KeyValuePair<int, int> enemy in capturedEnemy)
        {
            if (playerConfig.allCapturedEnemy.ContainsKey(enemy.Key))
            {
                playerConfig.allCapturedEnemy[enemy.Key] += enemy.Value;
            }
            else
            {
                playerConfig.allCapturedEnemy.Add(enemy.Key, enemy.Value);
            }
        }
    }
}
