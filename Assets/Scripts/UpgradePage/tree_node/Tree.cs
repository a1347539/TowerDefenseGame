using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree: MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nodes;

    public Dictionary<GameObject, TreeNode> NodeDict { get; private set; }


    public void setNode(float[][] NodesConfig, int treePosition)
    {
        NodeDict = new Dictionary<GameObject, TreeNode>();
        
        for (int node = 0; node < nodes.Count; node++)
        {
            TreeNode a = new TreeNode(nodes[node], NodesConfig[node], treePosition, node);
            NodeDict.Add(nodes[node], new TreeNode(nodes[node], NodesConfig[node], treePosition, node));
        }
    }
}
