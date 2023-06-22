using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Dubi.Localization;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(LocalizedData))]
public class LocalizedDataDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();
        root.styleSheets.Add(Resources.Load<StyleSheet>("LocalizedDataUSS"));
        root.AddToClassList("root");

        /// Text Area
        TextField textField = new TextField() { multiline = true, doubleClickSelectsWord = true };
        textField.AddToClassList("root__textField");        

        textField.BindProperty(property.FindPropertyRelative("text"));

        ///

        root.Add(textField);

        return root;
    }
}
