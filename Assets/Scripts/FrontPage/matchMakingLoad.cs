using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class matchMakingLoad : MonoBehaviour
{
    [SerializeField]
    private matchMaking matchMaking;

    [SerializeField]
    private GameObject NoOpponentError;


    public void startMatching()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        StartCoroutine(waitForMatch());
    }

    IEnumerator waitForMatch()
    {
        yield return new WaitForSeconds(1f);

        if (matchMaking.getMatch() == false)
        {
            NoOpponentError.SetActive(true);

            StartCoroutine(turnoffmessage());

            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator turnoffmessage()
    {
        yield return new WaitForSeconds(1f);
        NoOpponentError.SetActive(false);
    }

}
