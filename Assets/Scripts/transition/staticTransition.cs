using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class staticTransition
{
    public static string pathToLevel = "Level/Level_6";

    //public static string pathToLevel;

    public static string userID = "234";

    public static string userPass = "234";

    //public static string userID { get; private set; }

    //public static string userPass { get; private set; }

    public static PlayerConfig opponentConfig { get; private set; }

    public static bool isNewGame;

    public static void setUserID(string ID)
    {
        userID = ID; 
    }

    public static void setUserPass(string pass)
    {
        userPass = pass;
    }

    public static void setOpponentConfig(PlayerConfig config)
    {
        opponentConfig = config;
    }
}
