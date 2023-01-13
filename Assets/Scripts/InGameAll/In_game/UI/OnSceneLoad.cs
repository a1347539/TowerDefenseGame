using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSceneLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject loadScene;

    [SerializeField]
    private Text levelNumber;

    private void Awake()
    { 
        
    }

    IEnumerator disableLoadSceneCorotine()
    {
        yield return new WaitForSeconds(3f);
        disableLoadScene();
    }

    private void disableLoadScene()
    {
        loadScene.SetActive(false);
    }

    private string levelNum 
    { 
        set
        {
            levelNumber.text = "Level: " + value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loadScene.SetActive(true);

        levelNum = LevelManager.Instance.config.LevelNumber;
        StartCoroutine(disableLoadSceneCorotine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
