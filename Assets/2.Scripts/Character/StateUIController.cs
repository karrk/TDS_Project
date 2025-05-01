using UnityEngine;
using UnityEngine.UI;

public class StateUIController : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;

    /// <summary>
    /// 슬라이더의 최대 값을 설정합니다.
    /// </summary>
    public void InitSliderMaxValue(float m_value)
    {
        _hpSlider.maxValue = m_value;
    }

    /// <summary>
    /// 전달하는 값으로 슬레이더 value 를 설정합니다.
    /// </summary>
    public void SetHpSliderValue(float m_value)
    {
        _hpSlider.value = m_value;
    }
}
