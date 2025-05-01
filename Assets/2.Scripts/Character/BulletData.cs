using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "GameData/Bullet Data")]
public class BulletData : ScriptableObject
{
    [SerializeField] private E_Bullet _type;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _power;
    [SerializeField,Header("1회 발사 시 총알 수")] private int _bulletCount;
    [SerializeField, Header("산탄 범위")] private float _spreadAngle;

    public E_Bullet Type => _type;
    public float MoveSpeed => _moveSpeed;
    public float Power => _power;
    public int BulletCount => _bulletCount;
    public float SpreadAngle => _spreadAngle;
}
