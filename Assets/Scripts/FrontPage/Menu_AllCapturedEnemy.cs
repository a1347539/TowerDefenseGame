using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_AllCapturedEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject scrollContainer;

    [SerializeField]
    private GameObject container;

    [SerializeField]
    private Text totalNumberCaptured;

    private PlayerConfig playerConfig { get { return GetComponent<PlayerDataStorage>().current_player_config; } }

    private Dictionary<int, int> captured;

    private int numberCapturedUntilExceeded;


    private void Awake()
    {
        captured = playerConfig.allCapturedEnemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        getCapturedEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getCapturedEnemy()
    {
        int Buttom = 0;

        foreach (KeyValuePair<int, int> enemy in captured)
        {
            for (int i = 0; i < captured[enemy.Key]; i++)
            {
                numberCapturedUntilExceeded++;

                if (numberCapturedUntilExceeded % 13 == 0)
                {
                    if (numberCapturedUntilExceeded >= 65)
                    {
                        Buttom -= 26;
                        scrollContainer.GetComponent<RectTransform>().offsetMin = new Vector2(
                            scrollContainer.GetComponent<RectTransform>().offsetMin.x, scrollContainer.GetComponent<RectTransform>().offsetMin.y + Buttom);
                    }
                }
                GameObject capturedEnemy = Instantiate(container);
                Sprite sprite = Resources.Load<Sprite>("CapturedEnemy/" + enemy.Key);
                capturedEnemy.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                capturedEnemy.transform.SetParent(scrollContainer.transform, false);
            }
        }

        totalNumberCaptured.text = "Total: " + numberCapturedUntilExceeded.ToString();
    }

}
