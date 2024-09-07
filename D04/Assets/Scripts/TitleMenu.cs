using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public PlayerProfile Profile;

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetProfile()
    {
        Profile.DeleteProfile();
        Profile.ResetProfile();
    }
}
