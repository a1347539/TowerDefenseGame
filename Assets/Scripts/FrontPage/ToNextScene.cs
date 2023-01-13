using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextScene : MonoBehaviour
{

    [SerializeField]
    private GameObject continueLastGame;

    [SerializeField]
    private GameObject notEnoughEnemy;

    [SerializeField]
    private GameObject matchMakingLoad;

    private PlayerConfig playerConfig { get { return GetComponent<PlayerDataStorage>().current_player_config; } }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerConfig.isInGame == true)
        {
            continueLastGame.SetActive(true);
        }
        else
        {
            continueLastGame.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void continueGame()
    {
        staticTransition.pathToLevel = playerConfig.userMapPath;

        staticTransition.isNewGame = false;

        SceneManager.LoadScene("InGame");
    }

    public void ToPrepPage()
    {
        SceneManager.LoadScene("PrepPage");
    }

    public void ToUpgradePage()
    {
        SceneManager.LoadScene("UpgradePage");
    }

    public void ToOnlinePage()
    {
        
        if (PlayerDataStorage.Instance.numberOfEnemy == 0)
        {
            notEnoughEnemy.SetActive(true);
            StartCoroutine(turnoffmessage());
        }
        else 
        {
            matchMakingLoad.SetActive(true);
            matchMakingLoad.GetComponent<matchMakingLoad>().startMatching();
        }

        IEnumerator turnoffmessage()
        {
            yield return new WaitForSeconds(0.5f);
            notEnoughEnemy.SetActive(false);
        }
    }

    
}
