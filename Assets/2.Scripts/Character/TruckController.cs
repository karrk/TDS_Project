using System.Collections;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    [SerializeField] private float _limitVelocity;
    [SerializeField] private float _accelPower;
    [SerializeField] private float _brakeVelocity;
    
    private Rigidbody2D _rb;
    private CapsuleCollider2D _capsuleColl;
    private BoxCollider2D _boxCollider;
    public float Speed => _rb.velocity.x;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _capsuleColl = GetComponent<CapsuleCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(MoveStart());
    }

    private IEnumerator MoveStart()
    {
        while (true)
        {
            Move();

            yield return null;
        }
    }

    public void SetActiveWall(bool m_active)
    {
        this._boxCollider.enabled = m_active;
    }

    private void Move()
    {
        if (_rb.velocity.x < _brakeVelocity)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (_rb.velocity.x > _limitVelocity)
            return;

        _rb.velocity += Vector2.right * _accelPower * Time.deltaTime;
    }
}
