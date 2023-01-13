using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class List : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHolder;

    private PlayerConfig player { get { return GameManager_Online.Instance.player; } }

    private void Start()
    {
        setHolder();
    }

    private void setCurrentEnemy(int i)
    {

        Spawner_Online.Instance.CurrentEnemy = i;
    }

    private void setHolder()
    {
        foreach (KeyValuePair<int, int> enemy in player.allCapturedEnemy)
        {

            GameObject holder = Instantiate(enemyHolder);

            Spawner_Online.Instance.allHolders[enemy.Key] = holder;

            holder.transform.SetParent(this.transform.GetChild(0));

            Button btn = holder.AddComponent<Button>();
            
            btn.onClick.AddListener(() => setCurrentEnemy(enemy.Key));

            Sprite sprite = Resources.Load<Sprite>("CapturedEnemy/" + enemy.Key);
            holder.transform.GetChild(0).GetComponent<Image>().sprite = sprite;

            holder.transform.GetChild(1).GetComponent<Text>().text = (enemy.Value).ToString();

        }
    }
}
