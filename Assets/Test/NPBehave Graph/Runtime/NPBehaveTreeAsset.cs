using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPBehave
{
    public class NPBehaveTreeAsset : ScriptableObject
    {
        private string _code = "";
        public string Code => _code;
     
        public static NPBehaveTreeAsset Create(string code)
        {
            var asset = CreateInstance<NPBehaveTreeAsset>();
            asset.Initialize(code);
            return asset;
        }

        private void Initialize(string text)
        {
            _code = text;
        }
    }
}
