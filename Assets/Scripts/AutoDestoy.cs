using UnityEngine;
using UnityEngine.SceneManagement;
public class AutoDestroy : MonoBehaviour
{
    public float lifetime = 2f;            // Время до уничтожения
    public GameObject objectToActivate;    // Объект, который нужно активировать
    public bool IsLast = false;
    public int sceneNum = 2;

    void Start()
    {

        Invoke(nameof(HandleDestruction), lifetime);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(2);
        }
    }

    void HandleDestruction()
    {
        if (!IsLast)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
            Destroy(gameObject);
        }
        else
        {
            SceneManager.LoadScene(sceneNum);
        }
    }
}

