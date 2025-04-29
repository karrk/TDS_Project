using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "GameData/Monster Data")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _atkRange;
    [SerializeField] private float _hp;

    public float MoveSpeed => _moveSpeed;
    public float AtkRange => _atkRange;
    public float HP => _hp;

}
