using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //���̺꺰�� ������.
    //�迭�� �ؼ� ���͵� ���� 
    public MonsterWave[] waves = new MonsterWave[60];
    public int waveCount = 0;

    //���� �迭�� ���͸� ������ �ִ´�?
    public MonsterGameObject monsterGameObject = null;
    //�����̺�� �ִ� 10����
    private int maxCount = 10;
    //������ ���ڵ�
    private List<int> excludedNumbers = new List<int>();

    private int pickNum = 0;

    private void Start()
    {
        //1�и��� ���� ���̺긦 �����Ѵ�.
        StartCoroutine(CoolWave());
    }

    //1���� ������ �ٽ� ���̺� ������
    private IEnumerator CoolWave()
    {
        while (waveCount < 2)
        {
            StartCoroutine(StartMonsterWave());
            yield return new WaitForSeconds(15f);
            waveCount++;
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
                Debug.Log("�� �� ���� �����Դϴ�.");
                break;
        }

        //��ȯ�� ���� ���ڸ� �����Ѵ�.
        waves[waveCount].monsterWaveInforms[num] -= 1;

        //���� ������ 0�̶�� �ش� ���͸� ���� ��Ͽ� �߰��Ѵ�.
        if (waves[waveCount].monsterWaveInforms[num] == 0) excludedNumbers.Add(num);
    }

    //��Ÿ�� ���Ŀ� ���� ���̺� ����
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
    [Header("���� 1")]
    public GameObject[] monster1 = new GameObject[10];
    [Header("���� 2")]
    public GameObject[] monster2 = new GameObject[10];
    [Header("���� 3")]
    public GameObject[] monster3 = new GameObject[10];
    [Header("��������")]
    public GameObject bossMonster = null;
}

