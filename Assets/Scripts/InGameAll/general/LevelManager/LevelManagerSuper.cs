using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManagerSuper : Singleton<LevelManagerSuper>
{
    [SerializeField]
    private GameObject EnemyEntrance;
    
    public LevelConfig config;

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private GameObject entranceIndicator, exitIndicator;

    private float[] entranceIndicatorInfo;

    private float[] exitIndicatorInfo;

    private string[] mapData;

    private int mapWidth;

    private int mapHeight;

    private Vector3 origin;

    #region start_end

    [SerializeField]
    private GameObject walkerPrefab;
    public GameObject spawner { get; set; }

    public Point spawnPoint { get { return getPointFromConfig(config.start); } }

    public GameObject destination { get; set; }

    public Point desPoint { get { return getPointFromConfig(config.end); } }

    #endregion

    [SerializeField]
    private Transform map;

    public Point mapSize { get; private set; }

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

        cameraMovement = CameraMovement.Instance;
    }

    void Start()
    {
        mapData = config.map;

        mapWidth = mapData[0].ToCharArray().Length;

        mapHeight = mapData.Length;

        cameraMovement.TileWidth = TileWidth;
        cameraMovement.TileHeight = TileHeight;

        //initOngoingGame();

        setCameraPosition(spawnPoint);

        entranceIndicatorInfo = getEntranceExitPosition(spawnPoint, false);
        exitIndicatorInfo = getEntranceExitPosition(desPoint, true);

        Instantiate(entranceIndicator, new Vector2(entranceIndicatorInfo[1], entranceIndicatorInfo[2]), Quaternion.Euler(new Vector3(0, 0, entranceIndicatorInfo[0])));
        Instantiate(exitIndicator, new Vector2(exitIndicatorInfo[1], exitIndicatorInfo[2]), Quaternion.Euler(new Vector3(0, 0, exitIndicatorInfo[0])));
    }


    private Point getPointFromConfig(string str)
    {
        List<string> strs = str.Split(',').ToList();
        return new Point(int.Parse(strs[0]), int.Parse(strs[1]));
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
}
