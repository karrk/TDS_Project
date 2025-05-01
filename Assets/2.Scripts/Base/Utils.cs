using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Utils
{
    // TODO : 코루틴 매니징작업, yield return null 이 아닌 로직 교체
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