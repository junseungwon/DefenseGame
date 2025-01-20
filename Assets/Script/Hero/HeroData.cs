
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Scriptable Object/HeroData", order = 0)]
public class HeroData : ScriptableObject
{
    //���� �ڵ����
    [SerializeField]
    private int heroCodeName;
    public int HeroCodeName { get { return heroCodeName; } }

    //���� �̸�
    [SerializeField]
    private string heroName;
    public string HeroName { get { return heroName; } }

    //������ �ӵ�
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    //���ݷ�
    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } }

    [SerializeField]
    private float coolTime;
    public float CoolTime { get { return coolTime; } }

    [SerializeField]
    private int rank;
    public int Rank
    {
        get { return rank; }
    }
}
public enum Rank
{
    Red, Oragne, Yellow
}

