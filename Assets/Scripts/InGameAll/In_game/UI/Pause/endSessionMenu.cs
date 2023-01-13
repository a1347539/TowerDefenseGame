using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endSessionMenu : MonoBehaviour
{
    [SerializeField]
    private Text waveNum, goldEarn;

    [SerializeField]
    private GameObject scrollContainer;

    [SerializeField]
    private GameObject container;

    [SerializeField]
    private GameObject button;

    [SerializeField]
    private Pause_Menu Pause_Menu;

    private GameManager gameManager;

    private Dictionary<int, int> captured;

    private string wave { set { waveNum.text = value; } }
    private string gold { set { goldEarn.text = value; } }

    private int numberCapturedUntilExceeded = 0;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {
        captured = gameManager.capturedEnemy;

        StartCoroutine(showButton());

        gold = gameManager.Currency.ToString();
        wave = gameManager.Current_wave.ToString();

        getCapturedEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator showButton()
    {
        yield return new WaitForSecondsRealtime(2f);
        button.SetActive(true);
    }

    private void getCapturedEnemy()
    {
        int Buttom = 0;

        foreach (KeyValuePair<int, int> enemy in captured)
        {
            for (int i = 0; i < captured[enemy.Key]; i++)
            {
                numberCapturedUntilExceeded++;

                if (numberCapturedUntilExceeded % 10 == 0)
                {
                    if (numberCapturedUntilExceeded >= 30)
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
    }
}
