using System.Collections;
using UnityEngine;

public class FixedObject : MonoBehaviour
{
    private float _fixedLocalPosX;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _fixedLocalPosX = transform.localPosition.x;

        Fix();
    }

    /// <summary>
    /// 해당 오브젝트의 로컬 좌표 X축을 고정시킵니다.
    /// </summary>
    public void Fix()
    {
        StartCoroutine(StartFix());
    }

    /// <summary>
    /// 물체가 중력의 영향을 받을 수 있게 설정합니다.
    /// </summary>
    public void Drop()
    {
        if (isActiveAndEnabled == false)
            return;

        _rb.isKinematic = false;

        StopCoroutine(ConvertKinetic());
        StartCoroutine(ConvertKinetic());
    }

    /// <summary>
    /// N 초 뒤 다시 Kinetic Mode 로 전환합니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ConvertKinetic()
    {
        yield return new WaitForSeconds(1f);

        _rb.isKinematic = true;
    }

    /// <summary>
    /// Fix 로직 시작 코루틴
    /// </summary>
    private IEnumerator StartFix()
    {
        while (true)
        {
            transform.localPosition = new Vector2(_fixedLocalPosX, transform.localPosition.y);
            yield return null;
        }
    }

    /// <summary>
    /// 파괴 시 메인 컨트롤러에게 자신이 파괴됨을 전달합니다.
    /// </summary>
    private void OnDestroy()
    {
        if (transform.parent.TryGetComponent<LinkObjController>(out LinkObjController controller))
        {
            controller.SendDestroySignal(this);
        }
        StopAllCoroutines();
    }
}
