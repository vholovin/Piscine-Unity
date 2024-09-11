using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] Weapon;
    private uint curWeapon = 0;

    public Image CursorUI;
    private Color isNotEnemy = Color.white;
    private Color isEnemy = Color.red;

    //  private bool fireTarget = false;


    private void Start()
    {
        ReloadWeapon(0);
    }

    private void Update()
    {
        GetKeyChoose();
        GetKeyShot();

        UpdateCursorUI();
    }


    private void ReloadWeapon(uint newChoose)
    {
        foreach (GameObject weapon in Weapon)
        {
            weapon.SetActive(false);
        }

        if (newChoose <= Weapon.Length - 1)
        {
            Weapon[newChoose].SetActive(true);
        }
    }


    private void GetKeyChoose()
    {
        if (Input.GetKey(KeyCode.Alpha1) && curWeapon != 0)
        {
            curWeapon = 0;
            ReloadWeapon(curWeapon);
        }
        else if (Input.GetKey(KeyCode.Alpha2) && curWeapon != 1)
        {
            curWeapon = 1;
            ReloadWeapon(curWeapon);
        }
    }


    private void GetKeyShot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Weapon[curWeapon].GetComponent<Weapon>().ToShot();
        }
    }


    private void UpdateCursorUI()
    {
        if (Weapon[curWeapon].GetComponent<Weapon>().GetFireTarget())
        {
            CursorUI.color = isEnemy;
        }
        else
        {
            CursorUI.color = isNotEnemy;
        }
    }
}
