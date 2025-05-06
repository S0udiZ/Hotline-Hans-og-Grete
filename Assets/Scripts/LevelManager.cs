using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int Startlevel = 1;
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
        try { //Does not fucking work and I do not know why
            SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
        } catch (System.Exception)
        {
            SceneManager.LoadSceneAsync("End", LoadSceneMode.Additive);
            throw;
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
