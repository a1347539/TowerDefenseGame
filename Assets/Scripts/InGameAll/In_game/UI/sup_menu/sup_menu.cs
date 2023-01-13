using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sup_menu: MonoBehaviour
{
    [SerializeField]
    private Text inGameSpeed;
    private bool isSpeedChanged = false;

    public Button pause_resume;

    public int InGameSpeed
    {
        set
        {
            inGameSpeed.text = "x" + value.ToString();
        }
    }

    [SerializeField]
    private Text timer;

    public int Timer
    {
        set
        {

            timer.text = value.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InGameSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
