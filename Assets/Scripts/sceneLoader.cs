using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadBackstory()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();
    }
}
