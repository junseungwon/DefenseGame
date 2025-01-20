
using UnityEngine;

[CreateAssetMenu(fileName = "HeroData", menuName = "Scriptable Object/HeroData", order = 0)]
public class HeroData : ScriptableObject
{
    //영웅 코드네임
    [SerializeField]
    private int heroCodeName;
    public int HeroCodeName { get { return heroCodeName; } }

    //영웅 이름
    [SerializeField]
    private string heroName;
    public string HeroName { get { return heroName; } }

    //움직임 속도
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    //공격력
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

