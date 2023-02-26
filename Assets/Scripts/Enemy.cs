using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyType EnemyTypes;
    private int maxHealth;
    public int currentHealth;

    private int damage;

    private float moveSpeed;
    private float speed;
    [HideInInspector] public bool isInAggroRange;

    private Transform player;
    private Animator animator;
    private PlayerHealth playerHealth;
    private Collider attackCollider;
    private GameManager gameManager;
    private EnemyManager enemyManager;
    private BoxCollider boxCollider;
    private Rigidbody rigidbody;
    private GameObject coin;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        attackCollider = GetComponentInChildren<SphereCollider>();
        gameManager = FindObjectOfType<GameManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
        boxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        coin = GameObject.FindGameObjectWithTag("Coin");

        if (EnemyTypes == EnemyType.Goblin)
        {
            maxHealth = 80;
            moveSpeed = 3f;
            damage = 3;
        }

        if (EnemyTypes == EnemyType.Zombie)
        {
            maxHealth = 120;
            moveSpeed = 0.8f;
            damage = 4;
        }
        if (EnemyTypes == EnemyType.BossWejmar)
        {
            maxHealth = 500;
            moveSpeed = 0.4f;
            damage = 16;
        }

        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            speed = 0f;
        }
        else
        {
            speed = moveSpeed;
        }

        Vector3 playerPosition = new Vector3(player.position.x,
            transform.position.y,
            player.position.z);

        if (animator.GetBool("isDead") == false)
        {
            transform.LookAt(playerPosition);
        }

        if (isInAggroRange && animator.GetBool("isDead") == false)
        {
            if (playerHealth.isDead())
            {
                speed = 0f;
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", true);

                var step = speed * Time.deltaTime;
                transform.position =
                    Vector3.MoveTowards(transform.position,
                        player.position, step);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.ResetTrigger("Attack");
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        boxCollider.enabled = false;

        attackCollider.enabled = false;

        animator.SetBool("isDead", true);

        if (EnemyTypes == EnemyType.Goblin)
        {
            for (int i = 0; i < 2; i++)
            {
                DropCoin();
            }

            Debug.Log("Goblin died!");
        }

        if (EnemyTypes == EnemyType.Zombie)
        {
            for (int i = 0; i < 3; i++)
            {
                DropCoin();
            }

            Debug.Log("Zombie died!");
        }
        if (EnemyTypes == EnemyType.BossWejmar)
        {
            for (int i = 0; i < 15; i++)
            {
                DropCoin();
            }

            Debug.Log("Wejmar died!");
        }

        gameManager.enemiesAmount--;
        Destroy(gameObject, 2);
        Destroy(transform.parent.gameObject, 4);
    }

    public void Attack()
    {
        animator.SetBool("isMoving", false);
        playerHealth.TakeDamage(damage);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        }
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }

    private void DropCoin()
    {
        Vector3 enemyPosition = new Vector3(transform.position.x,
            transform.position.y + 1,
            transform.position.z);

        Instantiate(coin, enemyPosition, Quaternion.identity);
    }
}