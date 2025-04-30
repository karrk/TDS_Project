using UnityEngine;

public class BackGroundObject : MonoBehaviour
{
    private const float STANDARD_Y = 0.1f;
    private BackGroundController _mainController;

    private void Awake()
    {
        _mainController = GetComponentInParent<BackGroundController>();
    }

    /// <summary>
    /// 전달받은 값을 기반으로 콜라이더를 형성합니다.
    /// 전달받은 레이어를 기반으로 각 오브젝트의 레이어를 설정합니다.
    /// </summary>
    /// <param name="m_sizeInfo">콜라이더 사이즈</param>
    /// <param name="m_boundaryPoses">경계선 Y 위치값 배열</param>
    /// <param name="m_layer">콜라이더 감지 레이어</param>
    public void CreateColliderObject(Vector2 m_sizeInfo, float[] m_boundaryPoses, int m_layer)
    {
        for (int i = 0; i < m_boundaryPoses.Length; i++)
        {
            GameObject newObj = new GameObject();
            BoxCollider2D coll = newObj.AddComponent<BoxCollider2D>();
            newObj.transform.SetParent(transform);

            newObj.name = $"Way_{(E_Way)(i)}";

            newObj.layer = m_layer+i;

            coll.size = m_sizeInfo;
            newObj.transform.position =
                new Vector2(transform.position.x, m_boundaryPoses[i] - m_sizeInfo.y/2);

            coll.forceSendLayers = 1<<7+i | 1<<10+i;
        }
    }

    /// <summary>
    /// 카메라에 부착된 콜라이더(트리거)와 충돌시, 
    /// BG컨트롤러에게 다음 좌표를 전달받아 다음 배치되어야 할 위치로 이동합니다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector2(_mainController.GetNextPosX(), STANDARD_Y);
    }
}
