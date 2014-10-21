using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FormattedRect))]
public class EdFormattedRect : Editor
{
    public override void OnInspectorGUI()
    {
        FormattedRect a = (FormattedRect)target;

        GUILayout.Space(5);
        a.rectType = (FormattedRect.RectType)EditorGUILayout.EnumPopup(a.rectType);

        if (a.rectType == FormattedRect.RectType.Standard)
        {
            a.anchor = (TextAnchor)EditorGUILayout.EnumPopup("Anchor", a.anchor);

            EditorGUILayout.BeginHorizontal();
            a.x = EditorGUILayout.FloatField("X", a.x);
            a.xFormat = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.xFormat);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            a.y = EditorGUILayout.FloatField("Y", a.y);
            a.yFormat = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.yFormat);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            a.width = EditorGUILayout.FloatField("Width", a.width);
            a.widthFormat = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.widthFormat);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            a.height = EditorGUILayout.FloatField("Height", a.height);
            a.heightFormat = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.heightFormat);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            a.x = EditorGUILayout.FloatField("Left", a.x);
            a.xFormat = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.xFormat);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            a.y = EditorGUILayout.FloatField("Top", a.y);
            a.yFormat = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.yFormat);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            a.x2 = EditorGUILayout.FloatField("Right", a.x2);
            a.x2Format = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.x2Format);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            a.y2 = EditorGUILayout.FloatField("Bottom", a.y2);
            a.y2Format = (FormattedRect.eFormat)EditorGUILayout.EnumPopup(a.y2Format);
            EditorGUILayout.EndHorizontal();
        }
    }
}

