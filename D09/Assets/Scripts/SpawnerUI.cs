using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerUI : MonoBehaviour
{
    private SpawnerManager manager;
    private readonly string managerName = "SpawnerManager";

    public Text WaveUI;
    public Text TimeUI;

    public GameObject MessageUI;
    public string[] MessageTextVariant = {"First wave!", "Next wave!", "Last wave!"};
    public float MessageTimeShow = 2f;
    public float MessageFadeSpeed = 0.5f;

    private Text messageText;
    private CanvasGroup messageCanvas;


    private void Awake()
    {
        GameObject managerFind;
        managerFind = GameObject.Find(managerName);

        manager = managerFind.GetComponent<SpawnerManager>();
    }

    private void Start()
    {
        if (MessageUI == null)
        {
            return;
        }
        messageText = MessageUI.GetComponent<Text>();
        messageCanvas = messageText.GetComponent<CanvasGroup>();    
    }

    private void Update()
    {
        UpdateWaveUI();
        UpdateTimeUI();
        UpdateMessageUI();
    }

    private void UpdateWaveUI()
    {
        if (WaveUI == null)
        {
            return;
        }

        WaveUI.text = manager.GetCurWave().ToString() + "/" + manager.GetMaxWave().ToString();
    }

    private void UpdateTimeUI()
    {
        if (TimeUI == null)
        {
            return;
        }

        uint time = (uint)((manager.GetTimerWave() - manager.GetTimer()) * 100.0f);
        uint minutes = time / (60 * 100);
        uint seconds = (time % (60 * 100)) / 100;
        uint hundredths = time % 100;

        TimeUI.text = String.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths) + "s";
    }

    private void UpdateMessageUI()
    {
        if (MessageUI == null)
        {
            return;
        }

        UpdateFadeMessage();
        UpdateTextMessage();
    }

    private void UpdateFadeMessage()
    {
        if (manager.GetTimer() < MessageTimeShow)
        {
            messageCanvas.alpha = 1;
        }
        else
        {
            if (messageCanvas.alpha >= 0)
            {
                messageCanvas.alpha -= Time.deltaTime * MessageFadeSpeed;
            }
        }
    }

    private void UpdateTextMessage()
    {
        if (messageCanvas.alpha <= 0)
        {
            return;
        }

        if (MessageTextVariant == null)
        {
            return;
        }

        if (MessageTextVariant.Length <= 2)
        {
            return;
        }

        if (manager.GetCurWave() == 1)
        {
            messageText.text = MessageTextVariant[0];
        }
        else if (manager.GetCurWave() < manager.GetMaxWave())
        {
            messageText.text = MessageTextVariant[1];
        }
        else if (manager.GetCurWave() == manager.GetMaxWave())
        {
            messageText.text = MessageTextVariant[2];
        }

    }
}
