using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Dubi.Localization
{
    [CreateAssetMenu(menuName = "Dubi/Localization/Localization Object")]
    public class LocalizationObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] Table<LocalizedData> table = new Table<LocalizedData>();
        [SerializeField] CurrentLanguageObject currentLanguageObject = null;       

        private void Awake()
        {
            this.currentLanguageObject = Resources.Load<CurrentLanguageObject>("Current Language Object");
        }

        public int maxNameChar = 25;
        string LocalizedStringsPath
        {
            get
            {
                return Path.GetDirectoryName(AssetDatabase.GetAssetPath(this)) + "/Localized Strings/";
            }
        }

        [ContextMenu("Create Translate Strings")]
        public void UpdateTranslateStrings()
        {
            if(this.currentLanguageObject == null)
            {
                Debug.LogWarning("Aborted - No Current Language Object Assigned", this);
                return;
            }

            string path = LocalizedStringsPath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }

            List<LocalizedDataObject> currentTranslatedStrings = new List<LocalizedDataObject>();

            for (int i = 0; i < this.table.Rows.Length; i++)
            {
                LocalizedData[] array = this.table.Rows[i].array;
                string firstEntry = array[0].Text;
                string assetName = (i < 10 ? "0" : "") + i.ToString() + " - " + firstEntry[..Mathf.Min(this.maxNameChar, firstEntry.Length)].Trim();
                string currentPath = path + assetName + ".asset";

                LocalizedDataObject localizedString = AssetDatabase.LoadAssetAtPath<LocalizedDataObject>(currentPath);

                /// Create new serialized object
                if (localizedString == null)
                {
                    localizedString = ScriptableObject.CreateInstance<LocalizedDataObject>(); 
                    AssetDatabase.CreateAsset(localizedString, currentPath);                    
                }
                
                /// Inject Data
                EditorUtility.SetDirty(localizedString);
                localizedString.Inject(array);
                currentTranslatedStrings.Add(localizedString);
            }            

            AssetDatabase.SaveAssets();

            EditorApplication.delayCall += () =>
            {
                AssetDatabase.Refresh();
            };
        }


        public TextAsset textAsset = null;
        string[] lineEnds = new string[3] { "\n", "\r", "\r\n" };
        [SerializeField] char entrySplit = ',';
        System.Action RefreshInspector = null;


        [ContextMenu("CreateTable")]
        public void ReadInTable()
        {
            if (this.textAsset != null)
            {
                string allText = this.textAsset.text;
                string[] lines = allText.Split(this.lineEnds, System.StringSplitOptions.RemoveEmptyEntries);

                lines = lines[1..lines.Length];

                LocalizedData[][] newTable = new LocalizedData[lines.Length][];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] row = lines[i].Split(this.entrySplit);                                     

                    LocalizedData[] dataRow = new LocalizedData[row.Length];
                    for (int h = 0; h < row.Length; h++)
                    {
                        dataRow[h] = new LocalizedData(row[h]);
                    }
                    newTable[i] = dataRow;
                }

                this.table.Data = new MultiDimensional<LocalizedData>(newTable);

                EditorApplication.delayCall += () =>
                {
                    RefreshInspector?.Invoke();
                };
            }
        }


        [ContextMenu("Write Table")]
        public void WriteTable()
        {
            if (this.textAsset != null)
            {
                string path = AssetDatabase.GetAssetPath(this.textAsset);
                StreamWriter streamWriter = new StreamWriter(path);
                streamWriter.Write(GetLanguagesString());
                streamWriter.Write(GetTranslationsString());
                streamWriter.Close();

                EditorApplication.delayCall += () =>
                {
                    AssetDatabase.Refresh();
                    ReadInTable();                    
                    AssetDatabase.Refresh();

                    // UpdateTranslateStrings();
                };
            }
        }

        string GetLanguagesString()
        {
            if(this.currentLanguageObject == null)
            {
                Debug.LogWarning("Aborted - No Current Language Object Assigned", this);
                return "";
            }

            string main = "";
            int length = this.currentLanguageObject.languages.Count;
            for (int i = 0; i < length; i++)
            {
                main += this.currentLanguageObject.languages[i];
                if (i < length - 1)
                {
                    main += this.entrySplit;
                }
            }

            main += "\n";

            return main;
        }

        string GetTranslationsString()
        {
            string main = "";
            int length = this.currentLanguageObject.languages.Count;
            foreach (SingleDimensional<LocalizedData> row in this.table.Rows)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i < row.array.Length)
                    {
                        main += row.array[i].Text.Trim();
                    }
                    else
                    {
                        main += "EMPTY";
                    }

                    if (i < length - 1)
                    {
                        main += this.entrySplit;
                    }
                }

                main += "\n";
            }

            return main;
        }

        public void OnBeforeSerialize()
        {
            this.table.Titles = this.currentLanguageObject.languages.ToArray();
        }

        public void OnAfterDeserialize()
        {
        }
    }
}