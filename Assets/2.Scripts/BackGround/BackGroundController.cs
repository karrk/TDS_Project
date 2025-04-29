using UnityEngine;

public class BackGroundController : InitBehaviour
{
    [SerializeField,Header("블록 간 간격")] private float _blockInterval = 0;
    [SerializeField,ReadOnly, Header("다음 위치 인덱스")] private int _blockIdx = 0;
    [SerializeField, Header("중앙 경로 콜라이더 상단 경계면 높이값")] private float _centerWayPosY = 0;
    [SerializeField, Header("모든 경계면 그리기")] private bool _drawAllWays = false;
    [SerializeField, Header("경로 간 간격(높이)")] private float _wayInterval = 0;
    [SerializeField, Header("바닥면 설정 레이어(상단 레이어)")] private LayerMask _wayLayer; 
    
    public override void Init()
    {
        _blockIdx = 0;
        RequestCreateColliders();
    }

    /// <summary>
    /// 다음 지점으로 이동되어야 할 좌표 X 값을 반환합니다.
    /// </summary>
    public float GetNextPosX()
    {
        _blockIdx++;
        return _blockInterval * (_blockIdx+1);
    }

    /// <summary>
    /// 하위 오브젝트 중 BackGroundObject 컴포넌트를 가지고 있다면
    /// 대상에게 설정된 경계면 위치 값에 맞는 콜라이더를 생성하도록 요청합니다.
    /// </summary>
    private void RequestCreateColliders()
    {
        Vector2 collSize = new Vector2(_blockInterval, 1);
        float[] yPoses = new float[(int)E_Way.Size];
        int layerNumber = Mathf.RoundToInt(Mathf.Log(_wayLayer.value, 2));

        for (int i = 0; i < yPoses.Length; i++)
        {
            yPoses[i] = _centerWayPosY + _wayInterval - (_wayInterval * i);

            Manager.Data.RegistWayPos((E_Way)i, yPoses[i]);
        }

        foreach (var bgObject in GetComponentsInChildren<BackGroundObject>())
        {
            bgObject.CreateColliderObject(collSize, yPoses, layerNumber);
        }
    }


    #region Debug

    private void OnDrawGizmos()
    {
        DrawLineWayLine();
    }

    /// <summary>
    /// 각 바닥면의 콜라이더 상단 부 위치를 확인합니다.
    /// 인스펙터 상 DrawAllWays 활성 시 모든 바닥 면 위치를 확인합니다.
    /// </summary>
    private void DrawLineWayLine()
    {
        Vector2 baseStart = new Vector2(-_blockInterval / 2f, _centerWayPosY);
        Vector2 baseEnd = new Vector2(_blockInterval / 2f, _centerWayPosY);

        // 기본 라인 (흰색)
        Debug.DrawLine(baseStart, baseEnd, Color.white);

        if (_drawAllWays)
        {
            // 위쪽 라인 (빨간색)
            Vector2 upOffset = Vector2.up * _wayInterval;
            Debug.DrawLine(baseStart + upOffset, baseEnd + upOffset, Color.red);

            // 아래쪽 라인 (초록색)
            Vector2 downOffset = Vector2.down * _wayInterval;
            Debug.DrawLine(baseStart + downOffset, baseEnd + downOffset, Color.green);
        }
    }
    #endregion
}