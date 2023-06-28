using UnityEngine.UIElements;

namespace Dubi.Localization
{
    public class LocalizedLabel : Label, IPostLanguageChanged
    {
        LocalizedDataObject data = null;

        public LocalizedLabel() { }

        public LocalizedLabel(LocalizedDataObject data)
        {
            this.data = data;
            base.text = this.data.GetLocalization(0).Text;
        }

        public LocalizedLabel(string text) : base(text) { }

        public new class UxmlFactory : UxmlFactory<LocalizedLabel, UxmlTraits> { }

        public new class UxmlTraits : Label.UxmlTraits
        {       
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                LocalizedLabel element = (LocalizedLabel)ve;
            }
        }

        public void InjectLocalizedData(LocalizedDataObject data)
        {
            this.data = data;
        }

        public void OnLanguageChanged(int index)
        {
            base.text = this.data.GetLocalization(index).Text;
        }
    }
}
