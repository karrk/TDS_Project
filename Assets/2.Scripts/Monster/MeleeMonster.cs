using System;

public class MeleeMonster : Monster
{
    public override Enum Type => E_MonsterType.Melee;

    /// <summary>
    /// Melee 몬스터의 구체적인 Attack 동작 과정
    /// </summary>
    protected override void Attack()
    {
        _anim.SetTrigger("IsAttacking");
    }
}
