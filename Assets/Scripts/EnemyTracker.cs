using UnityEngine;
using UnityEngine.UI; // ��� TMPro, ���� ����������� TextMeshPro

public class EnemyTracker : MonoBehaviour
{
    public Text enemyCountText;       // UI-����� (���� ����������� TMPro, ������� �� TMP_Text)
    public GameObject portal;         // ������ �������, ������� ������ ��������������
    public string enemyTag = "Enemy"; // ���, �� �������� ��������� �����

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

