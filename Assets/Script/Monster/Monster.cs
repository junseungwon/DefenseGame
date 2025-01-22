using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour
{
    public Transform target = null;
    public float speed = 30.0f;
    
    [SerializeField]
    private MonsterData monsterData;

    private float hp = 100;
    public float Hp {  get { return hp; } set { hp = value; if (hp <= 0) { Dead(); } } }

    private void Awake()
    {
        target = GameObject.Find("Center").transform;
    }
    private void Start()
    {
       
       hp = monsterData.MonsterHp;
    }

    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, monsterData.MoveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.identity;
    }

    //외부 공격으로부터 데미지를 받음
    public void GetDamaged(float num)
    {
        Debug.Log(Hp + "가 남았습니다.");
        Hp -= num;
    }

    //비활성화 및 위치 초기화, 다시 que에 삽입
    private void Dead()
    {
        transform.localPosition = new Vector3(2.5f, 0, 0);
        GameManager.instance.gold += monsterData.Gold;
        GameManager.instance.monsterSpawner.ReInputMonster(this.gameObject, monsterData.MonsterCodeName);
        gameObject.SetActive(false);
    }
}

 