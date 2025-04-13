using UnityEngine;
using UnityEngine.UI; // или TMPro, если используешь TextMeshPro

public class EnemyTracker : MonoBehaviour
{
    public Text enemyCountText;       // UI-текст (если используешь TMPro, поменяй на TMP_Text)
    public GameObject portal;         // Объект портала, который должен активироваться
    public string enemyTag = "Enemy"; // Тег, по которому считаются враги

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag(enemyTag).Length;
        enemyCountText.text = "Enemies: " + enemyCount;
        if (enemyCount == 0)
        {
            portal.SetActive(true);
            enemyCountText.text = "All enemies are defeated!";
        }
        else
        {
            portal.SetActive(false);
        }
    }
}

