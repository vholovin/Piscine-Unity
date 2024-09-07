using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    void OnMouseDown()
    {
    }
   
    void Start()
    {
    }

    void Update()
    {
    }
   
    public void OnPlayClick()
    {
        SceneManager.LoadScene ("level0");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
