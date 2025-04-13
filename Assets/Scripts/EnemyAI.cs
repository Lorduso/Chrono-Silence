using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;              // Игрок
    public float detectionRadius = 5f;    // Радиус обнаружения
    public float moveSpeed = 2f;          // Скорость врага
    public int damageAmount = 1;          // Сколько урона наносит враг

    private Animator animator;
    private Rigidbody2D rb2d;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer();
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            rb2d.linearVelocity = Vector2.zero; // Остановить врага
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb2d.linearVelocity = direction * moveSpeed;

        if (direction.x < 0)
            transform.localScale = new Vector3(1, 1, 1); // Влево
        else if (direction.x > 0)
            transform.localScale = new Vector3(-1, 1, 1); // Вправо
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}

