using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSceneLoad_PrePage : MonoBehaviour
{
    [SerializeField]
    private GameObject loadScene;

    IEnumerator disableLoadSceneCorotine()
    {
        yield return new WaitForSeconds(1.5f);
        disableLoadScene();
    }

    private void disableLoadScene()
    {
        loadScene.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        loadScene.SetActive(true);

        StartCoroutine(disableLoadSceneCorotine());
    }
}
