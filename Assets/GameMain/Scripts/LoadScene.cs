using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public bool IsStartLoad;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        if (IsStartLoad)
        {
            Load(sceneName);
        }
    }

    public void Load(string name)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }
        SceneManager.LoadScene(name);
    }
}
