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

    //쿨타임 가능 여부
    private bool isCool = true;

    private IEnumerator corutineCoolTimer = null;

    private int bulletCount = 0;
    //소환되면 그때부터 몬스터들을 탐지한다. 탐지한 몬스터를 향해서 미사일 발사한다.

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
    private void OnTriggerStay2D(Collider2D collision)
    {
        DetectMonster(collision);
    }
  
    //몬스터를 감지 했을 경우 총알을 발사한다.
    private void DetectMonster(Collider2D collision)
    {
        //쿨타임이 돌거나 연속샷일 경우 총알을 발사함
        if (isCool&&collision.gameObject.layer == 7)
        {
            Debug.Log("Shot");

            //쿨타임이 true일 경우에만 true
            if (isCool && this.gameObject.activeSelf == true) StartCoolTimer();
    
            //총알을 활성화 시킨다.
            bullet[bulletCount].SetActive(true);

            //총알에 값을 세팅해준다.
            bullet[bulletCount].GetComponent<Bullet>().Setting(10, collision.gameObject, 1.0f);
            

            //다음 총알로 인계
            bulletCount++;

            //최대 총알이되면 리셋
            if (bulletCount >= 10) bulletCount = 0;

        }
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
