using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFormBase : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ShowORHide(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void SetMode(int num)
    {
        CommandManger.Mode = num;
    }
}
