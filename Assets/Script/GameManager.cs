using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }


    public MonsterSpawner monsterSpawner = null;
    public int[] unitRank = new int[3];

    public static implicit operator GameManager(ScenesManager v)
    {
        throw new NotImplementedException();
    }
}
