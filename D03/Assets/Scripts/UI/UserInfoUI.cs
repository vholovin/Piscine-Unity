using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    private GameObject BridgeAPI;
    public Text TextHP;
    public Text TextEnergy;

    private void Awake()
    {
        BridgeAPI = GameObject.Find("BridgeAPI");
    }

    private void Update()
    {
        int HP = BridgeAPI.GetComponent<BridgeAPI>().GetHP();
        if (HP < 0)
        {
            HP = 0;
        }
        TextHP.text = HP.ToString();
        TextEnergy.text = BridgeAPI.GetComponent<BridgeAPI>().GetEnergy().ToString();
    }
}
