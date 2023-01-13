using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Online : MonoBehaviour
{
    [SerializeField]
    private GameObject endSessionPrompt;

    public void endSession()
    {
        endSessionPrompt.SetActive(true);
    }

    public void endSessionFinal()
    {

        GameManager_Online.Instance.saveData();

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


}
