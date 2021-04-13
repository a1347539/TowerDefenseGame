using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceExit : MonoBehaviour
{

    IEnumerator disableLoadSceneCorotine()
    {
        yield return new WaitForSeconds(8f);
        disableLoadScene();
    }

    private void disableLoadScene()
    {
        this.gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);

        StartCoroutine(disableLoadSceneCorotine());
    }

}
