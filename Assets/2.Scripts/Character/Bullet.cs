using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooling
{
    [SerializeField] private float _lifeTime;
    private float _timer;
    private float _velocity;
    private float _damage;

    private E_Bullet _type;
    public Enum Type => _type;

    public GameObject Obj => gameObject;

    private void OnEnable()
    {
        _timer = _lifeTime;
    }

    public void SetType(E_Bullet m_type)
    {
        this._type = m_type;
    }

    public void SetVelocity(float m_value)
    {
        this._velocity = m_value;
    }

    public void SetDamage(float m_value)
    {
        this._damage = m_value;
    }

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
        Manager.Pool.Return(this);
    }
}
