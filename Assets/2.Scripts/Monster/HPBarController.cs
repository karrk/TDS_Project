using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField] private GameObject _hpBackGround;
    [SerializeField] private Image _hpBar;

    /// <summary>
    /// 최대체력과 현재체력의 비율로 게이지를 표기합니다.
    /// </summary>
    public void SetHpBarGauge(float m_maxHP, float m_currentHP)
    {
        float ratio = Mathf.Clamp01(m_currentHP / m_maxHP);
        _hpBar.fillAmount = ratio;
    }
}
