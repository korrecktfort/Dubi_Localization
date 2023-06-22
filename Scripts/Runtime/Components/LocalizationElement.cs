using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dubi.Localization
{
    public class LocalizationElement : MonoBehaviour
    {
        [SerializeField] LocalizedDataObject localizedDataObject = null;
        [SerializeField] CurrentLanguageValue currentLanguage = null;
        [SerializeField] TextMeshProUGUI textMeshPro = null;
        RectTransform rectTransform;

        private void Awake()
        {
            this.rectTransform = GetComponent<RectTransform>();
        }

        public void OnEnable()
        {
            this.currentLanguage.RegisterCallback(OnLanguageChanged);
        }

        public void OnDisable()
        {
            this.currentLanguage.DeregisterCallback(OnLanguageChanged);
        }  

        void OnLanguageChanged(int index)
        {
            if(this.localizedDataObject != null)
            {
                this.textMeshPro.text = this.localizedDataObject.GetLocalization(index).Text;
                LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
            }
        }

        public void InjectLocalizedString(LocalizedDataObject localizedStringObject)
        {
            this.localizedDataObject = localizedStringObject;
            OnLanguageChanged(this.currentLanguage.Value);
        }

        public void DissectLocalizedString(string rawText = "")
        {
            this.localizedDataObject = null;
            this.textMeshPro.text = rawText;
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.rectTransform);
        }
    }
}