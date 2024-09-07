using UnityEngine;

public class SpeedUI : MonoBehaviour
{
    [HideInInspector] public GameObject BridgeAPI;
    [HideInInspector] public GameObject MenuInfo;

    private void Awake()
    {
        BridgeAPI = GameObject.Find("BridgeAPI");
        MenuInfo = GameObject.Find("Menu");
    }

    public void Pause()
    {
        MenuInfo.GetComponent<MenuUI>().PauseGame();
    }

    public void Play()
    {
        if (MenuInfo.GetComponent<MenuUI>().GetPause() == false)
        {
            BridgeAPI.GetComponent<BridgeAPI>().SetPause(false);
            BridgeAPI.GetComponent<BridgeAPI>().SetSpeed(1);
        }
    }

    public void SpeedX2()
    {
        if (MenuInfo.GetComponent<MenuUI>().GetPause() == false)
        {
            BridgeAPI.GetComponent<BridgeAPI>().SetPause(false);
            BridgeAPI.GetComponent<BridgeAPI>().SetSpeed(5);
        }
    }

    public void SpeedX3()
    {
        if (MenuInfo.GetComponent<MenuUI>().GetPause() == false)
        {
            BridgeAPI.GetComponent<BridgeAPI>().SetPause(false);
            BridgeAPI.GetComponent<BridgeAPI>().SetSpeed(10);
        }
    }
}