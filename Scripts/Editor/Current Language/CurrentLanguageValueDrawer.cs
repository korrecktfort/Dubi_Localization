using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dubi.Localization;
using UnityEditor;

[CustomPropertyDrawer(typeof(CurrentLanguageValue))]
public class CurrentLanguageValueDrawer : BaseValueDrawer<CurrentLanguageObject>
{
    protected override void DisplayValueField(Rect position, SerializedProperty property)
    {
        SerializedObject serializedValueObject = new SerializedObject(base.valueObject.objectReferenceValue);
        SerializedProperty languagesProp = serializedValueObject.FindProperty("languages");
        List<string> stringList = new List<string>();
        for (int i = 0; i < languagesProp.arraySize; i++)
        {
            stringList.Add(languagesProp.GetArrayElementAtIndex(i).stringValue);
        }

        EditorGUI.BeginChangeCheck();
        position.height = EditorGUIUtility.singleLineHeight;
        int value = EditorGUI.Popup(position, property.intValue, stringList.ToArray());

        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = value;
            property.serializedObject.ApplyModifiedProperties();

            base.Call();
        }
    }
}
