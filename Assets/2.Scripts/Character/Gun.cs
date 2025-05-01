using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private RectTransform _topLine;
    [SerializeField] private RectTransform _centerLine;
    [SerializeField] private RectTransform _botLine;

    [SerializeField] private Transform _firePos;
    private GameObject _canvasObj;
    private int _bulletCount;
    private BulletData _data;
    public float SpreadAngle => _data.SpreadAngle;

    private void Awake()
    {
        _canvasObj = GetComponentInChildren<Canvas>(true).gameObject;
    }

    /// <summary>
    /// 총이 활용해야할 총알 데이터를 설정합니다.
    /// </summary>
    public void SetBullet(E_Bullet m_type)
    {
        this._data = Manager.Data.GetBulletData(m_type);
    }

    /// <summary>
    /// 총알을 발사합니다.
    /// </summary>
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

    /// <summary>
    /// 전달받은 위치를 기준으로 산탄범위 영역을 표기합니다.
    /// </summary>
    public void ShowAimArea(Vector3 m_aimPos)
    {
        _canvasObj.SetActive(true);

        Vector3 dir = m_aimPos - _centerLine.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        _centerLine.rotation = Quaternion.Euler(0, 0, angle);
        _topLine.rotation = Quaternion.Euler(0, 0, angle + _data.SpreadAngle / 2);
        _botLine.rotation = Quaternion.Euler(0, 0, angle - _data.SpreadAngle / 2);
    }

    /// <summary>
    /// 에임 범위 이미지를 비활성화 합니다.
    /// </summary>
    public void HideAimArea()
    {
        _canvasObj.SetActive(false);
    }

    /// <summary>
    /// 전달받은 해당 위치를 바라보게 합니다. 
    /// Forward 기준
    /// </summary>
    public void SetLookTarget(Vector3 m_target)
    {
        transform.LookAt(m_target);
    }

    /// <summary>
    /// Bullet 에 적용된 산탄범위를 적용합니다.
    /// </summary>
    private void ApplySpreadAngle(Transform m_tr)
    {
        m_tr.RotateAround(m_tr.transform.position, Vector3.right, Random.Range(-1 * _data.SpreadAngle, _data.SpreadAngle));
    }
}
