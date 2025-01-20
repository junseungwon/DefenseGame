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

    //���� ��ü�� ����Ʈ
    private List<Collider> detectedColliders = new List<Collider>();

    //��Ÿ�� ���� ����
    private bool isCool = true;

    private IEnumerator corutineCoolTimer = null;

    private int bulletCount = 0;

    //��ü ������ ����
    private void Update()
    {
        DetectMonster();
    }

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
            Debug.Log("����");
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
        if (isCool&& detectedColliders.Count == 0) return;

        GameObject minObj =CalculateMinDistanceColliders();

        //��Ÿ�� �� Ȱ��ȭ�ÿ��� ��Ÿ�� ����
        if (isCool && this.gameObject.activeSelf == true) StartCoolTimer();

        //�Ѿ��� Ȱ��ȭ ��Ų��.
        bullet[bulletCount].SetActive(true);

        //�Ѿ˿� ���� �������ش�.
        bullet[bulletCount].GetComponent<Bullet>().Setting(10, minObj, 4.0f);


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
