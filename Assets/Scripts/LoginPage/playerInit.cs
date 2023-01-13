using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class playerInit
{

    private static string playinitPathPrefix = "Player/Data/";

    public static void playerInitiation(string Username, string password)
    {
        string saveFile = playinitPathPrefix + Username + ".txt";

        PlayerConfig config = new PlayerConfig();

        config.Username = Username;
        config.Password = password;

        config.isInGame = false;

        config.userMapPath = null;

        config.userMapConfig = null;

        config.gameConfig = null;

        config.capturedEnemy_duringSession = new Dictionary<int, int>();

        config.allCapturedEnemy = new Dictionary<int, int>();

        config.gold = 200;

        //first value in the list is level, second is increment
        //first four elements of tower are the same

        config.trees = new Dictionary<int, float[][]>
        {
            {
                //utility
                0, new float[][]
                {
                    new float[] { 1, 1 } ,                                //inGame_life
                    new float[] { 1, 1 }                                  //inGame_gold
                }
            },

            { 
                //general upgrade
                1, new float[][]
                {
                    new float[] { 1, 0 },                                 //general_damage
                    new float[] { 1, 0 },                                 //general_range
                    new float[] { 1, 0 },                                 //general_atk_speed
                    new float[] { 1, 0 }                                  //general_effect
                }
            },

            { 
                //first tower
                2, new float[][]
                {
                    new float[] { 0, 0 },
                    new float[] { 1, 0 },                                 //damage
                    new float[] { 1, 0 },                                 //range
                    new float[] { 1, 0 },                                 //atk_speed
                    new float[] { 1, 0 }                                  //effect
                }
            },

            {
                //second tower
                3, new float[][]
                {
                    new float[] { 0, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 }
                }
            },

            {
                //third tower
                4, new float[][]
                {
                    new float[] { 0, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 }
                }
            },

            { 
                //fourth tower
                5, new float[][]
                {
                    new float[] { 0, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 }
                }
            },

            { 
                //fifth tower
                6, new float[][]
                {
                    new float[] { 0, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 }
                }
            },

            { 
                //sixth tower
                7, new float[][]
                {
                    new float[] { 0, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 },
                    new float[] { 1, 0 }
                }
            }
        };

        ConfigSaver.save<PlayerConfig>(saveFile, config);
    }
}
