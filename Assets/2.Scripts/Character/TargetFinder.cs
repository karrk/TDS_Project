using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private Vector2 _finderCenterOffset;
    [SerializeField] private Vector2 _finderSize;
    [SerializeField] private bool _drawFindArea;
    [SerializeField] private LayerMask _layers;
    
    private Vector2 _finderCenter => (Vector2)transform.position + _finderCenterOffset;

    /// <summary>
    /// 타게팅 가능한 오브젝트의 위치를 반환받습니다.
    /// </summary>
    public Vector3 GetTargetPos()
    {
        Collider2D target = FindTargets();

        if (target == null)
        {
            return Vector3.back;
        }

        return target.transform.position;
    }

    /// <summary>
    /// 우선순위가 높은 타겟, 가까운 타겟 순으로 타겟을 탐색합니다.
    /// </summary>
    private Collider2D FindTargets()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(_finderCenter, _finderSize, 0, _layers);
        int maxPriority = 0;
        Collider2D coll = null;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].TryGetComponent<ITargeting>(out ITargeting target))
            {
                if (target.IsNoneTarget == true)
                    continue;

                // 기존에 찾았던 대상이 없었다면 현재 오브젝트로 바로 적용한다.
                if (coll == null)
                {
                    coll = colls[i];
                    continue;
                }

                // 찾은 대상의 우선순위가 더 높다면 해당 오브젝트로 적용한다.
                if (maxPriority < target.TargetPriority)
                {
                    coll = colls[i];
                    continue;
                }

                // 현재 오브젝트가 기존 오브젝트보다 더 가깝다면 변경한다.
                if (colls[i].transform.position.x < coll.transform.position.x)
                {
                    coll = colls[i];
                }
            }
        }

        return coll;
    }

    private void OnDrawGizmos()
    {
        if (_drawFindArea == true)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(_finderCenter, _finderSize);
        }
    }
}
