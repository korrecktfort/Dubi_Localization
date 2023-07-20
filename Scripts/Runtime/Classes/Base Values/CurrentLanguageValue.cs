using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dubi.BaseValues;
using System.Linq;

namespace Dubi.Localization
{
    [System.Serializable]
    public class CurrentLanguageValue : GenericBaseValue<int, CurrentLanguageObject, BaseValueUpdater>
    {
        public CurrentLanguageValue() : base(0, true)
        {
        }

        public void InjectLanguageObject(CurrentLanguageObject currentLanguageObject)
        {
            base.ValueObject = currentLanguageObject;
        }
    }
}