using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //���̺꺰�� ������.
    //�迭�� �ؼ� ���͵� ���� 
    public MonsterWave[] waves = new MonsterWave[60];

    //���� ���̺� ��
    public int waveCount = 0;

    //���� �迭�� ���͸� ������ �ִ´�?
    private MonsterGameObject monsterGameObject = new MonsterGameObject();

    //���͵��� ����� �θ� parent
    [SerializeField]
    private GameObject monsterParent = null;

    //������ ���ڵ�
    private List<int> excludedNumbers = new List<int>();

    //�����̺�� �ִ� 10����
    private int maxCount = 10;

    //�������� ���� ����
    private int pickNum = 0;

    private float nextWaveTime = 60.0f;
    public int monsterCount = 0;

    private GameStateEnum gameState = GameStateEnum.Win;

    private void Awake()
    {
        GameManager.instance.monsterSpawner = this;
        //�����۾� ���� que�� �����Ѵ�.
        MonsterSetting();
    }
    private enum GameStateEnum
    {
        Win, Lose
    }
    private void Start()
    {
        //1�и��� ���� ���̺긦 �����Ѵ�.
        StartCoroutine(CoolWave());
        //Time.timeScale =4f;
    }
    //1���� ������ �ٽ� ���̺� ������
    private IEnumerator CoolWave()
    {
        
        while (waveCount < 3 || gameState == GameStateEnum.Lose)
        {
            StartCoroutine(StartMonsterWave());
            GameManager.instance.uIManager.UpdateTimeBar();
            yield return new WaitForSeconds(nextWaveTime);
            Debug.Log("���ο� ���̺갡 ���۵˴ϴ�.");
            waveCount++;
        }

        //�й�
        if (GameStateEnum.Lose == gameState)
        {
            ScenesManager.Instance.QuitGame(() => { GameManager.instance.uIManager.winLoseObj[1].SetActive(true); });
            
        }
        //�¸� 
        else if (GameStateEnum.Win == gameState)
        {
            ScenesManager.Instance.QuitGame(() => { GameManager.instance.uIManager.winLoseObj[0].SetActive(true); });
        }
        Debug.Log("�������̺� ����");
    }

    //�ڷ�ƾ ���� �����ϱ�
    private IEnumerator StartMonsterWave()
    {
        //����Ʈ �ʱ�ȭ
        excludedNumbers.Clear();

        //���Ͱ� �ٴڳ� ������ �ݺ��Ѵ�.
        //for���� ���鼭 0�ΰ͵��� ������ �׷��� �̸� ������ ���ڵ��� ����Ʈ�� �߰��� ���̺갡 ���۵ɶ� ����Ʈ�� �ʱ�ȭ�ȴ�.
        for (int i = 0; i < waves[waveCount].monsterWaveInforms.Length; i++)
        {
            if (waves[waveCount].monsterWaveInforms[i] == 0)
            {
                excludedNumbers.Add(i);
            }
        }

        //0.1�ʸ��� ���Ͱ� �����ȴ�.
        while (true)
        {
            //Ư�� ���ڸ� ������ ������ ��ü�� ��ȯ�Ѵ�.
            pickNum = GenerateRandomNumber(0, 3);

            if (pickNum == -1) { Debug.Log("���Ͱ� ��� �����Ǿ����ϴ�."); break; }
            //���͸� ��ȯ
            SpawnerMonster(pickNum);

            yield return new WaitForSeconds(1f);
        }
        Debug.Log("���̺갡 ����Ǿ����ϴ�.");
    }

    //Ư�����ڸ� �����ϰ� ���ڸ� �̴´�.
    private int GenerateRandomNumber(int min, int max)
    {
        List<int> validNumbers = new List<int>();

        // ��ȿ�� ���� ����Ʈ ����
        for (int i = min; i <= max; i++)
        {
            if (!excludedNumbers.Contains(i))
            {
                validNumbers.Add(i);
            }
        }

        // ��ȿ�� ���ڰ� ���ٸ� 0 ��ȯ (�Ǵ� �ٸ� ó��)
        if (validNumbers.Count == 0)
        {
            Debug.LogWarning("No valid numbers available to choose from.");
            return -1; // �Ǵ� �ٸ� �⺻��
        }

        // �������� ���� ����
        int randomIndex = Random.Range(0, validNumbers.Count);
        return validNumbers[randomIndex];
    }

    //�� ���̺� ���� �ִ� ���� ���ڰ� �־� 

    //���� �����ϱ�
    public void SpawnerMonster(int num)
    {
        //���͸� Ȱ��ȭ�� ��Ű�� �ɵ� 
        switch (num)
        {
            case 0:
                // �� ���̺�� �ִ� ���̺� ���� - ���� ������ ���� ī��Ʈ�� ���� �� ���͸� ����ϸ� �ɵ�
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
                Debug.Log("�� �� ���� �����Դϴ�.");
                break;
        }

        //��ȯ�� ���� ���ڸ� �����Ѵ�.
        waves[waveCount].monsterWaveInforms[num] -= 1;

        //���� ������ 0�̶�� �ش� ���͸� ���� ��Ͽ� �߰��Ѵ�.
        if (waves[waveCount].monsterWaveInforms[num] == 0) excludedNumbers.Add(num);

        monsterCount++;
        //100������ ������ ����
        if (monsterCount >= 100)
        {
            gameState = GameStateEnum.Lose;
            monsterCount = 0;
        }
    }

    //���Ͱ� �������� Ȯ���ϰ� �����ϸ� �����Ѵ�.
    private void DequeMonster(Queue<GameObject> que, int num)
    {
        //���� que�� ���Ͱ� ������ �����ϰ� que�� ����
        if (que.Count == 0)
        {
            GameObject obj = Instantiate(monsterParent.transform.GetChild(num).gameObject);

            //�ڽļ����� �ڵ������ 0 ���۰� 1������ �޶� +1 �߰��Ѵ�.
            ReInputMonster(obj, num + 1);
        };
        //���͸� ������ Ȱ��ȭ
        que.Dequeue().SetActive(true);
    }
    //��Ÿ�� ���Ŀ� ���� ���̺� ����
    private IEnumerator CorutineNextWave()
    {
        yield return null;
        StartMonsterWave();
    }

    //�ʹ� ���� ��ü ����
    private void MonsterSetting()
    {
        //���� 1���� 1�� que�� ����
        MonsterInsert(0, monsterGameObject.monster1);
        MonsterInsert(1, monsterGameObject.monster2);
        MonsterInsert(2, monsterGameObject.monster3);
        monsterGameObject.monster4 = monsterParent.transform.GetChild(3).gameObject;
    }

    //que�� ���͸� ����
    private void MonsterInsert(int childNum, Queue<GameObject> que)
    {
        for (int i = 0; i < monsterParent.transform.GetChild(childNum).childCount; i++)
        {
            que.Enqueue(monsterParent.transform.GetChild(childNum).GetChild(i).gameObject);
        }
    }
    //���Ͱ� �װų� �ܺο������� �Ҹ��� ��� �ٽ� que�� ����
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
                Debug.Log("���� ����");
                break;
            default:
                Debug.LogError($"�� �� ���� ���Ͱ� ���� �ڵ������ {codeName} �Դϴ�");
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
    [Header("���� 1")]
    public Queue<GameObject> monster1 = new Queue<GameObject>();
    [Header("���� 2")]
    public Queue<GameObject> monster2 = new Queue<GameObject>();
    [Header("���� 3")]
    public Queue<GameObject> monster3 = new Queue<GameObject>();
    [Header("��������")]
    public GameObject monster4 = null;
}

