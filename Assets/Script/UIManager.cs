using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //���� ���׷��̵� ��ư
    [SerializeField]
    private Button[] upGradeBt = new Button[3];
    // Start is called before the first frame update
    void Start()
    {
        StartSetting();
    }

    //�ʹ� ����
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
    //���� ��ȯ ��ư
    public void InstanceHero()
    {

    }
    
    //�ʷϻ�(�ڿ�)
    //up��ư ������ ���� ���� ���׷��̵�
    //��3���� ���׷��̵� �ҰŰ� 
    //��ư�� ������ �ش� ������ ��ȭ�ȴ�.
    //���� �Ŵ����� �ϳ� ���� ���ֵ��� ��ȭ���¸� �����Ѵ�.
    //���� ��ũ�� ���� �Ŵ������� ������ ���� ���ݷ�++
    public void UpGradeUnitButton(int num)
    {

    }
}
