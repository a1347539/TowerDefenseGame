using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition_UpgradePage : MonoBehaviour
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
