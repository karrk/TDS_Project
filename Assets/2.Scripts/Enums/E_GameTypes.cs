/// <summary>
/// 이동 경로 타입
/// </summary>
public enum E_Way
{
    None = -1,
    Top = 0,    // Ground 01
    Mid,        // Ground 02
    Bot,        // Ground 03

    Size
}

/// <summary>
/// 몬스터 행위별 타입
/// </summary>
public enum E_MonsterType
{
    None = -1,
    Melee = 0,
    Range,

    Size
}

public enum E_Bullet
{
    None = -1,
    BasicBullet = 0,

    Size
}

/// <summary>
/// 풀링 요소 타입
/// </summary>
public enum E_Pool
{
    None = -1,
    Monster = 0,
    Bullet,

    Size
}