using UnityEngine;

public class BackGroundController : InitBehaviour
{
    [SerializeField,Header("블록 간 간격")] private float _blockInterval;
    [SerializeField,ReadOnly, Header("다음 위치 인덱스")] private int _blockIdx = 0;

    public override int InitOrder => 0;

    public override void Init()
    {
        _blockIdx = 0;
    }

    /// <summary>
    /// 다음 지점으로 이동되어야 할 좌표 X 값을 반환합니다.
    /// </summary>
    public float GetNextPosX()
    {
        _blockIdx++;
        return _blockInterval * (_blockIdx+1);
    }
}