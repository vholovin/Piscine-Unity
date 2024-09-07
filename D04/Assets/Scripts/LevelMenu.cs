using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public PlayerProfile Profile;

    public Text PlayerRings;

    public Image[] RedBorderOfLevels;
    public Image[] UnlockOfLevels;
    private int Index = 0;
    private readonly float minValue = 0.0f;
    private readonly float middleValue = 0.5f;
    private readonly float maxValue = 1.0f;

    public Text CurrentLevelNumber;
    public Text CurrentLevelScore;


    void Start()
    {
        Profile.ReadProfile();

        PlayerRings.text = Profile.GetCountOfRingsPlayers().ToString();

        ChangeLevelSelection(Index);
        UnlockLevel();
    }

    void Update()
    {
        GetKeysDown();
        GetLevelScore();
    }

    private void GetKeysDown()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Index--;

            if (Index < 0)
            {
                Index = RedBorderOfLevels.Length - 1;
            }

            ChangeLevelSelection(Index);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Index++;

            if (Index >= RedBorderOfLevels.Length)
            {
                Index = 0;
            }

            ChangeLevelSelection(Index);
        }
        else if (Input.GetKeyDown(KeyCode.Return) && Profile.GetUnlockLevel(Index) == 1)
        {
            int indexInBuild = Index + Profile.GetBuildIndexOffset();

            if (indexInBuild < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(indexInBuild);
            }
            else
            {
                Debug.Log("does not have scene in builds");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void GetLevelScore()
    {
        CurrentLevelScore.text = Profile.GetScoreLevel(Index).ToString();
        CurrentLevelNumber.text = (Index + 1).ToString();
    }

    void ChangeLevelSelection(int newIndex)
    {
        for (int i = 0; i < RedBorderOfLevels.Length; i++)
        {
            Color redBorder = RedBorderOfLevels[i].GetComponent<Image>().color;

            if (i != newIndex)
            {
                redBorder.a = minValue;
            }
            else
            {
                redBorder.a = maxValue;
            }

            RedBorderOfLevels[i].color = redBorder;
        }
    }

    void UnlockLevel()
    {
        for (int i = 0; i < UnlockOfLevels.Length; i++)
        {
            Color unlock = UnlockOfLevels[i].GetComponent<Image>().color;

            if (Profile.GetUnlockLevel(i) == 1)
            {
                unlock.a = minValue;
            }
            else
            {
                unlock.a = middleValue;
            }

            UnlockOfLevels[i].color = unlock;
        }
    }
}