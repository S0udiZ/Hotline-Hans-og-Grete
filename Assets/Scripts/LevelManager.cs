using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int Startlevel = 1;
    [SerializeField] int Maxlevel; // Maximum number of levels
    int level = 0;
    void Start()
    {
        level = Startlevel;
        SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
    }
    public void ChangeLevel(int newlevel)
    {
        GetComponent<PlayerControllerScript>().ResetPlayers();
        SceneManager.UnloadSceneAsync(level.ToString());
        Debug.Log("Level Change");
        Debug.Log("lvl: " + level);
        Debug.Log("new lvl: " + newlevel);
        level = newlevel;
        if (level <= Maxlevel)
        {
            SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
        } else {
            SceneManager.LoadSceneAsync("End", LoadSceneMode.Additive);
        }
    }

    public void ResetLevel()
    {
        Debug.Log("Level Reset");
        GetComponent<PlayerControllerScript>().ResetPlayers();
        SceneManager.UnloadSceneAsync(level.ToString());
        SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
    }

    public int GetLevel()
    {
        return level;
    }
}
