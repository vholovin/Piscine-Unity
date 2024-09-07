using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreen : MonoBehaviour
{
    void Start()
    {        
    }

    void Update()
    {        
    }
    
    public void ToMenu()
    {
        SceneManager.LoadScene ("menu");
        Debug.Log("down");
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
