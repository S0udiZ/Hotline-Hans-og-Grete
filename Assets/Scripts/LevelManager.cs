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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeLevel(GetLevel()+1);
        }
    }
    public void ChangeLevel(int newlevel)
    {
        SceneManager.UnloadSceneAsync(level.ToString());
        level = newlevel;
        SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
    }

    public void ResetLevel()
    {
        ChangeLevel(Startlevel);
    }

    public int GetLevel()
    {
        return level;
    }
}
