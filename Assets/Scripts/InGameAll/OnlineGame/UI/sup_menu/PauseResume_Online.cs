using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResume_Online : MonoBehaviour
{
    [SerializeField]
    private Sprite pause, resume;

    public void setValue(bool isPause)
    {
        if (isPause)
        {

            GetComponent<Image>().sprite = resume;

        }
        else
        {

            GetComponent<Image>().sprite = pause;

        }
    }
}
