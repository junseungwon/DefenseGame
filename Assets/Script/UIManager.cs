using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //���� ���׷��̵� ��ư
    [SerializeField]
    private Button[] upGradeBt = new Button[3];

    //���� ��ȯ ��ư
    [SerializeField]
    private Button instanceButton = null;

    //���� ��ȯ ��ġ
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

    //�ʹ� ����
    private void StartSetting()
    {
        instanceButton.onClick.AddListener(() => InstanceHero());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGold();
    }
    //���� ��ȯ ��ư
    public void InstanceHero()
    {
        //��ȯ ��ư�� ������ 20��尡 �Ҹ� �ǰ� ��ȯ�Ǵ� ������ �����̴�.
        if(GameManager.instance.gold > 20)
        {
            GameManager.instance.gold -= 20;
            int randomIndex = Random.Range(0, 3);
            Debug.Log(randomIndex + "������ ��ȯ�Ǿ����ϴ�.");
            GameManager.instance.unitSpawner.SpawnerUnit(randomIndex);
        }
    }

    //�ʷϻ�(�ڿ�)
    //up��ư ������ ���� ���� ���׷��̵�
    //��3���� ���׷��̵� �ҰŰ� 
    //��ư�� ������ �ش� ������ ��ȭ�ȴ�.
    //���� �Ŵ����� �ϳ� ���� ���ֵ��� ��ȭ���¸� �����Ѵ�.
    //���� ��ũ�� ���� �Ŵ������� ������ ���� ���ݷ�++
    public void UpGradeUnitButton(int num)
    {
        //�Ҹ�Ǵ� ��尡 100����Ʈ �̻��� �� 100�� �����ϰ� ��ũ�� 1 ��� ��Ų��.
        if (GameManager.instance.gold > 1)
        {
            GameManager.instance.gold -= 1;
            GameManager.instance.unitRank[num] += 1;
        }
    }

    //���� ���̺���� ��Ÿ���� ǥ����
    public void UpdateTimeBar()
    {
        StartCoroutine(CorutineNextWaveTimer());
    }
    
    //0.1�ʸ��� �ð� Ÿ�̸Ӱ� �پ���.
    private IEnumerator CorutineNextWaveTimer()
    {
        while (timerBar.fillAmount > 0)
        {
            timerBar.fillAmount -= 0.1f / 60;
            yield return new WaitForSeconds(0.1f);
        }
        timerBar.fillAmount = 1f;
    }
    //��� �ǽð� ������Ʈ
    public void UpdateGold()
    {
        goldText.text = GameManager.instance.gold.ToString();
    }
}
