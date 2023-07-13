using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dubi.Localization.Ui
{
    public enum FontStyle
    {
        h1,
        h2,
        h3,
        h4,
        h5,
        h6,
        caption,
        paragraph,
    }

    public class LocalizedTextElement : TextElement, SetLocalization
    {
        LocalizedDataObject data = null;
        public LocalizedDataObject Data
        {
            get => this.data;
            set
            {
                this.data = value;

                if (Application.isPlaying)
                    SetLocalization(LocalizationManager.Instance.CurrentLanguage);
            }
        }

        FontStyle fontStyle = FontStyle.paragraph;
        public FontStyle FontStyle
        {
            get => this.fontStyle;
            set
            {
                this.fontStyle = value;

                RemoveFromClassList("localized-text-element--h1");
                RemoveFromClassList("localized-text-element--h2");
                RemoveFromClassList("localized-text-element--h3");
                RemoveFromClassList("localized-text-element--h4");
                RemoveFromClassList("localized-text-element--h5");
                RemoveFromClassList("localized-text-element--h6");
                RemoveFromClassList("localized-text-element--caption");
                RemoveFromClassList("localized-text-element--paragraph");

                switch (value)
                {
                    case FontStyle.h1:
                        AddToClassList("localized-text-element--h1");
                        break;
                    case FontStyle.h2:
                        AddToClassList("localized-text-element--h2");
                        break;
                    case FontStyle.h3:
                        AddToClassList("localized-text-element--h3");
                        break;
                    case FontStyle.h4:
                        AddToClassList("localized-text-element--h4");
                        break;
                    case FontStyle.h5:
                        AddToClassList("localized-text-element--h5");
                        break;
                    case FontStyle.h6:
                        AddToClassList("localized-text-element--h6");
                        break;
                    case FontStyle.caption:
                        AddToClassList("localized-text-element--caption");
                        break;
                    case FontStyle.paragraph:
                        AddToClassList("localized-text-element--paragraph");
                        break;
                }
            }
        }

        LocalizationManager localizationManager = null;

        public LocalizedTextElement()
        {
            AddToClassList("localized-text-element");            
            this.RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            this.RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent evt)
        {
            if (!Application.isPlaying)
                return;

            if(this.localizationManager == null)
                this.localizationManager = LocalizationManager.Instance;

            this.localizationManager.Register(this);            
        }
        private void OnDetachFromPanel(DetachFromPanelEvent evt)
        {
            if (!Application.isPlaying)
                return;
                
            this.localizationManager.Deregister(this);            
        }

        public new class UxmlFactory : UxmlFactory<LocalizedTextElement, UxmlTraits> { }

        public new class UxmlTraits : TextElement.UxmlTraits
        {
#if UNITY_2023_OR_NEWER
            public UxmlAssetAttributeDescription<LocalizedDataObject> Data = new() { name = "data", defaultValue = null };
#endif
    
            public UxmlEnumAttributeDescription<FontStyle> fontStyle = new() { name = "font-style" };   
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                LocalizedTextElement element = ve as LocalizedTextElement;
#if UNITY_2023_OR_NEWER
                if(Data.TryGetValueFromBag(bag, cc, out LocalizedDataObject data))
                    element.Data = data;
#endif
                element.FontStyle = this.fontStyle.GetValueFromBag(bag, cc);
            }
        }

        public void SetLocalization(int index)
        {
            if (this.data == null)
                return;

            this.text = this.data.GetLocalization(index).Text;
        }
    }
}
