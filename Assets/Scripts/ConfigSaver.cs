using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using UnityEditor;

public static class ConfigSaver
{
    private static string configPathPrefix = "Assets/Resources/";

    private static string configPathSuffix = ".txt";

    public static void save<T>(string filePath, T config)
    {
        string saveFile = configPathPrefix + filePath;

        string output = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(saveFile, output);
    }

    public static void saveTextFile<T>(string filePath, T config)
    {
        TextAsset temp = (TextAsset)Resources.Load(filePath);

        string output = JsonConvert.SerializeObject(config, Formatting.Indented);

        File.WriteAllText(temp.text, output);
    }

    public static bool save(string user, string pass, PlayerConfig config)
    {
        /*NameValueCollection postData = new NameValueCollection();
        postData["user"] = user;
        postData["pass"] = security.SHA256(pass);
        postData["playerData"] = JsonConvert.SerializeObject(config);
        postData["playerData_hash"] = security.SHA256(config);

        string response = security.webAPI("save", postData);

        Debug.Log(response);

        if (response == "3")
        {
            Debug.Log("Player: Save successful");
            return true;
        }
        else
        {
            //Debug.Log("Player: Save NOT successful");
            return false;
        }*/

        string pathPrefix = "Assets/Resources/Player/Data/";
        string saveFile = pathPrefix + $"{user}.txt";

        string output = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(saveFile, output);
        return true;
    }

    public static bool saveOpponent(string user, string passHash, PlayerConfig playerConfig, PlayerConfig opponentConfig)
    {
        WebClient connection = new WebClient();

        NameValueCollection postData = new NameValueCollection();
        postData["user"] = user;
        postData["pass"] = security.SHA256(passHash);
        postData["playerData"] = JsonConvert.SerializeObject(playerConfig);
        postData["opponentData"] = JsonConvert.SerializeObject(opponentConfig);
        postData["opponentData_hash"] = security.SHA256(opponentConfig);

        string response = security.webAPI("saveOpponent", postData);

        if (response == "3")
        {
            Debug.Log("Opponent: Save successful");
            return true;
        }
        else
        {
            Debug.Log("Opponent: Save NOT successful");
            return false;
        }
    }
}
