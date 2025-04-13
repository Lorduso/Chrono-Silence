using UnityEngine;

public class AnimController : MonoBehaviour
{
    public PlayerController PC;
    public float moveSpeed = 5f;
    public float shootCooldown = 0.5f;

    private float nextShootTime = 0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        rb.linearVelocity = moveInput * moveSpeed;

        // Анимация стояния
        animator.SetBool("IsStay", moveInput == Vector2.zero);

        // Поворот спрайта
        if (Input.GetKey(KeyCode.A))
            spriteRenderer.flipX = true;
        else if (Input.GetKey(KeyCode.D))
            spriteRenderer.flipX = false;

        // Стрельба с кулдауном
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextShootTime)
        {
            PC.Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }
}
