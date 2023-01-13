using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition : Singleton<transition>
{
    [SerializeField]
    public string pathToLevel;

    [SerializeField]
    public string pathToPlayerConfig;


    //private void Start()
    //{
    //    buildTree.build();
    //}
}
