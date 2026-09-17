using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndHandler : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }

    public void Main()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
