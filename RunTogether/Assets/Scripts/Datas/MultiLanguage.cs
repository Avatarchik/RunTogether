using System.Collections.Generic;
using UnityEngine;

namespace Datas
{
    [CreateAssetMenu(menuName = "MultiLanguage")]
    public class MultiLanguage : ScriptableObject
    {
        public List<Language> Languages = new List<Language>(); 
    }

    [System.Serializable]
    public class Language
    {
        public string ZH;
        public string EN;

    }
}
