using UnityEngine;
using System;

[Serializable]
public class DataManager
{
    private float[] _groundTopEdges;
    [SerializeField] private MonsterData[] _monsterDatas;
    [SerializeField] private BulletData[] _bulletDatas;

    public DataManager()
    {
        _groundTopEdges = new float[(int)E_Way.Size];
    }

    /// <summary>
    /// 바닥 콜라이더 상단 위치값을 설정합니다.
    /// </summary>
    public void RegistWayPos(E_Way m_wayType, float m_posY)
    {
        //Debug.Log($"{m_wayType} {m_posY}");

        _groundTopEdges[(int)m_wayType] = m_posY;
    }

    /// <summary>
    /// 지정한 타입의 몬스터 데이터를 반환합니다.
    /// </summary>
    public MonsterData GetMonsterData(E_MonsterType m_type)
    {
        foreach (var data in _monsterDatas)
        {
            if (data.Type == m_type)
                return data;
        }

        throw new Exception("찾는 몬스터 데이터가 없습니다.");
    }

    /// <summary>
    /// 지정한 타입의 총알 데이터를 반환합니다.
    /// </summary>
    public BulletData GetBulletData(E_Bullet m_type)
    {
        foreach (var data in _bulletDatas)
        {
            if (data.Type == m_type)
                return data;
        }

        throw new Exception("찾는 총알 데이터가 없습니다.");
    }

    /// <summary>
    /// 각 경로의 콜라이더 상단 경계면의 높이값을 반환합니다.
    /// </summary>
    public float TopEdgeY(E_Way m_wayType)
    {
        return _groundTopEdges[(int)m_wayType];
    }
}
