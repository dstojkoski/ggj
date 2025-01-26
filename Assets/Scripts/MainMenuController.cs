using UnityEngine;
using UnityEngine.SceneManagement;  // To use scene management

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the "Level_Main" scene when the Play button is clicked
        SceneManager.LoadScene("Level_Main");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();

    }
}
