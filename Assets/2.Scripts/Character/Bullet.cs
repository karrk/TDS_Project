using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour, IPooling
{
    [SerializeField] private float _lifeTime;

    private TrailRenderer _trail;
    private float _timer;
    private float _velocity;
    private float _damage;

    private E_Bullet _type;
    public Enum Type => _type;

    public GameObject Obj => gameObject;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    /// <summary>
    /// 오브젝트 활성 시 자동 Destroy 타이머를 초기화합니다.
    /// </summary>
    private void OnEnable()
    {
        _timer = _lifeTime;
        _trail.enabled = true;
    }

    /// <summary>
    /// 총알 타입을 설정합니다.
    /// 오브젝트 풀 반환 타입을 결정하기 위함입니다.
    /// </summary>
    public void SetType(E_Bullet m_type)
    {
        this._type = m_type;
    }

    /// <summary>
    /// 총알의 이동속도를 설정합니다.
    /// </summary>
    public void SetVelocity(float m_value)
    {
        this._velocity = m_value;
    }

    /// <summary>
    /// 총알의 데미지값을 저장합니다.
    /// </summary>
    public void SetDamage(float m_value)
    {
        this._damage = m_value;
    }

    /// <summary>
    /// 충돌 가능한 오브젝트를 감지
    /// 만약, 피격 가능한 오브젝트라면 데미지를 전달합니다.
    /// 이후 풀로 다시 반환합니다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable m_target))
        {
            m_target.OnDamage(_damage);
        }
        Return();
    }

    private void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
        else
            Return();

        transform.position += transform.right * Time.deltaTime * _velocity;
    }

    public void Return()
    {
        _trail.enabled = false;
        Manager.Pool.Return(this);
    }
}
