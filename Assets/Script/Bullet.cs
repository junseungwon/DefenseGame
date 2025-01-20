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

    //�� ����
    public void Setting(float damage, GameObject monster, float speed)
    {
        this.damage = damage;
        this.monster = monster;
        this.speed = speed;
        isShoot = true;
    }

    //��ȯ�Ǹ� ���͸� ���ؼ� �����̰� ������ ������ �߰� �Ҹ� �� �ٽ� ����
    private void FireShot()
    {
        //�߻� ���ɿ��� �Ǵ�
        if (!isShoot) return;

        //��ü�� ���ų� ��ü�� ��Ȱ��ȭ �Ǿ� �ִ��� Ȯ���ϱ�
        if (monster != null||monster.activeSelf == false)
        {
            // ��ǥ ���� ���
            Vector3 direction = (monster.transform.position - transform.position).normalized;

            // ����ź �̵�
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("��ü�� �����");
            //��ǥ�� ������� ��Ȱ��ȭ
            isShoot = false;
             this.gameObject.SetActive(false);
        }
    }


    //���Ϳ� �浹 ��
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            Debug.Log("�����Ͽ����ϴ�.");

            //���� �ʱ�ȭ �� ��ü ��Ȱ��ȭ
            isShoot = false;
            this.gameObject.SetActive(false);
        }
    }
}
