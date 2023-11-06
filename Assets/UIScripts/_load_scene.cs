using UnityEngine;
using UnityEngine.SceneManagement;

public class _Load_Scene : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
