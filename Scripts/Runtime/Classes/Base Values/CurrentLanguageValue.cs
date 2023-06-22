using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rubi.BaseValues;
using System.Linq;

namespace Dubi.Localization
{
    [System.Serializable]
    public class CurrentLanguageValue : GenericBaseValue<int, CurrentLanguageObject, BaseValueUpdater>
    {
        public CurrentLanguageValue() : base(0, true)
        {
        }
    }
}