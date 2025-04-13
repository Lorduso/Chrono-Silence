using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SceneFader sceneFader;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Animator animator;
    public GameObject gameOverPanel;
    public Slider healthSlider;

    public int maxHealth = 200;
    public int currentHealth = 100;
    public int maxArmor = 100;
    public int currentArmor = 0;

    public float damageInterval = 1f; // Интервал между ударами
    private bool isTakingDamage = false;


    private bool isAlive = true;
    void Start()
    {
        // Чтобы в начале выставить правильное значение
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            Shoot();
        }
        //currentHealth = maxHealth;
    }

    public void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Нет bulletPrefab или firePoint!");
            return;
        }

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        animator.SetTrigger("Shoot");
    }

    public void TakeDamage(int damage)
    {
        int remainingDamage = damage;

        if (currentArmor > 0)
        {
            int absorbed = Mathf.Min(currentArmor, damage);
            currentArmor -= absorbed;
            remainingDamage -= absorbed;
        }

        currentHealth -= remainingDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            gameOverPanel.SetActive(true);
        }
    }
    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Portal"))
    {
        
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (sceneFader != null)
            {
                sceneFader.FadeAndLoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(nextSceneIndex);
            }

        }
        else
        {
            Debug.Log("Следующая сцена не найдена. Возможно, это последняя.");
        }
    }
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isTakingDamage)
        {
            StartCoroutine(DamageOverTime());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isTakingDamage = false;
        }
    }

    IEnumerator DamageOverTime()
    {
        isTakingDamage = true;

        while (isTakingDamage)
        {
            TakeDamage(20); // Урон каждые damageInterval секунд
            yield return new WaitForSeconds(damageInterval);
        }
    }

}
