using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endSessionMenu_Online : MonoBehaviour
{
    [SerializeField]
    private Text goldEarn;

    [SerializeField]
    private GameObject button;

    private string gold { set { goldEarn.text = value; } }

    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {

        gold = GameManager_Online.Instance.GoldLooted.ToString();

    }

    private void Update()
    {
        StartCoroutine(showButton());
    }

    IEnumerator showButton()
    {
        yield return new WaitForSecondsRealtime(2f);
        button.SetActive(true);
    }
}
