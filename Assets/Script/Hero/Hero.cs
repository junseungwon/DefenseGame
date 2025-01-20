using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //����� ������
    [SerializeField]
    private HeroData heroData;

    [SerializeField]
    private GameObject[] bullet = null;

    //��Ÿ�� ���� ����
    private bool isCool = true;

    private IEnumerator corutineCoolTimer = null;

    private int bulletCount = 0;
    //��ȯ�Ǹ� �׶����� ���͵��� Ž���Ѵ�. Ž���� ���͸� ���ؼ� �̻��� �߻��Ѵ�.

    //��Ÿ�� �߻�
    private void StartCoolTimer()
    {
        if (this.gameObject.activeSelf == false)
        {
            return;
        }
        if (corutineCoolTimer != null)
        {
            StopCoroutine(corutineCoolTimer);
        }
        corutineCoolTimer = CoolTimer();
        StartCoroutine(corutineCoolTimer);
    }

    //��Ÿ�� �ڷ�ƾ
    private IEnumerator CoolTimer()
    {
        isCool = false;

        yield return new WaitForSeconds(heroData.CoolTime);

        isCool = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        DetectMonster(collision);
    }
  
    //���͸� ���� ���� ��� �Ѿ��� �߻��Ѵ�.
    private void DetectMonster(Collider2D collision)
    {
        //��Ÿ���� ���ų� ���Ӽ��� ��� �Ѿ��� �߻���
        if (isCool&&collision.gameObject.layer == 7)
        {
            Debug.Log("Shot");

            //��Ÿ���� true�� ��쿡�� true
            if (isCool && this.gameObject.activeSelf == true) StartCoolTimer();
    
            //�Ѿ��� Ȱ��ȭ ��Ų��.
            bullet[bulletCount].SetActive(true);

            //�Ѿ˿� ���� �������ش�.
            bullet[bulletCount].GetComponent<Bullet>().Setting(10, collision.gameObject, 1.0f);
            

            //���� �Ѿ˷� �ΰ�
            bulletCount++;

            //�ִ� �Ѿ��̵Ǹ� ����
            if (bulletCount >= 10) bulletCount = 0;

        }
    }

    //�Ѿ˵��� ������
    private void BulletSetting()
    {
        //�Ѿ˵��� ���� ����
        for (int i = 0; i < bullet.Length; i++)
        {
           
        }
    }
}
