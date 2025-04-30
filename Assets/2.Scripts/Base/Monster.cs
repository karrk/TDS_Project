using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Monster : MonoBehaviour, IPooling, IDamageable
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
    public bool IsDead => _hp <= 0;
    private Vector2 LiftBoxSize => new Vector2(_coll.size.x/2, _coll.size.y/4);
    private Vector3 LiftBoxCenter => 
        transform.position + (Vector3.right * (_coll.size.x / 2 + _coll.offset.x + _liftBoxOffset.x)
        + (Vector3.up * (_coll.size.y/2 + _liftBoxOffset.y)));
    private bool _isLifting;
    public bool IsLifting
    {
        get { return _isLifting; }
        private set
        {
            _isLifting = value;

            if (_isLifting == true)
            {
                StartCoroutine(StartLiftDelay());
            }
        }
    }

    private IEnumerator StartLiftDelay()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.2f,2f));
        _isLifting = false;
    }

    /// <summary>
    /// 콜라이더의 중심점부터 바닥까지의 길이 값
    /// </summary>
    public float PivotToBot => _coll.size.y/2 - _coll.offset.y;

    private bool IsOnRanged
    {
        get
        {
            _hitInfos = Physics2D.CircleCast(transform.position, _range, Vector2.up);

            if (_hitInfos.collider.tag == "Player")
                return true;

            return false;
        }
    }

    #endregion

    #region 변수

    private RaycastHit2D _hitInfos;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _coll;
    private Vector2 _liftBoxOffset = new Vector2(0, 0f);
    [SerializeField] private float _liftPower;

    private float _speed;
    private float _range;
    private float _hp;

    #endregion

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<CapsuleCollider2D>();
    }

    /// <summary>
    /// 몬스터 오브젝트의 충돌 레이어를 설정합니다.
    /// 대상 바닥 콜라이더, 같은 경로 내 위치한 몬스터 콜라이더
    /// </summary>
    public void SetLayer(E_Way m_wayType)
    {
        switch (m_wayType)
        {
            case E_Way.Top:
                this.gameObject.layer = 10;
                break;
            case E_Way.Mid:
                this.gameObject.layer = 11;
                break;
            case E_Way.Bot:
                this.gameObject.layer = 12;
                break;
        }

        this._coll.forceSendLayers = 1 << 7 + gameObject.layer - 10 | 1 << 10 + gameObject.layer - 10;
    }

    /// <summary>
    /// 몬스터 스폰 후 모든 과정을 시작합니다.
    /// </summary>
    public virtual void LogicStart()
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

            LiftBackMonster();

            yield return null;
        }
    }

    private void LiftBackMonster()
    {
        FindBackMonster();
    }

    private void FindBackMonster()
    {
        RaycastHit2D[] objs = Physics2D.BoxCastAll(LiftBoxCenter, LiftBoxSize, 0, Vector2.up,0,1<<this.gameObject.layer);

        foreach (var item in objs)
        {
            if (item.collider != _coll)
            {
                Monster mob = item.collider.GetComponent<Monster>();

                if(mob.IsLifting == false)
                {
                    mob.IsLifting = true;
                    item.collider.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 7f, ForceMode2D.Impulse);
                }
            }
                
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
        _rb.velocity = new Vector2(-1 * _speed, _rb.velocity.y);
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

    public bool OnDamage(float m_value)
    {
        this._hp = Mathf.Clamp(_hp - m_value, 0, _hp - m_value);

        if (IsDead == true)
        {
            StartCoroutine(DeadAction());
        }

        return IsDead;
    }

    #region 디버깅

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawCube(LiftBoxCenter, LiftBoxSize);
    }

    #endregion
}
