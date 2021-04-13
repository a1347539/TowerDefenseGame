using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    public GameObject node { get; private set; }

    public string nodeName { get { return node.name; } }

    public float[] nodeConfig { get; private set; }

    public int nodeOfTree;

    public int rank { get; private set; }

    public float nodeLevel { get { return UpgradePage.Instance.playerConfig.trees[nodeOfTree][rank][0]; } }

    public float NumberOfPrerequisite { get { return nodeConfig[0]; } }

    public float increment { get { return nodeConfig[1]; } }

    public float incrementMultiplier { get { return nodeConfig[2]; } }

    public float goldCost { get { return nodeConfig[3]; } }

    public float goldCostMultiplier { get { return nodeConfig[4]; } }

    public float upgradeDisplay { get { return stateCalculator.upgradeStat(increment, incrementMultiplier, nodeLevel); } }

    public float priceDisplay { get { return stateCalculator.upgradeStat(goldCost, goldCostMultiplier, nodeLevel); } }

    public TreeNode(GameObject node, float[] nodeConfig, int treePosition, int rank)
    {
        this.node = node;
        this.nodeConfig = nodeConfig;
        this.nodeOfTree = treePosition;
        this.rank = rank;
    }
}

