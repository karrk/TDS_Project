public interface IDamageable
{
    /// <summary>
    /// 대상의 체력을 감소합니다. 이후 체력이 없을경우
    /// 사망상태를 전달합니다.
    /// </summary>
    /// <returns>true = 사망</returns>>
    public bool OnDamage(float m_value);

}