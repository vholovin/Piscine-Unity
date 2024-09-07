using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Canvas canvasWin;

    public Canvas canvasPause;
    private bool isShowPause = false;

    public AudioClip[] music;
    AudioSource m_MyAudioSource;
    bool m_Play = true;
    bool m_ToggleChange = true;

    public float dampTime = 0.1f;
    public GameObject Hero;

    private bool isPlayingCoroutine = false;
    private readonly uint pauseTimeCoroutine = 10;
    Scene m_Scene;
   
    void Start ()
    {
        UpdateCameraPosition();

        GetComponent<AudioSource>().clip = music[Random.Range(0, music.Length)];
        m_MyAudioSource = GetComponent<AudioSource>();
        m_Play = true;

        canvasPause.gameObject.SetActive(isShowPause);
        canvasWin.gameObject.SetActive(false);
    }
      
    IEnumerator WinSound() 
    {
        m_MyAudioSource.clip = canvasWin.GetComponent<AudioSource>().clip;
        GetComponent<AudioSource>().PlayOneShot(m_MyAudioSource.clip);
        yield return  new WaitForSeconds (pauseTimeCoroutine);
    }
            
    void Update()
    {
        UpdateCameraPosition();
        UpdateCanvas();
        PlayMusics();
        KeyboardKeys();
    }


    private void UpdateCameraPosition()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 target = Hero.GetComponent<CharacterControllerPlayer>().GetCharacterPosition();

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, dampTime);
    }

    private void PlayMusics()
    {
        if (m_Play == true && m_ToggleChange == true)
        {
            m_MyAudioSource.Play();
            m_ToggleChange = false;
        }

        if (m_Play == false && m_ToggleChange == true)
        {
            m_ToggleChange = false;
        }
    }

    private void UpdateCanvas()
    {
        if (Hero.GetComponent<CharacterControllerPlayer>().GetFinish())
        {

            m_Scene = SceneManager.GetActiveScene();
            if (m_Scene.name == "level0")
            {
                if (isPlayingCoroutine == false)
                {
                    isPlayingCoroutine = true;
                    StartCoroutine(WinSound());
                }

                m_Play = false;
                m_ToggleChange = true;
                SceneManager.LoadScene("level1");
            }
            else
            {
                if (isPlayingCoroutine == false)
                {
                    isPlayingCoroutine = true;
                    StartCoroutine(WinSound());
                }

                m_Play = false;
                m_ToggleChange = true;

                canvasWin.gameObject.SetActive(true);
            }

        }

    }

    private void KeyboardKeys()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isShowPause == false)
            {
                isShowPause = true;
            }
            else
            {
                isShowPause = false;
            }

            canvasPause.gameObject.SetActive(isShowPause);
        }
        
    }
}
