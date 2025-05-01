using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour, IPooling
{
    public Enum Type => E_Text.Damage;

    public GameObject Obj => gameObject;
    
    [SerializeField,Header("텍스트가 사라지는 시간")] private float _delay;

    private TMP_Text _tmp;

    private Transform _anchor;

    private void Awake()
    {
        _tmp = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartDelayReturn());
    }

    /// <summary>
    /// 위치를 해당 좌표로 설정합니다.
    /// </summary>
    public void SetPosition(Vector3 m_pos)
    {
        this.transform.position = m_pos;
    }

    /// <summary>
    /// 위치를 해당 오브젝트 기반 위치로 설정합니다.
    /// </summary>
    public void SetPosition(Transform m_tr)
    {
        this._anchor = m_tr;
    }

    /// <summary>
    /// 표기되는 Text의 문자열을 수정합니다.
    /// </summary>
    public void SetText(string m_str)
    {
        _tmp.text = m_str;
    }

    /// <summary>
    /// 일정 시간 후 자동 반환을 진행합니다.
    /// </summary>
    private IEnumerator StartDelayReturn()
    {
        yield return new WaitForSeconds(_delay);

        Return();
    }

    public void Return()
    {
        StopCoroutine(StartDelayReturn());
        _anchor = null;
        _tmp.text = "";
        Manager.Pool.Return(this);
    }

    private void Update()
    {
        if (_anchor != null)
        {
            transform.position = _anchor.position;
        }
    }

}
