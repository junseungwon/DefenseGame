using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    //���� �迭�� ���͸� ������ �ִ´�?
    private UnitGameObject unitGameObject = new UnitGameObject();

    //���͵��� ����� �θ� parent
    [SerializeField]
    private GameObject unitParent = null;

    //���� �ʱ⼼��
    private void Awake()
    {
        GameManager.instance.unitSpawner = this;
        UnitSetting();

    }
    //���� �����ϱ�
    public void SpawnerUnit(int num)
    {
        //���͸� Ȱ��ȭ�� ��Ű�� �ɵ� 
        switch (num)
        {
            case 0:
                // �� ���̺�� �ִ� ���̺� ���� - ���� ������ ���� ī��Ʈ�� ���� �� ���͸� ����ϸ� �ɵ�
                DequeUnit(unitGameObject.unit1, 0);
                break;
            case 1:
                DequeUnit(unitGameObject.unit2, 1);
                break;
            case 2:
                DequeUnit(unitGameObject.unit3, 2);
                break;
            default:
                Debug.Log("�� �� ���� �����Դϴ�.");
                break;
        }
    }

    //���Ͱ� �������� Ȯ���ϰ� �����ϸ� �����Ѵ�.
    private void DequeUnit(Queue<GameObject> que, int num)
    {
        //���� que�� ���Ͱ� ������ �����ϰ� que�� ����
        if (que.Count == 0)
        {
            GameObject obj = Instantiate(unitParent.transform.GetChild(num).gameObject);

            //�ڽļ����� �ڵ������ 0 ���۰� 1������ �޶� +1 �߰��Ѵ�.
            ReInputUnit(obj, num + 1);
        };
        //������ ������ Ȱ��ȭ
        que.Dequeue().SetActive(true);
    }


    //�ʹ� ���� ��ü ����
    private void UnitSetting()
    {
        //���� 1���� 1�� que�� ����
        UnitInsert(0, unitGameObject.unit1);
        UnitInsert(1, unitGameObject.unit2);
        UnitInsert(2, unitGameObject.unit3);
    }

    //que�� ������ ����
    private void UnitInsert(int childNum, Queue<GameObject> que)
    {
        for (int i = 0; i < unitParent.transform.GetChild(childNum).childCount; i++)
        {
            //GameManager.instance.selectManager.selectableChars.Add(unitParent.transform.GetChild(childNum).GetChild(i).GetChild(0).GetChild(0).GetComponent<SelectableCharacter>());
            que.Enqueue(unitParent.transform.GetChild(childNum).GetChild(i).gameObject);
            
        }
    }

    //���Ͱ� �װų� �ܺο������� �Ҹ��� ��� �ٽ� que�� ����
    public void ReInputUnit(GameObject obj, int codeName)
    {
        switch (codeName)
        {
            case 1:
                unitGameObject.unit1.Enqueue(obj);
                break;
            case 2:
                unitGameObject.unit2.Enqueue(obj);
                break;
            case 3:
                unitGameObject.unit3.Enqueue(obj);
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
public class UnitGameObject
{
    [Header("���� 1")]
    public Queue<GameObject> unit1 = new Queue<GameObject>();
    [Header("���� 2")]
    public Queue<GameObject> unit2 = new Queue<GameObject>();
    [Header("���� 3")]
    public Queue<GameObject> unit3 = new Queue<GameObject>();

}

