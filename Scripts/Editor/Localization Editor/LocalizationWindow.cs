using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Dubi.Localization;
using UnityEngine.UIElements;
using Dubi.TableExtension;
using UnityEditor.UIElements;

public class LocalizationWindow : EditorWindow
{
    [MenuItem("Dubi/Localization Editor")]
    static void Init()
    {
        LocalizationWindow window = (LocalizationWindow)EditorWindow.GetWindow(typeof(LocalizationWindow));
        window.Show();
        window.SetupRoot(window.rootVisualElement);
    }

    

    [UnityEditor.Callbacks.OnOpenAssetAttribute(1)]
    public static bool OnOpenAsset(int instanceID)
    {
        LocalizationObject locaObject = EditorUtility.InstanceIDToObject(instanceID) as LocalizationObject;
        if(locaObject != null)
        {
            LocalizationWindow window = (LocalizationWindow)EditorWindow.GetWindow(typeof(LocalizationWindow));
            window.Show();
            window.SetupRoot(window.rootVisualElement, locaObject);
            return true;
        }

        return false;
    }

    void SetupRoot(VisualElement root, LocalizationObject locaObject = null)
    {
        CurrentLanguageObject languageObject = Resources.Load<CurrentLanguageObject>("Current Language Object");
        int languages = languageObject.languages.Count;

        /// To do: Create New Multidimensional Table
        if (locaObject == null)
        {
            return;
        }

        SerializedObject serializedObject = new SerializedObject(locaObject);
        SerializedProperty table = serializedObject.FindProperty("table");

        /// Use Table
        TableElement tableElement = new TableElement();
        root.Add(tableElement);
        tableElement.BindProperty(table);

        Button createStringsButton = new Button() { text = "Create Localizations"};
        createStringsButton.clickable = new Clickable(locaObject.UpdateTranslateStrings);
        tableElement.ToolBar.Add(createStringsButton);

        PropertyField textAssetField = new PropertyField() { label = "Loca File" };
        textAssetField.BindProperty(serializedObject.FindProperty("textAsset"));
        tableElement.ToolBar.Add(textAssetField);

        Button readInTableButton = new Button() { text = "Load Data From File" };
        readInTableButton.clickable = new Clickable(locaObject.ReadInTable);
        tableElement.ToolBar.Add(readInTableButton);

        Button writeToTable = new Button() { text = "Save Data to File" };
        writeToTable.clickable = new Clickable(locaObject.WriteTable);
        tableElement.ToolBar.Add(writeToTable);
    }
}
