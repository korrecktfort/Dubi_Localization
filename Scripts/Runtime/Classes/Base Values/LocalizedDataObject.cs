using UnityEngine;

namespace Dubi.Localization
{
    public class LocalizedDataObject : ScriptableObject
    {
        public LocalizedData[] data = new LocalizedData[0];        

        public void Inject(LocalizedData[] data)
        {
            this.data = data;
        }

        public LocalizedData GetLocalization(int index)
        {
            return this.data[index];
        }
    }
}
