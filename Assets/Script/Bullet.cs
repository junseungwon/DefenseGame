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
        if (monster != null && monster.activeSelf == true)
        {
            // ��ǥ ���� ���
            Vector3 direction = (monster.transform.position - transform.position);

            // ����ź �̵�
            transform.position += Normalized(direction) * speed * Time.deltaTime;
        }
        else
        {
            //��ǥ�� ������� ��Ȱ��ȭ
            isShoot = false;
             this.gameObject.SetActive(false);
        }
    }
    private float epsilon = 0.01f; // 0 �ٻ�ġ ����

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


    //���Ϳ� �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            //���� �ʱ�ȭ �� ��ü ��Ȱ��ȭ
            isShoot = false;
            transform.localPosition = Vector3.zero;
            monster.GetComponent<Monster>().GetDamaged(damage);
            this.gameObject.SetActive(false);
        }
    }
}
