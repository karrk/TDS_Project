using System.Collections;
using UnityEngine;

public class BoxObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float _hp;
    public float HP => _hp;

    public bool OnDamage(float m_value)
    {
        _hp = Mathf.Clamp(_hp - m_value, 0, float.MaxValue);

        if (_hp <= 0)
        {
            StartCoroutine(StartDestroy());
            return true;
        }

        return false;
    }

    private IEnumerator StartDestroy()
    {
        // 애니메이션 동작

        // 동작 후
        yield return null;

        Destroy(this.gameObject);
    }
}
