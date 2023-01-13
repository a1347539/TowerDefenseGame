using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    [SerializeField]
    private Text IDField;

    [SerializeField]
    private InputField PWField;

    [SerializeField]
    private GameObject Loading, successInput, unsuccessInput, signupPanel;

    private string path = "Assets/Resources/Player/AllPlayers.txt";

    private IEnumerator searchingCorotine, ExitCorotine, checkStatusCorotine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void signUp()
    {
        Loading.SetActive(true);
        /*register(IDField.text, PWField.text); */
        
        searchingCorotine = search();
        StartCoroutine(searchingCorotine); 
    }

    private IEnumerator search()
    {
        string[] lines = File.ReadAllLines(path);

        if (IDField.text == "")
        {
            unsuccessInput.SetActive(true);
            Loading.SetActive(false);
            yield break;
        }

        foreach (string line in lines)
        {
            if (IDField.text == line)
            {
                unsuccessInput.SetActive(true);
                Loading.SetActive(false);
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        successInput.SetActive(true);
        Loading.SetActive(false);
        register(IDField.text);
    }

    private void register(string ID)
    {
        File.AppendAllText(path, Environment.NewLine + ID);

        playerInit.playerInitiation(IDField.text, PWField.text);

        ExitCorotine = Exit();
        StartCoroutine(ExitCorotine);
    }

    private void register(string user, string pass)
    {
        checkStatusCorotine = checkStatus(user, pass); 
        StartCoroutine(checkStatusCorotine); 
    }

    private IEnumerator checkStatus(string user, string pass)
    {
        if (IDField.text == "" || PWField.text == "")
        {
            unsuccessInput.SetActive(true);
            Loading.SetActive(false);
            yield break;
        }

        yield return new WaitForSeconds(2f);


        NameValueCollection postData = new NameValueCollection();
        postData["user"] = user;
        postData["pass"] = security.SHA256(pass);

        string json = security.webAPI("signup", postData);

        if (json == "6")
        {
            successInput.SetActive(true);
            Loading.SetActive(false);

            ExitCorotine = Exit();
            StartCoroutine(ExitCorotine);
        }
        else
        {
            unsuccessInput.SetActive(true);
            Loading.SetActive(false);
        }
    }

    private IEnumerator Exit()
    {
        yield return new WaitForSeconds(2f);
        exitRegisterMenu();
    }

    public void CancelRegister()
    {
        if (checkStatusCorotine != null)
        { StopCoroutine(checkStatusCorotine); }
        Loading.SetActive(false);
    }

    public void exitRegisterMenu()
    {
        if (checkStatusCorotine != null)
        { StopCoroutine(checkStatusCorotine); }
        if (ExitCorotine != null)
        { StopCoroutine(ExitCorotine); }

        Loading.SetActive(false);
        successInput.SetActive(false);
        unsuccessInput.SetActive(false);
        signupPanel.SetActive(false);
    }
}
