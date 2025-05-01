using System.Collections;
using UnityEngine;

public class HeroController : InitBehaviour, IDamageable
{
    private Gun _gun;
    private TargetFinder _finder;
    private SpriteRenderer _renderer;
    private TruckController _truck;
    [SerializeField] private float _hp;
    [SerializeField] private float _shootingCool;
    [SerializeField, ReadOnly] private bool _isShootable = true;
    [SerializeField, ReadOnly] private bool _isUserControlling = false;
    public float HP => _hp;
    public bool IsDead => _hp <= 0;

    public override int InitOrder => 1;
    private Vector3 shootPos;

    public Vector3 CurrentPos => transform.position;

    protected override void Awake()
    {
        base.Awake();
        _gun = GetComponentInChildren<Gun>();
        _finder = GetComponentInParent<TargetFinder>();
        _renderer = GetComponent<SpriteRenderer>();
        _truck = GetComponentInParent<TruckController>();
    }

    private void Start()
    {
        Manager.Input.OnInputClick += UseControl;
        Manager.Input.OnInputClickUp += StopControl;
    }

    private void OnDisable()
    {
        Manager.Input.OnInputClick -= UseControl;
        Manager.Input.OnInputClickUp -= StopControl;

        _truck.Stop();
    }

    /// <summary>
    /// 유저의 조작을 진행
    /// 마우스 입력 위치로 총구의 방향을 결정합니다. 
    /// </summary>
    private void UseControl(Vector2 m_inputPos)
    {
        Vector3 screenPos = new Vector3(m_inputPos.x, m_inputPos.y, 10);
        shootPos = Camera.main.ScreenToWorldPoint(screenPos);

        _isUserControlling = true;
        _gun.transform.LookAt(shootPos);
        _gun.ShowAimArea(shootPos);
    }

    /// <summary>
    /// 유저의 조작을 중단합니다.
    /// </summary>
    private void StopControl()
    {
        _gun.HideAimArea();
        _isUserControlling = false;
    }

    public bool OnDamage(float m_value)
    {
        _hp = Mathf.Clamp(_hp - m_value, 0, float.MaxValue);

        if (_hp <= 0)
        {
            GetComponentInParent<TruckController>().SetActiveWall(false);
            Destroy(this.gameObject);
        }
        else
        {
            Utils.DamageColorChange(this, _renderer, Color.white);
        }

        return IsDead;
    }

    /// <summary>
    /// 쿨타임에 따른 사격을 진행합니다.
    /// </summary>
    private IEnumerator StartShooting()
    {
        float timer = 0;

        while (true)
        {
            if (IsDead == true)
                break;

            if(_isShootable == true)
            {
                if(_isUserControlling == false)
                    shootPos = _finder.GetTargetPos();

                if(shootPos != Vector3.back) 
                {
                    _gun.SetLookTarget(shootPos);
                    _gun.Fire(E_Bullet.BasicBullet);
                    timer = _shootingCool;
                    _isShootable = false;
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }


            if (timer <= 0)
                _isShootable = true;

            yield return null;
        }
    }

    public override void Init()
    {
        _gun.SetBullet(E_Bullet.BasicBullet);
        StartCoroutine(StartShooting());
    }
}
