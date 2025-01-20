using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //히어로 데이터
    [SerializeField]
    private HeroData heroData;

    [SerializeField]
    private GameObject[] bullet = null;

    //감지 물체들 리스트
    private List<Collider> detectedColliders = new List<Collider>();

    //쿨타임 가능 여부
    private bool isCool = true;

    private IEnumerator corutineCoolTimer = null;

    private int bulletCount = 0;

    //물체 감지를 시작
    private void Update()
    {
        DetectMonster();
    }

    //쿨타임 발생
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

    //쿨타임 코루틴
    private IEnumerator CoolTimer()
    {
        isCool = false;

        yield return new WaitForSeconds(heroData.CoolTime);

        isCool = true;
    }
  
    private void OnTriggerEnter(Collider other)
    {
        // 다른 Collider가 트리거에 들어올 때 리스트에 추가
        if (!detectedColliders.Contains(other) && other.gameObject.layer == 7)
        {
            Debug.Log("감지");
            detectedColliders.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 다른 Collider가 트리거를 나갈 때 리스트에서 제거
        if (detectedColliders.Contains(other) && other.gameObject.layer == 7)
        {
            detectedColliders.Remove(other);
        }
    }

    //몬스터를 감지 했을 경우 총알을 발사한다.
    private void DetectMonster()
    {
        //쿨타임 및 물체가 없는 경우 종료
        if (isCool&& detectedColliders.Count == 0) return;

        GameObject minObj =CalculateMinDistanceColliders();

        //쿨타임 및 활성화시에만 쿨타임 돌림
        if (isCool && this.gameObject.activeSelf == true) StartCoolTimer();

        //총알을 활성화 시킨다.
        bullet[bulletCount].SetActive(true);

        //총알에 값을 세팅해준다.
        bullet[bulletCount].GetComponent<Bullet>().Setting(10, minObj, 4.0f);


        //다음 총알로 인계
        bulletCount++;

        //최대 총알이되면 리셋
        if (bulletCount >= 20) bulletCount = 0;

    }

    // 현재 감지된 Collider 목록에서 가장 가까운 물체를 찾는다.
    public GameObject CalculateMinDistanceColliders()
    {
        //가장 작은 index값을 저장
        GameObject obj = null;
        float minDis = int.MaxValue;

        //반복문 돌려서 가장 가까운 물체를 찾는다.
        foreach (var collider in detectedColliders)
        {
            float dis = Vector3.Distance(collider.transform.position, transform.position);
            //최소 거리보다 작은지 확인한다. 작으면 오브젝트 및 거리값 저장한다.
            if (dis < minDis)
            {
                obj = collider.gameObject;
                minDis = dis;
            }
        }
        return obj;

    }

    //총알들을 재장전
    private void BulletSetting()
    {
        //총알들의 값을 세팅
        for (int i = 0; i < bullet.Length; i++)
        {

        }
    }
}
