using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Monster : MonoBehaviour, IPooling
{
    protected static E_MonsterType GetDetailType(Enum m_type)
    {
        switch (m_type)
        {
            case E_MonsterType.Melee:
                return E_MonsterType.Melee;
            case E_MonsterType.Range:
                return E_MonsterType.Range;
        }

        throw new Exception("지정된 몬스터 타입이 없음");
    }

    #region 프로퍼티

    public abstract Enum Type { get; }
    public GameObject Obj => gameObject;
    public bool IsDead => _hp < 1;

    private bool IsOnRanged
    {
        get
        {
            _hitInfos = Physics2D.CircleCast(transform.position, _range, Vector2.up);
            
            if (_hitInfos.collider == null)
                return false;

            return true;
        }
    }

    #endregion

    #region 변수

    private RaycastHit2D _hitInfos;
    private Rigidbody2D _rb;

    private float _speed;
    private float _range;
    private float _hp;

    #endregion

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 몬스터 스폰 후 모든 과정을 시작합니다.
    /// </summary>
    protected virtual void LogicStart()
    {
        OverWriteData();
        StartCoroutine(MainLogic());
    }

    /// <summary>
    /// 메인 로직으로 상태 별 행동을 구분하여 동작을 진행합니다.
    /// </summary>
    protected virtual IEnumerator MainLogic()
    {
        yield return null;

        while (true)
        {
            if (IsDead == true)
                break;

            if (IsOnRanged == true)
            {
                Attack();
            }
            else
            {
                Move();
            }

            yield return null;
        }
    }

    /// <summary>
    /// 현재 지정된 값 데이터를 원본 데이터 값으로 설정합니다.
    /// </summary>
    protected void OverWriteData()
    {
        MonsterData m_data = Manager.Data.GetMonsterData(GetDetailType(Type));

        this._speed = m_data.MoveSpeed;
        this._range = m_data.AtkRange;
        this._hp = m_data.HP;
    }

    /// <summary>
    /// 몬스터의 공격을 동작합니다.
    /// </summary>
    protected virtual void Attack() { }

    /// <summary>
    /// 몬스터의 이동을 동작합니다.
    /// </summary>
    protected virtual void Move()
    {
        _rb.velocity = Vector2.left * _speed;
    }

    /// <summary>
    /// 데미지 값을 적용하며, 체력이 없을 시 사망 로직을 진행합니다.
    /// </summary>
    public void OnDamage(float m_value)
    {
        this._hp = Mathf.Clamp(_hp - m_value, 0, _hp - m_value);

        if (IsDead == true)
        {
            StartCoroutine(DeadAction());
        }
    }

    /// <summary>
    /// 몬스터 사망 시 죽음 로직을 수행합니다.
    /// </summary>
    private IEnumerator DeadAction()
    {
        yield return null;
        Return();
    }

    /// <summary>
    /// 몬스터는 오브젝트 풀로 관리되어야하므로 반환 요청시 오브젝트 풀 매니저로 반환합니다.
    /// </summary>
    public void Return()
    {
        Manager.Pool.Return(this);
    }

}
