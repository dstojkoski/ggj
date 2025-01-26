using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public void MainMenu()
    {
        // Load the "Level_Main" scene when the MainMenu button is clicked
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();

    }

}
