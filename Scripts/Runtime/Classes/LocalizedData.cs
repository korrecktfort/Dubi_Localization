using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dubi.Localization
{
    [System.Serializable]
    public class LocalizedData
    {
        public string Text
        {
            get => this.text;
            set => this.text = value;
        }

        [SerializeField] string text = "";

        public LocalizedData(string text)
        {
            this.text = text;
        }
    }
}
