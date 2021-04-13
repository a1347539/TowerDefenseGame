using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class matchMaking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getMatch()
    {
        /*
         match making
        return opponent json => abc
        opponentConfig = configLoader.load(abc);
        staticTansition.pathToOpponentConfig
        */

        PlayerConfig temp = ConfigLoader.findOpponent(staticTransition.userID, staticTransition.userPass);

        if (temp == null)
        {
            return false;
        }

        staticTransition.setOpponentConfig(temp);

        SceneManager.LoadScene("OnlineGame");

        return true;
    }
}
