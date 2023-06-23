using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelManager Instance;
    private string filePath;
    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Level.txt");
        Debug.Log(CurrentLevel());
        LoadSceneByID(CurrentLevel());
    }

    public int CurrentLevel()
    {
        if (File.Exists(filePath))
        {
            int levelID;
            if (int.TryParse(File.ReadAllText(filePath), out levelID))
            {
                return levelID;
            }
        }
        return 0;
    }

    public void NextLevel()
    {
        int nextLevel = CurrentLevel() + 1;
        File.WriteAllText(filePath, nextLevel.ToString());
    }

    public void LoadSceneByID(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
