using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        UnitRankSetting();        
    }

    public MonsterSpawner monsterSpawner = null;
    public UnitSpawner unitSpawner = null;
    public UIManager uIManager = null;
    public SelectManager selectManager = null;
    
    public int[] unitRank = new int[3];
    public int gold = 100;

    public void UnitRankSetting()
    {
        for (int i = 0; i < unitRank.Length; i++)
        {
            unitRank[i] = 1;
        }
    }
}
