using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public float damage = 0f;
    public GameObject monster = null;
    public GameObject parent = null;
    public bool isShoot = false;
    public float speed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        FireShot();
    }

    //값 세팅
    public void Setting(float damage, GameObject monster, float speed)
    {
        this.damage = damage;
        this.monster = monster;
        this.speed = speed;
        isShoot = true;
    }

    //소환되면 몬스터를 향해서 움직이고 맞으면 적중이 뜨고 소멸 후 다시 장착
    private void FireShot()
    {
        //발사 가능여부 판단
        if (!isShoot) return;

        //물체가 없거나 물체가 비활성화 되어 있는지 확인하기
        if (monster != null||monster.activeSelf == false)
        {
            // 목표 방향 계산
            Vector3 direction = (monster.transform.position - transform.position).normalized;

            // 유도탄 이동
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("물체가 사라짐");
            //목표가 사라지면 비활성화
            isShoot = false;
             this.gameObject.SetActive(false);
        }
    }


    //몬스터와 충돌 시
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            Debug.Log("적중하였습니다.");

            //변수 초기화 및 물체 비활성화
            isShoot = false;
            this.gameObject.SetActive(false);
        }
    }
}
