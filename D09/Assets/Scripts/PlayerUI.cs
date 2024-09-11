using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    private PlayerManager manager;
    private readonly string managerName = "PlayerManager";


    public Text LifeUI;
    public Scrollbar LifeBarUI;

    private void Awake()
    {
        GameObject managerFind;
        managerFind = GameObject.Find(managerName);

        manager = managerFind.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        UpdateLifeUI();
        UpdateLifeBarUI();
    }

    private void UpdateLifeUI()
    {
        if (LifeUI == null)
        {
            return;
        }

        LifeUI.text = manager.GetCurHP().ToString() + "/" + manager.GetHP().ToString();
    }

    private void UpdateLifeBarUI()
    {
        if (LifeBarUI == null)
        {
            return;
        }

        LifeBarUI.size = manager.GetCurHP() / manager.GetHP();
    }

}
