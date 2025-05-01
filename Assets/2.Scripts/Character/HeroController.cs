using System.Collections;
using UnityEngine;

public class HeroController : InitBehaviour, IDamageable
{
    private Gun _gun;
    private TargetFinder _finder;
    private SpriteRenderer _renderer;
    [SerializeField] private float _hp;
    [SerializeField] private float _shootingCool;
    [SerializeField, ReadOnly] private bool _isShootable = true;
    [SerializeField, ReadOnly] private bool _isUserControlling = false;
    public float HP => _hp;
    public bool IsDead => _hp <= 0;

    public override int InitOrder => 1;

    protected override void Awake()
    {
        base.Awake();
        _gun = GetComponentInChildren<Gun>();
        _finder = GetComponentInParent<TargetFinder>();
        _renderer = GetComponent<SpriteRenderer>();
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
        Vector3 targetPos;

        while (true)
        {
            if (IsDead == true)
                break;

            if(_isShootable == true)
            {
                targetPos = _finder.GetTargetPos();

                if(targetPos != Vector3.back) 
                {
                    _gun.SetLookTarget(targetPos);
                    _gun.Fire(E_Bullet.BasicBullet);
                    timer = _shootingCool;
                    _isShootable = false;
                    targetPos = Vector3.back;
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
