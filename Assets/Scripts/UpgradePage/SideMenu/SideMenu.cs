using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SideMenu : MonoBehaviour
{
    [SerializeField]
    private Button upgradeButton;

    [SerializeField]
    private GameObject upgradeIndicator;

    private UpgradePage upgradePage;

    private GameObject currentButton;

    private int currentButtonOfTree;

    private TreeNode current_treeNode;

    private Tree currentTree;

    private float[] treeConfig { get { return current_treeNode.nodeConfig; } }

    private PlayerConfig playerConfig { get { return upgradePage.playerConfig; } }

    private float lastClickTime;

    private float Double_click_interval = 0.5f;

    private bool isOpen;

    private Animator animator;

    private bool isTester = false;

    private void Awake()
    {
        upgradePage = UpgradePage.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getPressed(GameObject button)
    {
        //print(button);
        upgradeIndicator.SetActive(false);

        currentButton = button;

        currentTree = button.transform.parent.parent.GetComponent<Tree>();

        current_treeNode = currentTree.NodeDict[button];

        currentButtonOfTree = current_treeNode.nodeOfTree;

        open_sideMenu();

        try
        {
            for (int i = 0; i < current_treeNode.NumberOfPrerequisite; i++)
            {
                if (playerConfig.trees[currentButtonOfTree][(int)treeConfig[2 * i + 5]][0] < (int)treeConfig[2 * i + 6])
                {
                    upgradeButton.interactable = false;
                }
                else
                {
                    upgradeButton.interactable = true;
                }
            }

            if (playerConfig.gold < current_treeNode.priceDisplay)
            { upgradeButton.interactable = false; }
            else if (current_treeNode.NumberOfPrerequisite == 0 && playerConfig.gold >= current_treeNode.priceDisplay)
            { upgradeButton.interactable = true; }


            if (current_treeNode.rank == 0)
            {
                if (currentTree != upgradePage.allTrees[0].GetComponent<Tree>() && currentTree != upgradePage.allTrees[1].GetComponent<Tree>())
                {
                    if (current_treeNode.nodeLevel >= 1)
                    {
                        upgradeButton.interactable = false;
                    }
                }
            }
            isTester = false;
        }
        catch { isTester = true; }

        StatRefresh();
    }


    //need big change
    //need to add discription to every nodes
    public void upgrade()
    {
        upgradeIndicator.SetActive(true);

        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= Double_click_interval)
        {
            print("tree#: " + (current_treeNode.nodeOfTree) + ", node#: " + (current_treeNode.rank));

            upgradeIndicator.SetActive(false);
            
            playerConfig.gold -= stateCalculator.upgradeStat(current_treeNode.goldCost, current_treeNode.goldCostMultiplier, current_treeNode.nodeLevel);

            playerConfig.trees[currentButtonOfTree][current_treeNode.rank][0]++;
            
            upgradePage.GetComponent<textHandler>().setValue(playerConfig.gold);

            playerConfig.trees[currentButtonOfTree][current_treeNode.rank][1] = current_treeNode.upgradeDisplay;
            
            ConfigSaver.save(staticTransition.userID, staticTransition.userPass, playerConfig); 

            upgradePage.getNewPlayerConfig();

            if (playerConfig.gold < current_treeNode.priceDisplay)
            { upgradeButton.interactable = false; }


            if (current_treeNode.rank == 0)
            {
                if (currentTree != upgradePage.allTrees[0].GetComponent<Tree>() && currentTree != upgradePage.allTrees[1].GetComponent<Tree>())
                {
                    if (current_treeNode.nodeLevel >= 1)
                    {
                        upgradeButton.interactable = false;
                    }
                }
            }
        }
        lastClickTime = Time.time;
        StatRefresh();
    }

    public void StatRefresh()
    {
        int displayerType = 0;
        if (currentButtonOfTree == 0)
        { displayerType = 1; }
        else if (current_treeNode.rank == 0 && currentButtonOfTree != 1)
        { displayerType = 2; }

        if (!isTester)
        {
            GetComponent<SideMenuTxtHandler>().setValue
                (
                displayerType,
                currentButton.transform.parent.parent.name,
                currentButton.name,
                current_treeNode.nodeLevel,
                stateCalculator.upgradeStat(current_treeNode.increment, current_treeNode.incrementMultiplier, current_treeNode.nodeLevel + 1) - current_treeNode.upgradeDisplay,
                current_treeNode.upgradeDisplay,
                current_treeNode.priceDisplay
                );
        }
        else
        {
            GetComponent<SideMenuTxtHandler>().setValue
                (
                1,
                "0",
                "0",
                0,
                0,
                0,
                0
                );
        }


    }


    public void close_sideMenu()
    {
        GetComponent<SideMenuTxtHandler>().resetField();

        animator.SetBool("isOpen", false);
        isOpen = false;
    }

    public void open_sideMenu()
    {
        if (isOpen)
        {
            animator.SetBool("isRefresh", true);
            StartCoroutine(turnOffIsRefresh());
            return;
        }
        animator.SetBool("isOpen", !isOpen);
        isOpen = !isOpen;
    }

    IEnumerator turnOffIsRefresh()
    {
        yield return 0;
        animator.SetBool("isRefresh", false);
    }
}
