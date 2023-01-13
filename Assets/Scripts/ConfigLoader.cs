using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Net;
using System.Collections.Specialized;

public static class ConfigLoader
{
    public static T Load<T>(string fileName) where T : class
    {
        string pathPrefix = "Assets/Resources/";
        string path = pathPrefix + fileName;

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(data);
        }
        else
        {
            return null;
        }
    }

    public static T LoadTextFile<T>(string path)
    {
        TextAsset temp = (TextAsset)Resources.Load(path);

        return JsonConvert.DeserializeObject<T>(temp.ToString());
    }

    public static PlayerConfig Load(string user, string pass)
    {
        /*NameValueCollection postData = new NameValueCollection();
        postData["user"] = user;
        postData["pass"] = security.SHA256(pass);

        string response = security.webAPI("login", postData);

        if (response != "2")
        {
            return JsonConvert.DeserializeObject<PlayerConfig>(response);
        }
        else
        {
            return null;
        }
*/

        string pathPrefix = "Assets/Resources/Player/Data/";
        string path = pathPrefix + $"{user}.txt";

        string data = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<PlayerConfig>(data);
    }

    public static PlayerConfig findOpponent(string user, string pass)
    {
        NameValueCollection postData = new NameValueCollection();
        postData["user"] = user;
        postData["pass"] = security.SHA256(pass);

        string response = security.webAPI("findOpponent", postData);

        if (response != "2" && response != "8")
        {
            return JsonConvert.DeserializeObject<PlayerConfig>(response);
        }
        else
        {
            return null;
        }
    }










    public static T LoadNew<T>(string data) where T : class
    {
        return JsonConvert.DeserializeObject<T>(data);

    }


    //ConfigLoader.Load<PlayerConfig>();
    
}
