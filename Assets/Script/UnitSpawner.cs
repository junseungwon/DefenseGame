using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    //각자 배열로 몬스터를 가지고 있는다?
    private UnitGameObject unitGameObject = new UnitGameObject();

    //몬스터들이 저장된 부모 parent
    [SerializeField]
    private GameObject unitParent = null;

    //유닛 초기세팅
    private void Awake()
    {
        GameManager.instance.unitSpawner = this;
        UnitSetting();

    }
    //몬스터 생성하기
    public void SpawnerUnit(int num)
    {
        //몬스터를 활성화만 시키면 될듯 
        switch (num)
        {
            case 0:
                // 각 웨이브당 최대 웨이브 숫자 - 현재 몬스터의 남은 카운트를 빼면 그 몬스터를 사용하면 될듯
                DequeUnit(unitGameObject.unit1, 0);
                break;
            case 1:
                DequeUnit(unitGameObject.unit2, 1);
                break;
            case 2:
                DequeUnit(unitGameObject.unit3, 2);
                break;
            default:
                Debug.Log("알 수 없는 유닛입니다.");
                break;
        }
    }

    //몬스터가 부족한지 확인하고 부족하면 생성한다.
    private void DequeUnit(Queue<GameObject> que, int num)
    {
        //만약 que에 몬스터가 없으면 생성하고 que에 넣음
        if (que.Count == 0)
        {
            GameObject obj = Instantiate(unitParent.transform.GetChild(num).gameObject);

            //자식순서랑 코드네임은 0 시작과 1시작이 달라서 +1 추가한다.
            ReInputUnit(obj, num + 1);
        };
        //유닛을 꺼내서 활성화
        que.Dequeue().SetActive(true);
    }


    //초반 몬스터 전체 저장
    private void UnitSetting()
    {
        //몬스터 1번을 1번 que에 저장
        UnitInsert(0, unitGameObject.unit1);
        UnitInsert(1, unitGameObject.unit2);
        UnitInsert(2, unitGameObject.unit3);
    }

    //que에 유닛을 삽입
    private void UnitInsert(int childNum, Queue<GameObject> que)
    {
        for (int i = 0; i < unitParent.transform.GetChild(childNum).childCount; i++)
        {
            //GameManager.instance.selectManager.selectableChars.Add(unitParent.transform.GetChild(childNum).GetChild(i).GetChild(0).GetChild(0).GetComponent<SelectableCharacter>());
            que.Enqueue(unitParent.transform.GetChild(childNum).GetChild(i).gameObject);
            
        }
    }

    //몬스터가 죽거나 외부요인으로 소멸할 경우 다시 que에 삽입
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
                Debug.Log("보스 죽음");
                break;
            default:
                Debug.LogError($"알 수 없는 몬스터가 죽음 코드네임은 {codeName} 입니다");
                break;
        }
    }
}

[System.Serializable]
public class UnitGameObject
{
    [Header("유닛 1")]
    public Queue<GameObject> unit1 = new Queue<GameObject>();
    [Header("유닛 2")]
    public Queue<GameObject> unit2 = new Queue<GameObject>();
    [Header("유닛 3")]
    public Queue<GameObject> unit3 = new Queue<GameObject>();

}

