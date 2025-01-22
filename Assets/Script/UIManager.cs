using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //유닛 업그레이드 버튼
    [SerializeField]
    private Button[] upGradeBt = new Button[3];
    // Start is called before the first frame update
    void Start()
    {
        StartSetting();
    }

    //초반 세팅
    private void StartSetting()
    {
        for (int i = 0; i < upGradeBt.Length; i++)
        {
            upGradeBt[i].onClick.AddListener(()=>UpGradeUnitButton(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //영웅 소환 버튼
    public void InstanceHero()
    {

    }
    
    //초록색(자원)
    //up버튼 누르면 각자 유닛 업그레이드
    //총3마리 업그레이드 할거고 
    //버튼을 누르면 해당 유닛이 강화된다.
    //유닛 매니져를 하나 만들어서 유닛들의 강화상태를 저장한다.
    //유닛 랭크를 게임 매니져에서 관리를 하자 공격력++
    public void UpGradeUnitButton(int num)
    {

    }
}
