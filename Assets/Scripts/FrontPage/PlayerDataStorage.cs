using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataStorage : Singleton<PlayerDataStorage>
{
    public PlayerConfig current_player_config { get; private set; }

    public int numberOfEnemy = 0;

    private void Awake()
    {
        current_player_config = ConfigLoader.Load(staticTransition.userID, staticTransition.userPass);
        //current_player_config = ConfigLoader.Load<PlayerConfig>(staticTransition.pathToPlayerConfig);
    }

    private void Start()
    {
        /*current_player_config.gold = 999999;
        ConfigSaver.save("234", "234", current_player_config); */
        GetComponent<TextHandler>().setValue(current_player_config.Username, current_player_config.gold);

        foreach (KeyValuePair<int, int> enemy in current_player_config.allCapturedEnemy)
        {
            numberOfEnemy += enemy.Value;
        }
    }
}
