using UnityEngine;

public class HeroController : MonoBehaviour, IDamageable
{
    private SpriteRenderer _renderer;
    [SerializeField] private float _hp;
    public float HP => _hp;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public bool OnDamage(float m_value)
    {
        _hp = Mathf.Clamp(_hp - m_value, 0, float.MaxValue);

        if (_hp <= 0)
        {
            //StartCoroutine(StartDestroy());
            return true;
        }
        else
        {
            Utils.DamageColorChange(this, _renderer, Color.white);
        }

        return false;
    }
}
