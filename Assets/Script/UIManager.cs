using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //유닛 업그레이드 버튼
    [SerializeField]
    private Button[] upGradeBt = new Button[3];

    //유닛 소환 버튼
    [SerializeField]
    private Button instanceButton = null;

    //유닛 소환 위치
    [SerializeField]
    private Transform instancePos = null;

    [SerializeField]
    private TextMeshProUGUI goldText = null;

    [SerializeField]
    private Image timerBar = null;
    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.instance.uIManager = this;
    }
    void Start()
    {
        StartSetting();
    }

    //초반 세팅
    private void StartSetting()
    {
        instanceButton.onClick.AddListener(() => InstanceHero());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGold();
    }
    //영웅 소환 버튼
    public void InstanceHero()
    {
        //소환 버튼을 누르면 20골드가 소모가 되고 소환되는 유닛은 랜덤이다.
        if(GameManager.instance.gold > 20)
        {
            GameManager.instance.gold -= 20;
            int randomIndex = Random.Range(0, 3);
            Debug.Log(randomIndex + "유닛이 소환되었습니다.");
            GameManager.instance.unitSpawner.SpawnerUnit(randomIndex);
        }
    }

    //초록색(자원)
    //up버튼 누르면 각자 유닛 업그레이드
    //총3마리 업그레이드 할거고 
    //버튼을 누르면 해당 유닛이 강화된다.
    //유닛 매니져를 하나 만들어서 유닛들의 강화상태를 저장한다.
    //유닛 랭크를 게임 매니져에서 관리를 하자 공격력++
    public void UpGradeUnitButton(int num)
    {
        //소모되는 골드가 100포인트 이상일 때 100을 차감하고 랭크를 1 상승 시킨다.
        if (GameManager.instance.gold > 1)
        {
            GameManager.instance.gold -= 1;
            GameManager.instance.unitRank[num] += 1;
        }
    }

    //다음 웨이브까지 쿨타임을 표시함
    public void UpdateTimeBar()
    {
        StartCoroutine(CorutineNextWaveTimer());
    }
    
    //0.1초마다 시간 타이머가 줄어든다.
    private IEnumerator CorutineNextWaveTimer()
    {
        while (timerBar.fillAmount > 0)
        {
            timerBar.fillAmount -= 0.1f / 60;
            yield return new WaitForSeconds(0.1f);
        }
        timerBar.fillAmount = 1f;
    }
    //골드 실시간 업데이트
    public void UpdateGold()
    {
        goldText.text = GameManager.instance.gold.ToString();
    }
}
