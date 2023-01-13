using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField]
    private Text IDField;

    [SerializeField]
    private InputField PWField;

    [SerializeField]
    private GameObject Loading, correctInput, wrongInput;

    private string path = "Assets/Resources/Player/AllPlayers.txt";

    private string configPathPrefix = "Player/Data/";

    private string configPathSuffix = ".txt";

    private string configPath;

    private IEnumerator searchingCorotine, acceptedCorotine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void login()
    {
        Loading.SetActive(true);
        searchingCorotine = search();
        StartCoroutine(searchingCorotine);
    }

    public IEnumerator search()
    {
        if (IDField.text == "")
        {
            wrongInput.SetActive(true);
            Loading.SetActive(false);
            yield break;
        }

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        {
            if (IDField.text == line)
            {
                checkPassword(IDField.text);
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }
        wrongInput.SetActive(true);
        Loading.SetActive(false);
    }

    public IEnumerator search(string user, string pass)
    {
        if (IDField.text == "" || PWField.text == "")
        {
            wrongInput.SetActive(true);
            Loading.SetActive(false);
            yield break;
        }

        yield return new WaitForSeconds(1f);

        if (checkPassword(user, pass))
        {
            yield break; 
        }
    }

    private void checkPassword(string username)
    {
        configPath = configPathPrefix + username + configPathSuffix;

        PlayerConfig PlayerData = ConfigLoader.Load<PlayerConfig>(configPath);

        if (PWField.text == PlayerData.Password)
        {
            correctInput.SetActive(true);
            Loading.SetActive(false);
            acceptedCorotine = accepted();
            StartCoroutine(acceptedCorotine);
            return;
        }
        wrongInput.SetActive(true);
        Loading.SetActive(false);
    }

    private bool checkPassword(string user, string pass) 
    { 

        NameValueCollection postData = new NameValueCollection();
        postData["user"] = user;
        postData["pass"] = security.SHA256(pass);

        string json = security.webAPI("login", postData); 

        if (json != "1")
        {
            correctInput.SetActive(true);
            Loading.SetActive(false);
            acceptedCorotine = accepted(user, pass);
            StartCoroutine(acceptedCorotine);

            return true;
        }
        else 
        {
            wrongInput.SetActive(true);
            Loading.SetActive(false);
            
            return false; 
        }
    }

    private IEnumerator accepted()
    {
        yield return new WaitForSeconds(2f);
        correctInput.SetActive(false);
        toFrontPage(IDField.text, PWField.text);
    }

    private IEnumerator accepted(string user, string pass)
    {
        yield return new WaitForSeconds(2f);
        correctInput.SetActive(false);
        toFrontPage(user, pass);
    }

    private void toFrontPage(string user, string pass)
    {
        staticTransition.setUserID(user); 
        staticTransition.setUserPass(pass); 
        SceneManager.LoadScene("FrontPage");
    }

    public void CancelLogin()
    {
        StopCoroutine(searchingCorotine);
        Loading.SetActive(false);
    }

    public void CancelEnter()
    {
        StopCoroutine(acceptedCorotine);
        correctInput.SetActive(false);
    }
}

