using UnityEngine;

public class MonsterAnimEvent : MonoBehaviour
{
    private Monster _monsterController;

    private void Awake()
    {
        _monsterController = GetComponent<Monster>();
    }

    /// <summary>
    /// 애니메이션 이벤트 전용 함수
    /// Melee 몬스터 Attack 애니메이션 동작 시 해당 이벤트가 호출됩니다.
    /// 몬스터의 범위 내 감지 대상이 있으며, 데미지를 받을 수 있는 오브젝트라면 데미지를 전달합니다.
    /// </summary>
    public void OnAttack()
    {
        if (_monsterController.Target != null)
        {
            if (_monsterController.Target.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.OnDamage(_monsterController.Power);
            }
        }
    }
}
