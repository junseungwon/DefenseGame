using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Hero : MonoBehaviour
{
    //����� ������
    [SerializeField]
    private HeroData heroData;

    [SerializeField]
    private GameObject[] bullet = null;

    //���� ��ü�� ����Ʈ
    private List<Collider> detectedColliders = new List<Collider>();

    //��Ÿ�� ���� ����
    private bool isCool = true;

    private IEnumerator corutineCoolTimer = null;
    private IEnumerator corutineMoveTimer = null;
    private bool isMove = false;

    private int bulletCount = 0;

    //��ü ������ ����
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        DetectMonster();

    }

    public void MoveArea(Vector3 movePos)
    {
        if (this.gameObject.activeSelf == false)
        {
            return;
        }
        //����� �ڷ�ƾ�� ������ ����ϱ�
        if (corutineMoveTimer != null)
        {
            StopCoroutine(corutineMoveTimer);
        }
        corutineMoveTimer = CorutineMoveArea(movePos);
        StartCoroutine(corutineMoveTimer);
    }

    private IEnumerator CorutineMoveArea(Vector3 movePos)
    {
        float dis = 0f;

        //�ش� ��ҷ� �̵��� ������ �ݺ��Ѵ�.
        while (dis <= 0)
        {
            Vector3 direction = (movePos - transform.position).normalized;
            direction.y = 0;
            transform.localPosition += direction * heroData.MoveSpeed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
    //��Ÿ��

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

    private void OnTriggerEnter(Collider other)
    {
        // �ٸ� Collider�� Ʈ���ſ� ���� �� ����Ʈ�� �߰�
        if (!detectedColliders.Contains(other) && other.gameObject.layer == 7)
        {
            detectedColliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �ٸ� Collider�� Ʈ���Ÿ� ���� �� ����Ʈ���� ����
        if (detectedColliders.Contains(other) && other.gameObject.layer == 7)
        {
            detectedColliders.Remove(other);
        }
    }

    //���͸� ���� ���� ��� �Ѿ��� �߻��Ѵ�.
    private void DetectMonster()
    {
        //��Ÿ�� �� ��ü�� ���� ��� ����
        if (!isCool || detectedColliders.Count == 0) return;

        GameObject minObj = CalculateMinDistanceColliders();

        //��Ÿ�� �� Ȱ��ȭ�ÿ��� ��Ÿ�� ����
        if (isCool && this.gameObject.activeSelf == true) StartCoolTimer();

        //�Ѿ��� Ȱ��ȭ ��Ų��.
        bullet[bulletCount].SetActive(true);

        // ������Ʈ�� Ÿ���� �ٶ󺸵��� ȸ���մϴ�.
        transform.LookAt(minObj.transform.position);
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.y + 90f, 0);
       // transform.localRotation = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);

        //�Ѿ˿� ���� �������ش�.
        //�������� �⺻ ���������� �⺻���������� + ������ (�⺻ ������*��ũ) 10+ 10(1)
        bullet[bulletCount].GetComponent<Bullet>().Setting(heroData.Damage+(heroData.Damage * GameManager.instance.unitRank[heroData.HeroCodeName]), minObj, 4.0f);

        //���� �Ѿ˷� �ΰ�
        bulletCount++;

        //�ִ� �Ѿ��̵Ǹ� ����
        if (bulletCount >= 20) bulletCount = 0;
    }

    // ���� ������ Collider ��Ͽ��� ���� ����� ��ü�� ã�´�.
    public GameObject CalculateMinDistanceColliders()
    {
        //���� ���� index���� ����
        GameObject obj = null;
        float minDis = int.MaxValue;

        //�ݺ��� ������ ���� ����� ��ü�� ã�´�.
        foreach (var collider in detectedColliders)
        {
            float dis = Vector3.Distance(collider.transform.position, transform.position);
            //�ּ� �Ÿ����� ������ Ȯ���Ѵ�. ������ ������Ʈ �� �Ÿ��� �����Ѵ�.
            if (dis < minDis)
            {
                obj = collider.gameObject;
                minDis = dis;
            }
        }
        return obj;

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
