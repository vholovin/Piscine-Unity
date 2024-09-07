using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private enum GameStatus { LOOSE, GAME, WIN };
    private GameStatus Status = GameStatus.GAME;

    [HideInInspector] public GameObject BridgeAPI;
    [HideInInspector] public bool isPause = false;

    public GameObject MenuForm1;
    public GameObject MenuForm2;
    public GameObject LooseMenu;
    public GameObject WinMenu;

    private void Awake()
    {
        BridgeAPI = GameObject.Find("BridgeAPI");
    }

    void Start()
    {
        if (MenuForm1 != null)
        {
            MenuForm1.SetActive(false);
        }
        if (MenuForm2 != null)
        {
            MenuForm2.SetActive(false);
        }
        if (LooseMenu != null)
        {
            LooseMenu.SetActive(false);
        }
        if (WinMenu != null)
        {
            WinMenu.SetActive(false);
        }
    }

    void Update()
    {
        if (BridgeAPI.GetComponent<BridgeAPI>().GetHP() <= 0)
        {
            Status = GameStatus.LOOSE;
        }
        else if (BridgeAPI.GetComponent<BridgeAPI>().CheckLastEnemy() == true)
        {
            Status = GameStatus.WIN;
        }

        if (Status == GameStatus.LOOSE)
        {
            LooseGame();
        }
        else if (Status == GameStatus.WIN)
        {
            WinGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            PauseGame();
        }

    }

    public bool GetPause()
    {
        return (isPause);
    }

    public void PauseGame()
    {
        if (isPause == false)
        {
            isPause = true;
            BridgeAPI.GetComponent<BridgeAPI>().SetPause(true);
            MenuForm1.SetActive(true);
        }
        else if (isPause == true)
        {
            isPause = false;
            BridgeAPI.GetComponent<BridgeAPI>().SetPause(false);
            MenuForm1.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isPause = false;
        MenuForm1.SetActive(false);
        MenuForm2.SetActive(false);
        BridgeAPI.GetComponent<BridgeAPI>().SetPause(false);
    }

    public void ExitGame()
    {
        MenuForm1.SetActive(false);
        MenuForm2.SetActive(true);
    }

    public void ExitYesGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitNoGame()
    {
        MenuForm2.SetActive(false);
        MenuForm1.SetActive(true);
    }

    public void LooseGame()
    {
        isPause = true;
        BridgeAPI.GetComponent<BridgeAPI>().GameOver();
        MenuForm1.SetActive(false);
        MenuForm2.SetActive(false);
        WinMenu.SetActive(false);
        LooseMenu.SetActive(true);
    }

    public void LooseYesGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LooseNoGame()
    {
        SceneManager.LoadScene(0);
    }

    public void WinGame()
    {
        isPause = true;
        BridgeAPI.GetComponent<BridgeAPI>().GameWin();
        MenuForm1.SetActive(false);
        MenuForm2.SetActive(false);
        LooseMenu.SetActive(false);
        WinMenu.SetActive(true);
    }

    public void WinYesGame()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void WinNoGame()
    {
        SceneManager.LoadScene(0);
    }
}
