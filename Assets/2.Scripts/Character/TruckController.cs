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

    /// <summary>
    /// 몬스터가 영역을 넘지 못하는 벽을 활성/비활성화 합니다.
    /// </summary>
    public void SetActiveWall(bool m_active)
    {
        this._boxCollider.enabled = m_active;
    }

    /// <summary>
    /// 인스펙터에서 지정된 속도로 이동을 시작합니다.
    /// </summary>
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

    /// <summary>
    /// 트럭의 속도를 0으로 변경합니다.
    /// </summary>
    public void Stop()
    {
        _accelPower = 0;
    }
}
