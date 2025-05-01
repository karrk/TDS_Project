using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Monster : MonoBehaviour, IPooling, IDamageable, ITargeting
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

    public abstract int TargetPriority { get; }
    public abstract Enum Type { get; }

    public bool IsNoneTarget => IsDead == true;
    public GameObject Obj => gameObject;
    public Collider2D Target => _target;
    public bool IsDead => _hp <= 0;
    public float Power => _power;
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
                StartCoroutine(AutoStateReset(UnityEngine.Random.Range(1.2f, 2),()=>_isLifting = false));
            }
        }
    }

    private bool _isAttacked;
    public bool IsAttacked
    {
        get { return _isAttacked; }
        set
        {
            _isAttacked = value;

            if (_isAttacked == true)
            {
                StartCoroutine(AutoStateReset(_atkCool, () => _isAttacked = false));
            }
        }
    }

    /// <summary>
    /// 콜라이더의 중심점부터 바닥까지의 길이 값
    /// </summary>
    public float PivotToBot => _coll.size.y/2 - _coll.offset.y;

    /// <summary>
    /// 범위 내 감지해야 할 대상이 있는지의 여부
    /// </summary>
    private bool IsOnRanged
    {
        get
        {
            _target = Physics2D.OverlapCircle(transform.position + Vector3.up * _coll.size.y/2, _range, 1<<3);

            if (_target != null)
            {
                if (_target.tag == "Player" || _target.tag == "Hero")
                {
                    return true;
                }
            }

            return false;
        }
    }

    #endregion

    #region 변수

    [SerializeField] private Transform _textPivot;
    private SpriteRenderer[] _renderers;
    [SerializeField] private bool _drawBackCast;
    [SerializeField] private bool _drawRangeArea;
    private HPBarController _hpBar;
    private Collider2D _target;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _coll;
    private Vector2 _liftBoxOffset = new Vector2(0, 0f);
    [SerializeField,Header("우측 몬스터를 띄워 올리는 힘")] private float _liftPower;
    protected Animator _anim;

    protected float _power;
    private float _atkCool;
    private float _speed;
    private float _range;
    private float _hp;
    private float _maxHP;

    // TODO : 레이 감지 변수명이 불분명함 재 정리 필요

    #endregion

    protected virtual void Awake()
    {
        _renderers = GetComponentsInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<CapsuleCollider2D>();
        _anim = GetComponent<Animator>();
        _hpBar = GetComponentInChildren<HPBarController>(true);
    }

    private void OnDisable()
    {
        StopCoroutine(MainLogic());
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
    /// 행동 시작 전, 각 상태를 초기화합니다.
    /// </summary>
    private void ResetOptions()
    {
        _isLifting = false;
        IsAttacked = false;
        _target = null;

        foreach (var renderer in _renderers)
        {
            renderer.color = Color.white;
        }
    }

    /// <summary>
    /// 몬스터 스폰 후 모든 과정을 시작합니다.
    /// </summary>
    public virtual void LogicStart()
    {
        ResetOptions();
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
            {
                _rb.velocity = Vector2.zero;
                break;
            }
                
            else
                Move();

            if (IsOnRanged == true && IsAttacked == false)
            {
                Attack();
                IsAttacked = true;
            }

            LiftBackMonster();

            yield return null;
        }
    }

    /// <summary>
    /// 뒤쪽 몬스터가 위로 올라가도 되는지 확인하는 기능입니다.
    /// </summary>
    private bool LiftCheck()
    {
        return transform.position.y + _coll.size.y * 2 <= Manager.Data.HeroPos.y;
    }

    /// <summary>
    /// 뒤쪽 몬스터가 있을경우 대상을 위쪽으로 띄워올립니다.
    /// </summary>
    private void LiftBackMonster()
    {
        if (LiftCheck() == false)
            return;

        Monster mob = FindBackMonster();

        if (mob == null)
            return;

        if (mob.IsLifting == true)
            return;

        mob.IsLifting = true;
        mob.GetComponent<Rigidbody2D>().AddForce(Vector3.up * _liftPower, ForceMode2D.Impulse);

    }

    /// <summary>
    /// 자신의 뒤쪽(vec3.Right)에 다른 몬스터를 찾아 반환합니다.
    /// </summary>
    private Monster FindBackMonster()
    {
        RaycastHit2D[] objs = Physics2D.BoxCastAll(LiftBoxCenter, LiftBoxSize, 0, Vector2.up,0,1<<this.gameObject.layer);

        foreach (var item in objs)
        {
            if (item.collider != _coll)
            {
                if (item.collider.TryGetComponent<Monster>(out Monster mob))
                {
                    return mob;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 현재 지정된 값 데이터를 원본 데이터 값으로 설정합니다.
    /// </summary>
    protected void OverWriteData()
    {
        MonsterData data = Manager.Data.GetMonsterData(GetDetailType(Type));

        this._atkCool = data.AtkCool;
        this._speed = data.MoveSpeed;
        this._range = data.AtkRange;
        this._hp = data.HP;
        this._maxHP = data.HP;
        this._power = data.Power;
    }

    /// <summary>
    /// 몬스터의 공격을 동작합니다.
    /// </summary>
    protected virtual void Attack() {  }

    /// <summary>
    /// 몬스터의 이동을 동작합니다.
    /// </summary>
    protected virtual void Move()
    {
        _rb.velocity = new Vector2(-1 * _speed, _rb.velocity.y);
    }

    protected virtual void Dead() 
    {
        Return();
    }

    /// <summary>
    /// 대상 시간 이후, 상태를 다시 
    /// </summary>
    /// <param name="m_delay"></param>
    /// <param name="m_state"></param>
    /// <returns></returns>
    private IEnumerator AutoStateReset(float m_delay,Action m_stateChange)
    {
        yield return new WaitForSeconds(m_delay);
        m_stateChange?.Invoke();
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
        if (IsDead)
            return false;

        this._hp = Mathf.Clamp(_hp - m_value, 0, _hp - m_value);

        

        if (IsDead == true)
        {
            _hpBar.gameObject.SetActive(false);
            Dead();
        }
        else
        {
            DamageText text = Manager.Pool.Get<DamageText>(E_Text.Damage);
            text.SetText(m_value.ToString());
            text.SetPosition(_textPivot);

            if (_hpBar.isActiveAndEnabled == false) { _hpBar.gameObject.SetActive(true); }

            _hpBar.SetHpBarGauge(_maxHP, _hp);

            for (int i = 0; i < _renderers.Length; i++)
            {
                Utils.DamageColorChange(this, _renderers[i], Color.white);
            }
        }

        return IsDead;
    }

    #region 디버깅

    private void OnDrawGizmos()
    {
        // 후방 다른 몬스터 감지범위 디버깅
        if (_drawBackCast == true)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(LiftBoxCenter, LiftBoxSize);
        }
        
        // 공격 대상 오브젝트 감지범위 디버깅
        if (_drawRangeArea == true)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position + Vector3.up * _coll.size.y / 2, _range);
        }
    }

    #endregion
}
