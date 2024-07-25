using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(GameManager.GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
