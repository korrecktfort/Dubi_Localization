using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Dubi.Localization;

[CustomEditor(typeof(CurrentLanguageObject))]
public class CurrentLanguageObjectDrawer : Editor
{  
    public override void OnInspectorGUI()
    {    
        EditorGUI.BeginChangeCheck();
        SerializedProperty languagesProp = base.serializedObject.FindProperty("languages");
        SerializedProperty valueProp = base.serializedObject.FindProperty("value");

        List<string> list = new List<string>();
        for (int i = 0; i < languagesProp.arraySize; i++)
        {
            list.Add(languagesProp.GetArrayElementAtIndex(i).stringValue);
        }

        int selected = EditorGUILayout.Popup(new GUIContent("Current Language"), valueProp.intValue, list.ToArray());

        if (EditorGUI.EndChangeCheck())
        {
            valueProp.intValue = selected;
            valueProp.serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(languagesProp, true);
        if (EditorGUI.EndChangeCheck())
        {
            base.serializedObject.ApplyModifiedProperties();
        }       
    }
}
