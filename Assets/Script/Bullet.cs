using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (monster != null && monster.activeSelf == true)
        {
            // 목표 방향 계산
            Vector3 direction = (monster.transform.position - transform.position);

            // 유도탄 이동
            transform.position += Normalized(direction) * speed * Time.deltaTime;
        }
        else
        {
            //목표가 사라지면 비활성화
            isShoot = false;
             this.gameObject.SetActive(false);
        }
    }
    private float epsilon = 0.01f; // 0 근사치 기준

    private Vector3 Normalized(Vector3 vector)
    {
        return new Vector3(ConvertToUnit(vector.x),
                           ConvertToUnit(vector.y),
                           ConvertToUnit(vector.z));
    }

    private float ConvertToUnit(float value)
    {
        if (value > epsilon)
            return 1f;
        else if (value < -epsilon)
            return -1f;
        else
            return 0f;
    }


    //몬스터와 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //변수 초기화 및 물체 비활성화
            isShoot = false;
            transform.localPosition = Vector3.zero;
            monster.GetComponent<Monster>().GetDamaged(damage);
            this.gameObject.SetActive(false);
        }
    }
}
