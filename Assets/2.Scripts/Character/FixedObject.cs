using System.Collections;
using UnityEngine;

public class FixedObject : MonoBehaviour
{
    private float _fixedLocalPosX;
    private float _fixDuration = 1f;

    private void Start()
    {
        _fixedLocalPosX = transform.localPosition.x;
    }

    /// <summary>
    /// 오브젝트가 떨어지는 동안 물체의 위치를 보정합니다.
    /// </summary>
    public void Fix()
    {
        if (isActiveAndEnabled == false)
            return;

        StopCoroutine(StartFix());
        StartCoroutine(StartFix());
    }

    /// <summary>
    /// Fix 로직 시작 코루틴
    /// </summary>
    private IEnumerator StartFix()
    {
        float time = _fixDuration;

        while (true)
        {
            if (_fixDuration <= 0)
                break;

            transform.localPosition = new Vector2(_fixedLocalPosX, transform.localPosition.y);

            yield return null;
            _fixDuration -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 파괴 시 메인 컨트롤러에게 자신이 파괴됨을 전달합니다.
    /// </summary>
    private void OnDestroy()
    {
        if(this.transform.parent.TryGetComponent<LinkObjController>(out LinkObjController controller))
        {
            controller.SendDestroySignal(this);
        }
    }
}
