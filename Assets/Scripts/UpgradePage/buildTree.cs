using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class buildTree
{
    public static void build()
    {
        string saveFile = "Upgrade/TreeNode";

        TreeConfig config = new TreeConfig();

        //first value in the list is level, second is increment
        //first four elements of tower are the same

        //sub-elements are 
        //pre-requisite number
        //increment, increment multiplier
        //gold cost, gold cost multiplier
        //pre-requisite_(num) number, pre-requisite_(num) level

        //array length is 6 + resource cost * 2. resource cost is in pair
        //pre-requisite * 2. pre-requisite is in pair

        //pre-requisite number is in the form of a in index


        config.trees = new Dictionary<int, float[][]>
        {
            {
                //utility
                0, new float[][]
                {
                    new float[] { 0, 2.2f, 0.5f, 3000f, 2,5f },                            //inGame_life
                    new float[] { 1, 1.5f, 0.4f, 900f, 1.5f, 0, 3}                                  //inGame_gold
                }
            },

            { 
                //general upgrade
                1, new float[][]
                {
                    new float[] { 0, 0.02f, 0.04f, 60f, 0.08f },                                 //general_damage
                    new float[] { 0, 0.02f, 0.04f, 60f, 0.08f },                                 //general_range
                    new float[] { 0, 0.02f, 0.04f, 60f, 0.08f },                                 //general_atk_speed
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 5 }                                  //general_effect
                }
            },

            { 
                //first tower
                2, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },                                 //damage
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },                                 //range
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },                                 //atk_speed
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }                                  //effect
                }
            },

            {
                //second tower
                3, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            },
            {
                //third tower
                4, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            },
                {
                //fourth tower
                5, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            },
            {
                //fifth tower
                6, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            },
            {
                //sixth tower
                7, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            }
            ,
            {
                //sixth tower
                8, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            }
            ,
            {
                //sixth tower
                9, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            }
            ,
            {
                //sixth tower
                10, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            }
            ,
            {
                //sixth tower
                11, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            }
            ,
            {
                //sixth tower
                12, new float[][]
                {
                    new float[] { 0, 0, 0, 5f, 0 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 0, 1 },
                    new float[] { 1, 0.02f, 0.04f, 60f, 0.08f, 1, 5 }
                }
            }

        };

        //ConfigSaver.saveTextFile<TreeConfig>(saveFile, config);
    }
}
