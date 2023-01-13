using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Online : MonoBehaviour
{
    [SerializeField]
    private GameObject damageTag;

    [SerializeField]
    private string path;

    private bool hasDirection = true;

    #region gameplayer related

    private EnemyConfig config;


    private float speedDebuff = 1;

    public float SpeedDebuff { set { speedDebuff = value; } }

    private float speed { get { return config.speed * speedDebuff * isGamePause; } }

    public bool isSlowed = false;

    private Health_bar healthBar;

    private float maxHealth { get { return config.health; } }

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

    #endregion


    private Animator current_animator;

    private int isGamePause;

    private GameManager_Online gameManager;

    private Transform damageTagContainer
    {
        get
        { return gameManager.damageTagContainer; }
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
        gameManager = GameManager_Online.Instance;

        config = ConfigLoader.LoadTextFile<EnemyConfig>(path);
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Health_bar>();
    }

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

    void FixedUpdate()
    {
        move();
    }

    public void Setup(Transform Enemies, string id, List<Node> path)
    {
        current_path = path;
        transform.position = LevelManager_Online.Instance.spawner.transform.position;
        name = id;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        transform.SetParent(Enemies);
    }

    private void move()
    {
        if (pathPos >= current_path.Count)
        {
            GameManager_Online.Instance.takeLife();
            removeEnemy();
        }
        else
        {

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
            //GameManager_Online.Instance.Currency += config.gold;

            if (new_damageTag != null)
            {
                Destroy(new_damageTag);
            }

            removeEnemy();
        }
    }


    private void direction(Vector3 currentPos, Vector3 nextPos)
    {
        if (this.hasDirection) {
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

    private void removeEnemy()
    {
        gameManager.GetComponent<Spawner_Online>().EnemiesList.Remove(this);

        Destroy(this.gameObject);
    }

}
