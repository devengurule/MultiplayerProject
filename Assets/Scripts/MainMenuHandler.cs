using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void DarkScene()
    {
        SceneManager.LoadScene("DarkMaze");
    }
    public void LightScene()
    {
        SceneManager.LoadScene("LightMaze");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
