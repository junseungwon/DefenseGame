using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //웨이브별로 나눈다.
    //배열로 해서 몬스터들 몬스터 
    public MonsterWave[] waves = new MonsterWave[60];
    public int waveCount = 0;

    //각자 배열로 몬스터를 가지고 있는다?
    public MonsterGameObject monsterGameObject = null;
    //각웨이브당 최대 10마리
    private int maxCount = 10;
    //제외할 숫자들
    private List<int> excludedNumbers = new List<int>();

    private int pickNum = 0;

    private void Start()
    {
        //1분마다 몬스터 웨이브를 실행한다.
        StartCoroutine(CoolWave());
    }

    //1분이 지나고 다시 웨이브 시작함
    private IEnumerator CoolWave()
    {
        while (waveCount < 2)
        {
            StartCoroutine(StartMonsterWave());
            yield return new WaitForSeconds(15f);
            waveCount++;
        }
        Debug.Log("최종웨이브 종료");
    }

    //코루틴 몬스터 생성하기
    private IEnumerator StartMonsterWave()
    {
        //리스트 초기화
        excludedNumbers.Clear();

        //몬스터가 바닥날 때까지 반복한다.
        //for문을 돌면서 0인것들을 조사함 그래서 미리 제외할 숫자들을 리스트에 추가함 웨이브가 시작될때 리스트는 초기화된다.
        for (int i = 0; i < waves[waveCount].monsterWaveInforms.Length; i++)
        {
            if (waves[waveCount].monsterWaveInforms[i] == 0)
            {
                excludedNumbers.Add(i);
            }
        }

        //0.1초마다 몬스터가 생성된다.
        while (true)
        {
            //특정 숫자를 제외한 나머지 물체를 소환한다.
            pickNum = GenerateRandomNumber(0, 3);

            if (pickNum == -1) { Debug.Log("몬스터가 모두 소진되었습니다."); break; }

            //몬스터를 소환
            SpawnerMonster(pickNum);

            yield return new WaitForSeconds(1f);
        }

        Debug.Log("웨이브가 종료되었습니다.");

    }

    //특정숫자를 제외하고 숫자를 뽑는다.
    private int GenerateRandomNumber(int min, int max)
    {
        List<int> validNumbers = new List<int>();

        // 유효한 숫자 리스트 생성
        for (int i = min; i <= max; i++)
        {
            if (!excludedNumbers.Contains(i))
            {
                validNumbers.Add(i);
            }
        }

        // 유효한 숫자가 없다면 0 반환 (또는 다른 처리)
        if (validNumbers.Count == 0)
        {
            Debug.LogWarning("No valid numbers available to choose from.");
            return -1; // 또는 다른 기본값
        }

        // 랜덤으로 숫자 선택
        int randomIndex = Random.Range(0, validNumbers.Count);
        return validNumbers[randomIndex];
    }

    //각 웨이브 별로 최대 몬스터 숫자가 있어 

    //몬스터 생성하기
    public void SpawnerMonster(int num)
    {
        //몬스터를 활성화만 시키면 될듯 
        switch (num)
        {
            case 0:
                // 각 웨이브당 최대 웨이브 숫자 - 현재 몬스터의 남은 카운트를 빼면 그 몬스터를 사용하면 될듯
                monsterGameObject.monster1[maxCount - waves[waveCount].monsterWaveInforms[0]].SetActive(true);
                break;
            case 1:
                monsterGameObject.monster2[maxCount - waves[waveCount].monsterWaveInforms[1]].SetActive(true);
                break;
            case 2:
                monsterGameObject.monster3[maxCount - waves[waveCount].monsterWaveInforms[2]].SetActive(true);
                break;
            case 3:
                monsterGameObject.bossMonster.SetActive(true);
                break;
            default:
                Debug.Log("알 수 없는 몬스터입니다.");
                break;
        }

        //소환한 몬스터 숫자를 차감한다.
        waves[waveCount].monsterWaveInforms[num] -= 1;

        //남은 수량이 0이라면 해당 몬스터를 제외 목록에 추가한다.
        if (waves[waveCount].monsterWaveInforms[num] == 0) excludedNumbers.Add(num);
    }

    //쿨타임 이후에 다음 웨이브 실행
    private IEnumerator CorutineNextWave()
    {
        yield return null;
        StartMonsterWave();
    }
}

[System.Serializable]
public class MonsterWave
{
    public int[] monsterWaveInforms = new int[4];
}
[System.Serializable]
public class MonsterGameObject
{
    [Header("몬스터 1")]
    public GameObject[] monster1 = new GameObject[10];
    [Header("몬스터 2")]
    public GameObject[] monster2 = new GameObject[10];
    [Header("몬스터 3")]
    public GameObject[] monster3 = new GameObject[10];
    [Header("보스몬스터")]
    public GameObject bossMonster = null;
}

