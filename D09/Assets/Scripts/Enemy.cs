using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public uint HP = 100;
    public uint Damage = 10;

    private int curHP;



    private void Start()
    {
        curHP = (int)HP;
    }

    public void GetDamage(uint damage)
    {
        curHP -= (int)damage;

        if (curHP <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void addHP(uint newHP)
    {
        HP += newHP;
        curHP = (int)HP;
    }

    public void addDamage(uint newDamage)
    {
       Damage += newDamage;
    }
}
