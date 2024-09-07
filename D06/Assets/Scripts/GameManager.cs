using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject Player;

	private float valueDanger = 0;
	private bool isDanger = false;
	
	public AudioClip SoundGameNormal;
	public AudioClip SoundGameDanger;
	
	public GameObject Slider;
	public float MinValueSlider = 0.0f;
	public float DangerValueSlider = 75.0f;
    public float MaxValueSlider = 100.0f;

    public GameObject SliderColor;
	public Color NonDangerColor = Color.white;
    public Color DangerColor = Color.red;

	public float StepForLight = 0.1f;
    public float StepForCamera = 1.0f;
    public float StepForRelax = 0.1f;

    public GameObject[] Megaphones;
	private bool isPlayedMegaphones = false;
	
	public AudioClip EndGame;
	public float TimeForEndGame = 5.0f;
    private bool isEnd = false;

	private float timer = 0.0f;

	private void Start()
	{
		Slider.GetComponent<Slider>().minValue = MinValueSlider;
		Slider.GetComponent<Slider>().maxValue = MaxValueSlider;

		Slider.GetComponent<Slider>().value = valueDanger;	
		SliderColor.GetComponent<Image>().color = NonDangerColor;

		gameObject.GetComponent<AudioSource>().clip = SoundGameNormal;
		gameObject.GetComponent<AudioSource>().Play();
	}

	private void Update()
	{
		UpdateTimer();
		GetDanger();
		PlaySound();
		PlayGame();		
	}

	private void UpdateTimer()
	{
        timer += Time.deltaTime;
    }

	private void GetDanger()
	{
		if (Player.GetComponent<PlayerController>().LightDetected && valueDanger <= DangerValueSlider)
		{
            valueDanger += StepForLight;
        }

        if (Player.GetComponent<PlayerController>().CameraDetected && valueDanger <= MaxValueSlider)
		{
            valueDanger += StepForCamera;
        }

		if (!Player.GetComponent<PlayerController>().LightDetected &&
			!Player.GetComponent<PlayerController>().CameraDetected &&
			valueDanger >= MinValueSlider && !isEnd)
		{
            valueDanger -= StepForRelax;
        }

		Slider.GetComponent<Slider>().value = valueDanger;
	}

	private void PlaySound()
	{
		bool oldStatus = isDanger;
		bool oldMegaphones = isPlayedMegaphones;

		if (valueDanger < DangerValueSlider)
		{ 
			SliderColor.GetComponent<Image>().color = NonDangerColor;
			isDanger = false;
			isPlayedMegaphones = false;
		}
		else if (valueDanger >= DangerValueSlider && valueDanger < MaxValueSlider)
		{
			SliderColor.GetComponent<Image>().color = DangerColor;
			isDanger = true;
			isPlayedMegaphones = false;
		}
		else if (valueDanger >= MaxValueSlider)
		{
			isPlayedMegaphones = true;
			isEnd = true;
		}


		if (isDanger != oldStatus)
		{
			if (isDanger)
			{ 
				gameObject.GetComponent<AudioSource>().clip = SoundGameDanger;
			}
			else if (!isDanger)
			{
				gameObject.GetComponent<AudioSource>().clip = SoundGameNormal;
				for (int i = 0; i < Megaphones.Length; i++)
				{
                    Megaphones[i].GetComponent<AudioSource>().Stop();
                }
			}
			gameObject.GetComponent<AudioSource>().Play();
		}

		if (isPlayedMegaphones != oldMegaphones)
		{
            for (int i = 0; i < Megaphones.Length; i++)
            {
                Megaphones[i].GetComponent<AudioSource>().Play();
            }
			timer = 0.0f;
		}
	}

	private void PlayGame()
	{
		if (isEnd && timer >= TimeForEndGame)
		{
			SceneManager.LoadScene(0);
		}

		if (Player.GetComponent<PlayerController>().GunInfo && Player.GetComponent<PlayerController>().MapInfo && !isEnd)
		{
			isEnd = true;
			timer = 0f;
			gameObject.GetComponent<AudioSource>().clip = EndGame;
			gameObject.GetComponent<AudioSource>().Play();
		}

//		if (isEnd && gameObject.GetComponent<AudioSource>().isPlaying && valueDanger <= MaxValueSlider && timer >= 4.4f)
//			SceneManager.LoadScene(0);
	}
}
