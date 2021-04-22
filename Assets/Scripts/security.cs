using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class security
{
    private static string ticket;
    private static string webAPI_key = "vGKv5w24STzTnS8smwMxwcG7mzSS4vDLeytqWt5RkyBGYpV8jTWHHryJzg6U5PtXPQnstVG263jBvTBHqsBMEkSRDB4ErVCVJWyWHG8yvCqN3SbsRLt3Ar5XTx8SaMVJ";
    private static string webAPI_endpoint = "https://fyp.kn021.xyz/2.0/";
    private static WebClient webAPI_connection = new WebClient();

    public static string SHA256(string text)
    {
        using (SHA256Managed method = new SHA256Managed())
        {
            byte[] utfData = Encoding.UTF8.GetBytes(text);
            byte[] shaData = method.ComputeHash(utfData);
            return BitConverter.ToString(shaData).Replace("-", String.Empty);
        }
    }
    public static string SHA256(PlayerConfig playerConfig)
    {
        PlayerConfig_Extra playerConfigExtra = randomlizeConfig(playerConfig);
        string hashFirst = JsonConvert.SerializeObject(playerConfigExtra);
        Debug.Log(hashFirst);
        string hashSecond = SHA256(hashFirst);

        return hashSecond; 
    }

    public static string webAPI(string action, NameValueCollection postData)
    {
        checkTicket(); 

        postData["key"] = webAPI_key; 
        postData["ticket"] = ticket;
        postData["action"] = action;

        string response = Encoding.Default.GetString(webAPI_connection.UploadValues(webAPI_endpoint, "POST", postData));

        return response; 
    }

    private static void getTicket()
    {
        NameValueCollection postData = new NameValueCollection();
        postData["key"] = webAPI_key;
        postData["action"] = "getTicket"; 

        ticket = Encoding.Default.GetString(webAPI_connection.UploadValues(webAPI_endpoint, "POST", postData));
    }

    private static void checkTicket()
    {
        //If no ticket
        if (String.IsNullOrEmpty(ticket)) 
        { 
            getTicket(); 
        } 
        else 
        {
            //If ticket is invalid
            NameValueCollection postData = new NameValueCollection();
            postData["key"] = webAPI_key;
            postData["ticket"] = ticket;
            postData["action"] = "checkTicket";

            string result = Encoding.Default.GetString(webAPI_connection.UploadValues(webAPI_endpoint, "POST", postData));

            if (result == "15")
            {
                getTicket();
            }
        }
    }

    private static PlayerConfig_Extra randomlizeConfig(PlayerConfig playerConfig)
    {
        string playerConfigString = JsonConvert.SerializeObject(playerConfig);
        PlayerConfig_Extra playerConfigExtra = JsonConvert.DeserializeObject<PlayerConfig_Extra>(playerConfigString); 
        playerConfigExtra.API_key = webAPI_key;
        playerConfigExtra.ticket = ticket;
        return playerConfigExtra;
    }
}

internal class PlayerConfig_Extra : PlayerConfig
{
    public string API_key;
    public string secret = "GB7GS7nDaZdULUZpwvV3vzUabkjUC5DggrAJm6vKnZVFh9dxgSThk5AqHcQRnxtvswWGUcu2eRSe9c2APLkBM3thnsLxQ7FzvAZezJTFeUW55ratUnnuxuJTGRSGHt7b";
    public string time = DateTime.Now.ToString("dd/MM/yy HH:mm");
    public string ticket;
}