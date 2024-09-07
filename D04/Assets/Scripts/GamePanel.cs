using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public PlayerProfile Profile;

    public Text TimerText;
    public Text RingsText;

    private float Timer = 0.0f;
    private int Rings = 0;
    private int Score = 0;

    public GameObject PauseMenu;
    public GameObject FinishMenu;

    public Text FinishTimerText;
    public Text FinishRingsText;
    public Text FinishScoreText;

    public int ScoreForRings = 100;
    public int ScoreForSec = 100;
    public int MaxSecForScore = 200;

    private bool isPause = false;
    private bool isFinish = false;

    private readonly float TimeDelayNextLevel = 6.0f;

    private void Start()
    {
        UpdateRings();
        PauseMenu.SetActive(false);
        FinishMenu.SetActive(false);
    }

    void Update()
    {
        GetKeyDown();

        if (!isPause || !isFinish)
        {
            UpdateTimer();
        }
    }

    private void GetKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            SetPause();
        }
    }

    public void ButtonContinue()
    {
        SetPause();
    }

    public void ButtonExit()
    {
        SceneManager.LoadScene(0);
    }

    private void SetPause()
    {
        isPause = !isPause;
        PauseMenu.SetActive(isPause);

        if (isPause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public bool GetMove()
    {
        if (isPause)
        {
            return false;
        }

        return true;
    }

    private void UpdateTimer()
    {
        Timer += Time.deltaTime;
        float minutes = Mathf.FloorToInt(Timer / 60);
        float seconds = Mathf.FloorToInt(Timer % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateRings()
    {
        RingsText.text = Rings.ToString();
    }

    public void AddRing()
    {
        Rings++;
        UpdateRings();
    }

    public void Finish()
    {
        if (!isFinish)
        {
            isFinish = true;
            StartCoroutine(ShowResultAndNextLevel());
        }
    }

    IEnumerator ShowResultAndNextLevel()
    {
        CalculateScore();
        ShowFinishMenu();
        SaveProgressInProfile();

        yield return new WaitForSeconds(TimeDelayNextLevel);

        UploadNextLevel();
    }

    private void CalculateScore()
    {
        Score += (Rings * ScoreForRings);

        if ((int)Timer < MaxSecForScore)
        {
            Score += ((int)Timer * ScoreForSec);
        }
        else
        {
            Score += (MaxSecForScore * ScoreForSec);
        }
    }

    private void ShowFinishMenu()
    {
        FinishMenu.SetActive(true);

        float minutes = Mathf.FloorToInt(Timer / 60);
        float seconds = Mathf.FloorToInt(Timer % 60);
        FinishTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        FinishRingsText.text = Rings.ToString();

        FinishScoreText.text = Score.ToString();
    }

    private void SaveProgressInProfile()
    {
        Profile.ReadProfile();

        int index = SceneManager.GetActiveScene().buildIndex - Profile.GetBuildIndexOffset();
        Profile.SaveNewResult(index, Score, Rings);
    }

    private void UploadNextLevel()
    {
        int nextIndexInBuild = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndexInBuild < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndexInBuild);
        }
        else
        {
            Debug.Log("does not have scene in builds");
            SceneManager.LoadScene(0);
        }
    }
}