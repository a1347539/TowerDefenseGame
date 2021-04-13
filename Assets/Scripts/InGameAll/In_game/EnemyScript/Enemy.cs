using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject damageTag;

    [SerializeField]
    private string path;

    private bool hasDirection = true;

    #region gameplayer related

    public EnemyConfig config { get; private set; }

    private float speedDebuff = 1;

    public float SpeedDebuff { set { speedDebuff = value; } }

    private float speed { get { return config.speed * speedDebuff * isGamePause; } }

    public bool isSlowed = false;

    private Health_bar healthBar;

    private float maxHealth { get { return config.health + gameManager.Current_wave * config.health / 30; } }

    private float current_health;

    #endregion

    #region pathfinding
    public List<Node> current_path { get; private set; }

    private int pathPos = 0;
    public int PathPos { get => pathPos; }

    private Vector3 next_position
    {
        get
        {
            return current_path[pathPos].position;
        }
    }

    public bool isMapChanged = false;

    #endregion

    private Animator current_animator;

    private int isGamePause;

    private GameManager gameManager;

    private Transform damageTagContainer
    {
        get
        { return GameManager.Instance.damageTagContainer; }
    }

    private Vector3 damageTagPos
    {
        get 
        { 
            return new Vector3(transform.position.x + 0.2f, transform.position.y + 0.4f, transform.position.z);
        }
    }

    private void Awake()
    {
        gameManager = GameManager.Instance;
        config = ConfigLoader.LoadTextFile<EnemyConfig>(path);
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Health_bar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        current_animator = GetComponent<Animator>();

        if (current_animator == null)
        { hasDirection = false; }

        current_health = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    private void Update()
    {
        isGamePause = gameManager.IsPausedInt;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    public void Setup(Transform Enemies, string id, List<Node> path)
    {
        current_path = path;
        transform.position = LevelManager.Instance.spawner.transform.position;
        name = id;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        transform.SetParent(Enemies);
    }


    public bool getNewPath()
    {
        foreach (Node node in current_path)
        {
            if (!node.Tile.IsWalkable)
            {
                if (PathFinding.FindPath(current_path[pathPos-1].tilePosition, current_path.Last().tilePosition))
                { 
                    current_path = PathFinding.Path;
                    if ((current_path[0].position.x < transform.position.x && transform.position.x < current_path[1].position.x) || (current_path[0].position.x > transform.position.x && transform.position.x > current_path[1].position.x))
                    {
                        current_path.RemoveAt(0);
                    }
                    else if ((current_path[0].position.y < transform.position.y && transform.position.y < current_path[1].position.y) || (current_path[0].position.y > transform.position.y && transform.position.y > current_path[1].position.y))
                    {
                        current_path.RemoveAt(0);
                    }
                    pathPos = 0;
                    direction(transform.position, next_position);
                    //isMapChanged = false;
                    return true;
                }
            }
        }
        return false;
    }

    private void move()
    {
        if (pathPos >= current_path.Count)
        {
            GameManager.Instance.Health--;
            removeEnemy(false);
        }
        else
        {
            if (isMapChanged)
            {
                if (getNewPath())
                {
                    pathPos = 0;
                    direction(transform.position, next_position);
                    isMapChanged = false;
                }

                isMapChanged = false;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, next_position, speed * Time.deltaTime);
            if (transform.position == next_position)
            {
                pathPos++;
                if (pathPos < current_path.Count)
                { direction(current_path[pathPos - 1].position, next_position); }
            }
        }
    }

    public void OnCollision(float damage)
    {
        GameObject new_damageTag = Instantiate(damageTag, damageTagPos, Quaternion.identity);

        new_damageTag.GetComponent<damageDisplay>().setup(damageTagContainer, damage);

        current_health -= damage;
        healthBar.setCurrentHealth(current_health);

        if (current_health <= 0)
        {
            GameManager.Instance.Currency += config.gold;

            if (new_damageTag != null)
            {
                Destroy(new_damageTag);
            }

            removeEnemy(true);
        }
    }


    private void direction(Vector3 currentPos, Vector3 nextPos)
    {
        if (this.hasDirection)
        {
            if (currentPos.x > nextPos.x)
            {
                //going left
                current_animator.SetInteger("direction", 1);
            }
            else if (currentPos.x < nextPos.x)
            {
                //going right
                current_animator.SetInteger("direction", 2);
            }
            else if (currentPos.y < nextPos.y)
            {
                //going up
                current_animator.SetInteger("direction", 3);
            }
            else if (currentPos.y > nextPos.y)
            {
                //going down
                current_animator.SetInteger("direction", 4);
            }
        }
    }

    private void removeEnemy(bool deathByTower)
    {
        if (deathByTower)
        {
            int r = new System.Random().Next(100);

            if (r / 100f <= config.capturedRate)
            {
                gameManager.NumOfCapturedEnemy++;

                if (!gameManager.capturedEnemy.ContainsKey(config.enemyId))
                {
                    gameManager.capturedEnemy.Add(config.enemyId, 1);
                }
                else
                {
                    gameManager.capturedEnemy[config.enemyId]++;
                }
            }
        }

        gameManager.GetComponent<Spawner>().EnemiesList.Remove(this);
        gameManager.enemyList.Remove(this);

        Destroy(this.gameObject);
    }
}
