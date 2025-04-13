using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public Sprite[] flightSprites; // [0] = старт, [1] = летит, [2] = попадание
    public float[] spriteChangeTimes; // [0] = через 0.1с, [1] = через 0.2с и т.д.
    public float lifeTime = 2f;

    private SpriteRenderer sr;
    private int spriteIndex = 0;
    private float timer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = flightSprites[0];
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (spriteIndex < flightSprites.Length - 1 && timer >= spriteChangeTimes[spriteIndex])
        {
            spriteIndex++;
            sr.sprite = flightSprites[spriteIndex];
        }

        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            sr.sprite = flightSprites[flightSprites.Length - 1]; // последний спрайт — взрыв
            Destroy(gameObject, 0.1f);
        }
    }
}
