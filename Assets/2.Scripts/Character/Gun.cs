using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    private int _bulletCount;
    private BulletData _data;
    public float SpreadAngle => _data.SpreadAngle;

    public void SetBullet(E_Bullet m_type)
    {
        this._data = Manager.Data.GetBulletData(m_type);
    }

    public void Fire(E_Bullet m_type)
    {
        for (int i = 0; i < _data.BulletCount; i++)
        {
            Bullet bullet = Manager.Pool.Get<Bullet>(m_type);
            bullet.SetDamage(_data.Power);
            bullet.SetType(m_type);
            bullet.SetVelocity(_data.MoveSpeed);

            bullet.transform.position = _firePos.position;
            bullet.transform.right = _firePos.forward;

            if (i == 0)
                continue;

            ApplySpreadAngle(bullet.transform);
        }
    }

    public void SetLookTarget(Vector3 m_target)
    {
        transform.LookAt(m_target);
    }

    private void ApplySpreadAngle(Transform m_tr)
    {
        m_tr.RotateAround(m_tr.transform.position, Vector3.right, Random.Range(-1 * _data.SpreadAngle, _data.SpreadAngle));
    }
}
