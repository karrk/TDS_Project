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
    /// 카메라에 부착된 콜라이더(트리거)와 충돌시, 
    /// BG컨트롤러에게 다음 좌표를 전달받아 다음 배치되어야 할 위치로 이동합니다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector2(_mainController.GetNextPosX(), STANDARD_Y);
    }
}
