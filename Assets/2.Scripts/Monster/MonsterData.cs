using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "GameData/Monster Data")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private E_MonsterType _type;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _atkRange;
    [SerializeField] private float _hp;

    public E_MonsterType Type => _type;
    public float MoveSpeed => _moveSpeed;
    public float AtkRange => _atkRange;
    public float HP => _hp;

}
