using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class LevelManager_Online : Singleton<LevelManager_Online>
{

    [SerializeField]
    private GameObject EnemyEntrance;

    private PlayerConfig playerConfig { get { return GameManager_Online.Instance.player; } }

    private PlayerConfig OpponentConfig { get { return GameManager_Online.Instance.opponent; } }

    private string opponentLevelPath { get { return OpponentConfig.userMapPath; } }


    public LevelConfig config;

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    private string[] mapData;

    private int mapWidth;

    private int mapHeight;

    private Vector3 origin;

    #region start_end

    [SerializeField]
    private GameObject walkerPrefab;

    [SerializeField]
    private GameObject entranceIndicator, exitIndicator;

    private float[] entranceIndicatorInfo;

    private float[] exitIndicatorInfo;

    public GameObject spawner { get; set; }

    public Point spawnPoint { get { return getPointFromConfig(config.start); } }

    public GameObject destination { get; set; }

    public Point desPoint { get { return getPointFromConfig(config.end); } }

    #endregion

    [SerializeField]
    private Transform map;

    public Point mapSize { get; private set; }

    public Dictionary<Point, Tile_Online> Tiles = new Dictionary<Point, Tile_Online>();

    public float TileWidth
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    public float TileHeight
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.y; }
    }

    private void Awake()
    {

        origin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        print(opponentLevelPath);
        config = ConfigLoader.LoadTextFile<LevelConfig>(opponentLevelPath);

        cameraMovement = CameraMovement.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        mapData = config.map;

        mapWidth = mapData[0].ToCharArray().Length;

        mapHeight = mapData.Length;

        CreateNewLevel();

        PlopTowersToMap();

        cameraMovement.TileWidth = TileWidth;
        cameraMovement.TileHeight = TileHeight;

        //initOngoingGame();

        setCameraPosition(spawnPoint);

        entranceIndicatorInfo = getEntranceExitPosition(spawnPoint, false);
        exitIndicatorInfo = getEntranceExitPosition(desPoint, true);

        Instantiate(entranceIndicator, new Vector2(entranceIndicatorInfo[1], entranceIndicatorInfo[2]), Quaternion.Euler(new Vector3(0, 0, entranceIndicatorInfo[0])));
        Instantiate(exitIndicator, new Vector2(exitIndicatorInfo[1], exitIndicatorInfo[2]), Quaternion.Euler(new Vector3(0, 0, exitIndicatorInfo[0])));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateNewLevel()
    {
        Vector3 maxTile;

        mapSize = new Point(mapWidth, mapHeight);

        for (int y = 0; y < mapHeight; y++)
        {
            char[] row = mapData[y].ToCharArray();

            for (int x = 0; x < mapWidth; x++)
            {
                PlaceTile(row[x].ToString(), x, y, origin);
            }
        }

        maxTile = Tiles[new Point(mapWidth - 1, mapHeight - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x, maxTile.y));

        SpawnWalker();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 origin)
    {
        bool walkable;

        int tileIndex = int.Parse(tileType);

        Tile_Online newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<Tile_Online>();

        if (tileIndex % 2 == 1) { walkable = true; }
        else { walkable = false; }

        newTile.Setup(new Point(x, y), new Vector3(origin.x + TileWidth * x, origin.y - TileHeight * y, 0), walkable, map);
    }

    private void SpawnWalker()
    {
        GameObject entrance = Instantiate(EnemyEntrance, Tiles[spawnPoint].CenterOfGridInWorldPosition, Quaternion.identity);
        entrance.GetComponent<SpriteRenderer>().sortingOrder = 60;
        //entrance.AddComponent<BoxCollider2D>();
        spawner = Instantiate(walkerPrefab, Tiles[spawnPoint].CenterOfGridInWorldPosition, Quaternion.identity);
        spawner.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Tiles[spawnPoint].TileIsEmpty = false;

        destination = Instantiate(walkerPrefab, Tiles[desPoint].CenterOfGridInWorldPosition, Quaternion.identity);
        destination.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Tiles[desPoint].TileIsEmpty = false;
    }

    private Point getPointFromConfig(string str)
    {
        List<string> strs = str.Split(',').ToList();
        return new Point(int.Parse(strs[0]), int.Parse(strs[1]));
    }

    private void PlopTowersToMap()
    {
        foreach (float[] towerInTile in OpponentConfig.userMapConfig)
        {
            string towerPath = "Prefabs/towers_Online/tower_" + towerInTile[2] + "/Tower";

            Tile_Online tile = Tiles[new Point((int)towerInTile[0], (int)towerInTile[1])];

            GameObject towerPrefab = Resources.Load<GameObject>(towerPath);

            tile.PlaceTower(towerPrefab, 0, towerInTile);
        }
    }

    private float[] getEntranceExitPosition(Point p, bool isExit)
    {
        Vector3 position;

        int angleRffset;

        if (isExit) { angleRffset = 180; }
        else { angleRffset = 0; }

        if (p.x == 0)
        {
            position = new Vector2(origin.x + TileWidth * 2, (origin.y - TileHeight * p.y) - TileHeight / 2);
            return new float[] { 0 + angleRffset, position.x, position.y };
        }
        else if (p.x == mapWidth - 1)
        {
            position = new Vector2((origin.x + TileWidth * (p.x - 1)), (origin.y - TileHeight * p.y) - TileHeight / 2);
            return new float[] { 180 - angleRffset, position.x, position.y };
        }
        else
        {
            if (p.y == 0)
            {
                position = new Vector2((origin.x + TileWidth * p.x) + TileWidth / 2, origin.y - TileHeight * 2);
                return new float[] { 270 - angleRffset, position.x, position.y };
            }
            else
            {
                position = new Vector2((origin.x + TileWidth * p.x) + TileWidth / 2, (origin.y - TileHeight * (p.y - 1)));
                return new float[] { 90 + angleRffset, position.x, position.y };
            }
        }
    }

    private void setCameraPosition(Point p)
    {
        float cameraHeight = cameraMovement.gameObject.GetComponent<Camera>().orthographicSize * 2f;
        float cameraWidth = cameraMovement.gameObject.GetComponent<Camera>().aspect * cameraHeight;


        if (p.x == 0)
        {
            cameraMovement.transform.position = new Vector3(TileWidth, -(mapHeight * TileHeight - cameraHeight) / 2, -1);
        }
        else if (p.x == mapWidth - 1)
        {
            cameraMovement.transform.position = new Vector3((mapWidth - 1) * TileWidth - cameraWidth, -(mapHeight * TileHeight - cameraHeight) / 2, -1);
        }
        else
        {
            if (p.y == 0)
            {
                cameraMovement.transform.position = new Vector3((mapWidth * TileWidth - cameraWidth) / 2, -TileHeight, -1);
            }
            else
            {
                cameraMovement.transform.position = new Vector3((mapWidth * TileWidth - cameraWidth) / 2, -((mapHeight - 1) * TileHeight - cameraHeight), -1);
            }
        }
    }

    /*
    private void initOngoingGame()
    {
        GameManager.Instance.Currency = (int)playerConfig.gameConfig[0];
        GameManager.Instance.NumOfCapturedEnemy = (int)playerConfig.gameConfig[1];
        GameManager.Instance.Current_wave = (int)playerConfig.gameConfig[2];
        GameManager.Instance.Health = (int)playerConfig.gameConfig[3];
        GameManager.Instance.capturedEnemy = playerConfig.capturedEnemy_duringSession;
    }*/
}
