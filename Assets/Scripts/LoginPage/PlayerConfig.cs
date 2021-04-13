using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfig
{
    public string Username;
    public string Password;

    public bool isInGame;

    public string userMapPath;

    public List<float[]> userMapConfig;

    public float[] gameConfig;

    public Dictionary<int, int> capturedEnemy_duringSession;

    public Dictionary<int, int> allCapturedEnemy;

    public float gold;

    public Dictionary<int, float[][]> trees;
}
