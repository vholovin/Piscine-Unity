using UnityEngine;

public class MusicGameplay : MonoBehaviour
{
    public AudioClip[] musics;
    private bool isPlay;
    private bool isToggleChange;

    void Start()
    {
        GetComponent<AudioSource>().clip = musics[Random.Range(0, musics.Length)];
        isPlay = true;
        isToggleChange = true;
    }

    void Update()
    {
        if (isPlay == true && isToggleChange == true)
        {
            gameObject.GetComponent<AudioSource>().Play();
            isToggleChange = false;
        }

        if (isPlay == false && isToggleChange == true)
        {
            isToggleChange = false;
        }
    }
}
