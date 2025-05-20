using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }




    public void Options()
    {

    }




    public void QuitGame()
    {
        Application.Quit();
    }

}
