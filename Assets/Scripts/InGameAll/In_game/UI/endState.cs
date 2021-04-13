using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endState : MonoBehaviour
{
    [SerializeField]
    private Text reward1, reward2, reward3;

    [SerializeField]
    private Sprite winprefab, lostprefab;

    [SerializeField]
    private GameObject button;

    private PlayerConfig playerConfig { get { return GameManager.Instance.player; } }

    private GameObject menu;

    private float Rate 
    { 
        get 
        {
            return GameManager.Instance.RewardRate_lost;
        }
    }

    public float Gold { get; private set; }

    public float Wave { get; private set; }

    public float Health { get; private set; }


    private void Awake()
    {
        menu = this.gameObject.transform.GetChild(0).gameObject;

        Gold = GameManager.Instance.Currency;
        Wave = GameManager.Instance.Current_wave;
        Health = GameManager.Instance.Health;
    }

    // Start is called before the first frame update
    void Start()
    {
        menu.GetComponent<Image>().sprite = lostprefab;

        setValue();
        setActiveExitButtonCorotine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setValue()
    {
        reward1.text = (Rate * Gold).ToString();
    }

    IEnumerator setActiveExitButtonCorotine()
    {
        yield return new WaitForSeconds(1f);
        setActiveExitButton();
    }
    private void setActiveExitButton()
    {
        button.SetActive(true);
    }

    public void ToFrontPage()
    {
        Time.timeScale = 1;

        playerConfig.isInGame = false;

        playerConfig.gold = Gold;

        ConfigSaver.save(staticTransition.userID, staticTransition.userPass, playerConfig);

        SceneManager.LoadScene("FrontPage");
    }
}
