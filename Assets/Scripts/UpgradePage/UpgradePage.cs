using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradePage : Singleton<UpgradePage>
{

    //[SerializeField]
    //public string playerConfigPath;

    //public string playerConfigPath { get { return "Player/Data/234.txt"; } }

    public PlayerConfig playerConfig { get; private set; }


    [SerializeField]
    public GameObject[] allTrees;

    [SerializeField]
    private string treeConfigPath;

    public Dictionary<int, float[][]> treesConfig { get; private set; }

    private void Awake()
    {
        buildTree.build();

        playerConfig = ConfigLoader.Load(staticTransition.userID, staticTransition.userPass);

        treesConfig = ConfigLoader.LoadTextFile<TreeConfig>(treeConfigPath).trees;
    }

    private void Start()
    {
        GetComponent<textHandler>().setValue(playerConfig.gold);

        for (int tree = 0; tree < allTrees.Length; tree++)
        {
            allTrees[tree].GetComponent<Tree>().setNode(treesConfig[tree], tree);
        }
    }

    public void getNewPlayerConfig()
    {
        playerConfig = ConfigLoader.Load(staticTransition.userID, staticTransition.userPass);
    }



    public void toFrontPage()
    {
        SceneManager.LoadScene("FrontPage");
    }
}
