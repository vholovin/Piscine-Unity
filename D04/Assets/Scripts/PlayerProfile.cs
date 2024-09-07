using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public string CountOfRings_Text = "CountOfRings";
    public int CountOfRings_Default = 0;
    private int CountOfRings;

    public string CountLevels_Text = "CountLevels";
    public int CountLevels_Default = 3;
    private int CountLevels;

    public string[] UnlockLevels_Text = { "UnlockLevel_1", "UnlockLevel_2", "UnlockLevel_3" };
    public int[] UnlockLevels_Default = { 1, 0, 0 };
    private int[] UnlockLevels;

    public string[] ScoreLevels_Text = { "ScoreLevel_1", "ScoreLevel_2", "ScoreLevel_3" };
    public int[] ScoreLevels_Default = { 0, 0, 0 };
    private int[] ScoreLevels;

    public string BuildIndexOffset_Text = "BuildIndexOffset";
    public int BuildIndexOffset_Default = 2;
    private int BuildIndexOffset;

    public void ResetProfile()
    {
        PlayerPrefs.SetInt(CountOfRings_Text, CountOfRings_Default);

        PlayerPrefs.SetInt(CountLevels_Text, CountLevels_Default);

        for (int i = 0; i < CountLevels_Default; i++)
        {
            PlayerPrefs.SetInt(UnlockLevels_Text[i], UnlockLevels_Default[i]);
            PlayerPrefs.SetInt(ScoreLevels_Text[i], ScoreLevels_Default[i]);
        }

        PlayerPrefs.SetInt(BuildIndexOffset_Text, BuildIndexOffset_Default);

        PlayerPrefs.Save();
    }

    public void ReadProfile()
    {
        CountOfRings = PlayerPrefs.GetInt(CountOfRings_Text);

        CountLevels = PlayerPrefs.GetInt(CountLevels_Text);

        UnlockLevels = new int[CountLevels];
        ScoreLevels = new int[CountLevels];

        for (int i = 0; i < CountLevels; i++)
        {
            UnlockLevels[i] = PlayerPrefs.GetInt(UnlockLevels_Text[i]);
            ScoreLevels[i] = PlayerPrefs.GetInt(ScoreLevels_Text[i]);
        }

        BuildIndexOffset = PlayerPrefs.GetInt(BuildIndexOffset_Text);
}

    public void DeleteProfile()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("delete player prefs");
    }

    public int GetCountOfRingsPlayers()
    {
        return CountOfRings;
    }

    public int GetUnlockLevel(int index)
    {
        return UnlockLevels[index];
    }

    public int GetScoreLevel(int index)
    {
        return ScoreLevels[index];
    }

    public int GetBuildIndexOffset()
    {
        return BuildIndexOffset;
    }

    public void SaveNewResult(int index, int score, int rings)
    {
        PlayerPrefs.SetInt(ScoreLevels_Text[index], score);

        index++;
        if (index < UnlockLevels_Text.Length)
        {
            PlayerPrefs.SetInt(UnlockLevels_Text[index], 1);
        }

        int newCountOfRings = GetCountOfRingsPlayers() + rings;
        PlayerPrefs.SetInt(CountOfRings_Text, newCountOfRings);
    }
}