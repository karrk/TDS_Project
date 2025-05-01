using System.Collections;
using UnityEngine;

public class BoxObject : MonoBehaviour, IDamageable
{
    private StateUIController _ui;
    private SpriteRenderer _renderer;
    [SerializeField] private float _hp;
    public float HP => _hp;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _ui = GetComponentInChildren<StateUIController>(true);
    }

    public bool OnDamage(float m_value)
    {
        _hp = Mathf.Clamp(_hp - m_value, 0, float.MaxValue);

        if (_hp <= 0)
        {
            StartCoroutine(StartDestroy());
            return true;
        }
        else
        {
            Utils.DamageColorChange(this, _renderer, Color.white);

            if (_ui.isActiveAndEnabled == false)
            {
                _ui.gameObject.SetActive(true);
                _ui.InitSliderMaxValue(_hp);
            }
            
            _ui.SetHpSliderValue(_hp);
        }

        return false;
    }

    /// <summary>
    /// 파괴 절차 과정을 수행합니다.
    /// </summary>
    private IEnumerator StartDestroy()
    {
        // 애니메이션 동작

        // 동작 후
        yield return null;

        Destroy(this.gameObject);
    }
}
