using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //웨이브별로 나눈다.
    //배열로 해서 몬스터들 몬스터 
    public MonsterWave[] waves = new MonsterWave[60];

    //현재 웨이브 수
    public int waveCount = 0;

    //각자 배열로 몬스터를 가지고 있는다?
    private MonsterGameObject monsterGameObject = new MonsterGameObject();

    //몬스터들이 저장된 부모 parent
    [SerializeField]
    private GameObject monsterParent = null;

    //제외할 숫자들
    private List<int> excludedNumbers = new List<int>();

    //각웨이브당 최대 10마리
    private int maxCount = 10;

    //랜덤으로 뽑은 숫자
    private int pickNum = 0;

    private float nextWaveTime = 60.0f;
    public int monsterCount = 0;

    private GameStateEnum gameState = GameStateEnum.Win;

    private void Awake()
    {
        GameManager.instance.monsterSpawner = this;
        //사전작업 몬스터 que에 삽입한다.
        MonsterSetting();
    }
    private enum GameStateEnum
    {
        Win, Lose
    }
    private void Start()
    {
        //1분마다 몬스터 웨이브를 실행한다.
        StartCoroutine(CoolWave());
        //Time.timeScale =4f;
    }
    //1분이 지나고 다시 웨이브 시작함
    private IEnumerator CoolWave()
    {
        
        while (waveCount < 3 || gameState == GameStateEnum.Lose)
        {
            StartCoroutine(StartMonsterWave());
            GameManager.instance.uIManager.UpdateTimeBar();
            yield return new WaitForSeconds(nextWaveTime);
            Debug.Log("새로운 웨이브가 시작됩니다.");
            waveCount++;
        }

        //패배
        if (GameStateEnum.Lose == gameState)
        {
            ScenesManager.Instance.QuitGame(() => { GameManager.instance.uIManager.winLoseObj[1].SetActive(true); });
            
        }
        //승리 
        else if (GameStateEnum.Win == gameState)
        {
            ScenesManager.Instance.QuitGame(() => { GameManager.instance.uIManager.winLoseObj[0].SetActive(true); });
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
                DequeMonster(monsterGameObject.monster1, 0);
                break;
            case 1:
                DequeMonster(monsterGameObject.monster2, 1);
                break;
            case 2:
                DequeMonster(monsterGameObject.monster3, 2);
                break;
            case 3:
                monsterGameObject.monster4.SetActive(true);
                break;
            default:
                Debug.Log("알 수 없는 몬스터입니다.");
                break;
        }

        //소환한 몬스터 숫자를 차감한다.
        waves[waveCount].monsterWaveInforms[num] -= 1;

        //남은 수량이 0이라면 해당 몬스터를 제외 목록에 추가한다.
        if (waves[waveCount].monsterWaveInforms[num] == 0) excludedNumbers.Add(num);

        monsterCount++;
        //100마리가 넘으면 종료
        if (monsterCount >= 100)
        {
            gameState = GameStateEnum.Lose;
            monsterCount = 0;
        }
    }

    //몬스터가 부족한지 확인하고 부족하면 생성한다.
    private void DequeMonster(Queue<GameObject> que, int num)
    {
        //만약 que에 몬스터가 없으면 생성하고 que에 넣음
        if (que.Count == 0)
        {
            GameObject obj = Instantiate(monsterParent.transform.GetChild(num).gameObject);

            //자식순서랑 코드네임은 0 시작과 1시작이 달라서 +1 추가한다.
            ReInputMonster(obj, num + 1);
        };
        //몬스터를 꺼내서 활성화
        que.Dequeue().SetActive(true);
    }
    //쿨타임 이후에 다음 웨이브 실행
    private IEnumerator CorutineNextWave()
    {
        yield return null;
        StartMonsterWave();
    }

    //초반 몬스터 전체 저장
    private void MonsterSetting()
    {
        //몬스터 1번을 1번 que에 저장
        MonsterInsert(0, monsterGameObject.monster1);
        MonsterInsert(1, monsterGameObject.monster2);
        MonsterInsert(2, monsterGameObject.monster3);
        monsterGameObject.monster4 = monsterParent.transform.GetChild(3).gameObject;
    }

    //que에 몬스터를 삽입
    private void MonsterInsert(int childNum, Queue<GameObject> que)
    {
        for (int i = 0; i < monsterParent.transform.GetChild(childNum).childCount; i++)
        {
            que.Enqueue(monsterParent.transform.GetChild(childNum).GetChild(i).gameObject);
        }
    }
    //몬스터가 죽거나 외부요인으로 소멸할 경우 다시 que에 삽입
    public void ReInputMonster(GameObject obj, int codeName)
    {
        switch (codeName)
        {
            case 1:
                monsterGameObject.monster1.Enqueue(obj);
                break;
            case 2:
                monsterGameObject.monster2.Enqueue(obj);
                break;
            case 3:
                monsterGameObject.monster3.Enqueue(obj);
                break;
            case 4:
                Debug.Log("보스 죽음");
                break;
            default:
                Debug.LogError($"알 수 없는 몬스터가 죽음 코드네임은 {codeName} 입니다");
                break;
        }

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
    public Queue<GameObject> monster1 = new Queue<GameObject>();
    [Header("몬스터 2")]
    public Queue<GameObject> monster2 = new Queue<GameObject>();
    [Header("몬스터 3")]
    public Queue<GameObject> monster3 = new Queue<GameObject>();
    [Header("보스몬스터")]
    public GameObject monster4 = null;
}

