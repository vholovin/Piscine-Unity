using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public uint HP = 100;
    private int curHP;

    private void Start()
    {
        curHP = (int)HP;
    }

    public int GetCurHP()
    {
        return curHP;
    }

    public uint GetHP()
    {
        return HP;
    }


}
