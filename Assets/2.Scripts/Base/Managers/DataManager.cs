using UnityEngine;

[System.Serializable]
public class DataManager
{
    private float[] _groundTopEdges;

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
    /// 각 경로의 콜라이더 상단 경계면의 높이값을 반환합니다.
    /// </summary>
    public float TopEdgeY(E_Way m_wayType)
    {
        return _groundTopEdges[(int)m_wayType];
    }
}
