using System.Collections;
using UnityEditor;
using UnityEngine;

public static class Utils
{
    // TODO : 코루틴 매니징작업, yield return null 이 아닌 로직 교체

    /// <summary>
    /// 렌더러의 색상을 데미지 컬러로 변경 후 다시 원상태로 복구시키는 과정입니다.
    /// </summary>
    public static void DamageColorChange(MonoBehaviour m_component, SpriteRenderer m_renderer, Color m_origin)
    {
        m_component.StopCoroutine(StartColor(m_renderer,m_origin));
        m_component.StartCoroutine(StartColor(m_renderer, m_origin));
    }

    /// <summary>
    /// 대상 렌더러의 색상을 서서히 원색으로 변경합니다.
    /// </summary>
    public static IEnumerator StartColor(SpriteRenderer m_renderer, Color m_origin)
    {
        m_renderer.color = IDamageable.Color;

        Color endColor = m_origin;
        float timer = 0f;
        float t;

        while (true)
        {
            if (timer >= IDamageable.ColorChangeDuration)
                break;

            timer += Time.deltaTime;
            t = timer / IDamageable.ColorChangeDuration;
            m_renderer.color = Color.Lerp(IDamageable.Color, endColor, t);

            yield return null;
        }
    }
}

#region 어트리뷰트 추가 정의

public class ReadOnlyAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;  // 다시 활성화
    }
}

#endregion