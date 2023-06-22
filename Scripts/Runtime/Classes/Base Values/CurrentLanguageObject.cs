using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rubi.BaseValues;
using System.Linq;

namespace Dubi.Localization
{
    [CreateAssetMenu(menuName ="Dubi/Localization/Current Language Object")]
    public class CurrentLanguageObject : GenericValueObject<int>
    {
        public List<string> languages = new List<string>();
    }
}

