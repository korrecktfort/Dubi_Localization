using Dubi.Localization;
using UnityEngine.UIElements;

public class LocalizedLabel : Label, SetLocalization
{
    LocalizedDataObject localizedData = null;

    public LocalizedLabel(LocalizedDataObject localizedData)
    {
        this.localizedData = localizedData;
    }

    public void SetLocalization(int index)
    {
        base.text = this.localizedData.GetLocalization(index).Text;
    }
}
