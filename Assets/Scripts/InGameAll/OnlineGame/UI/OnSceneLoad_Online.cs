using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSceneLoad_Online : MonoBehaviour
{
    [SerializeField]
    private GameObject loadScene;

    [SerializeField]
    private Text opponentInfo;

    IEnumerator disableLoadSceneCorotine()
    {
        yield return new WaitForSeconds(3f);
        disableLoadScene();
    }

    private void disableLoadScene()
    {
        loadScene.SetActive(false);
    }

    private string opponentI
    {
        set
        {
            opponentInfo.text = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loadScene.SetActive(true);

        opponentI = "Opponent: " + GameManager_Online.Instance.opponent.Username + "\nLevel: " + LevelManager_Online.Instance.config.LevelNumber;
        StartCoroutine(disableLoadSceneCorotine());
    }

}
