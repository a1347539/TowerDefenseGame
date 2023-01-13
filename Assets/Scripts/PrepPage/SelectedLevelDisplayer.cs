using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectedLevelDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject level_image;

    [SerializeField]
    private Text LevelNumber;

    private PlayerConfig playerConfig;

    [SerializeField]
    private GameObject isGameStartedPrompt;

    private string path_prefix = "Level/";

    private string path_suffix = ".txt";

    private string path;

    private string levelNum
    {
        set 
        {
            LevelNumber.text = value;
        }
    }

    private void Awake()
    {
        playerConfig = ConfigLoader.Load(staticTransition.userID, staticTransition.userPass);
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
    }
    public void getLevelInfo(GameObject button)
    {
        path = path_prefix + button.name;
        //consoleLog.write(path);

        LevelConfig config = ConfigLoader.LoadTextFile<LevelConfig>(path);

        //LevelConfig config = ConfigLoader.Load<LevelConfig>(temp.text);
        //consoleLog.write(config.GetType().ToString());

        levelNum = config.LevelNumber;

        level_image.GetComponent<Image>().sprite = button.GetComponent<Image>().sprite;

        gameObject.SetActive(true);
    }

    public void ToInGame()
    {
        if (playerConfig.isInGame == false)
        {
            playerConfig.isInGame = true;
            playerConfig.userMapPath = staticTransition.pathToLevel;
            playerConfig.gameConfig = new float[] { 10, 0, 0, 10 };


            ConfigSaver.save(staticTransition.userID, staticTransition.userPass, playerConfig);

            staticTransition.pathToLevel = path;
            gameObject.SetActive(false);

            staticTransition.isNewGame = true;

            SceneManager.LoadScene("InGame");
        }
        else
        {
            isGameStartedPrompt.SetActive(true);
        }
    }

    public void continueToNewGame()
    {
        playerConfig.isInGame = false;
        playerConfig.userMapPath = null;
        playerConfig.userMapConfig = null;
        playerConfig.gameConfig = null;

        ConfigSaver.save(staticTransition.userID, staticTransition.userPass, playerConfig);

        ToInGame();
    }
}
