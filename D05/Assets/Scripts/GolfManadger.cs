using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GolfManadger : MonoBehaviour
{
    public GameObject Golf;
    public Vector3 GolfStartPositionOffset = new (0.0f, 0.1f, 0.0f);
    public GameObject Player;
    public Vector3 PlayerStartPositionOffset = new(0.0f, 2.0f, 0.0f);

    public GameObject[] Starts = new GameObject[3];
    public GameObject[] Finishs = new GameObject[3];

    public GameObject NumberFields;
    public GameObject NumberShots;

    public GameObject KeyTab;

    public Text[] TabShots = new Text[3];
    public Text[] TabScore = new Text[3];

    public GameObject KeyEnter;
    public float TimeDelayEnter = 3.0f;
    private bool isCoroutine = true;

    private readonly int[] shots = new int[3];
    private readonly int[] score = new int[3];
    public int StartScore = -15;

    private int iter = 0;

    public GameObject SpeedGameText;
    public uint SpeedGame1 = 1;
    public uint SpeedGame2 = 10;

    private void Start()
    {
        KeyEnter.SetActive(false);
        KeyTab.SetActive(false);
        SetPositionGolfAndCamera();
        SetScoreAndShots();
    }

    private void Update()
    {
        CheckSticks();
        CheckInWater();
        UpdateTexts();
        ResetPosition();
        SetSpeed();
    }

    IEnumerator WinSoundEffect()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(TimeDelayEnter);
    }
    private void CheckSticks()
    {
        if (Finishs[iter].GetComponent<StickScript>().GetInStick() == true && isCoroutine == true)
        {
            isCoroutine = false;

            KeyEnter.SetActive(true);
            Golf.SetActive(false);

            StartCoroutine(WinSoundEffect());
        }

        if (Input.GetKey(KeyCode.Return) && isCoroutine == false)
        {
            isCoroutine = true;
            Finishs[iter].GetComponent<StickScript>().SetInStick(false);

            iter++;
            if (iter >= Finishs.Length)
            {
                iter = 0;
            }

            KeyEnter.SetActive(false);
            Golf.SetActive(true);
            SetPositionGolfAndCamera();
            SetScoreAndShots();
        }
    }

    public void AddShotAndScore()
    {
        shots[iter]++;
        score[iter] += 5;
    }

    private void SetPositionGolfAndCamera()
    {
        Player.transform.position = Starts[iter].transform.position + PlayerStartPositionOffset;

        Vector3 relativePos = Finishs[iter].transform.position - Player.transform.position;
        Player.transform.rotation = Quaternion.LookRotation(relativePos);

        Golf.transform.position = Starts[iter].transform.position + GolfStartPositionOffset;
        Golf.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void SetScoreAndShots()
    {
        shots[iter] = 0;
        score[iter] = StartScore;
    }

    private void CheckInWater()
    {
        if (Golf.GetComponent<GolfScript>().GetInWater())
        {
            Golf.GetComponent<GolfScript>().SetInWater(false);
            SetPositionGolfAndCamera();
        }
    }

    private void UpdateTexts()
    {
        NumberFields.GetComponent<Text>().text = (iter + 1).ToString() + "/" + Finishs.Length.ToString();
        NumberShots.GetComponent<Text>().text = shots[iter].ToString();

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.I))
        {
            KeyTab.SetActive(true);
            if (Finishs[iter].GetComponent<StickScript>().GetInStick() == true)
            {
                KeyEnter.SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.I))
        {
            for (int i = 0; i < shots.Length; i++)
            {
                TabShots[i].text = shots[i].ToString();
                TabScore[i].text = score[i].ToString();
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.I))
        {
            KeyTab.SetActive(false);
            if (Finishs[iter].GetComponent<StickScript>().GetInStick() == true)
            {
                KeyEnter.SetActive(true);
            }
        }
    }

    private void ResetPosition()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SetPositionGolfAndCamera();
        }
    }

    private void SetSpeed()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Time.timeScale = SpeedGame1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            Time.timeScale = SpeedGame2;
        }

        SpeedGameText.GetComponent<Text>().text = Time.timeScale.ToString();
    }
}
