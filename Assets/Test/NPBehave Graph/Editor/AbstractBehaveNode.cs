using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

namespace UnityEditor.NPBehaveGraph
{
    public class AbstractBehaveNode : JsonObject
    {
        [SerializeField]
        private string m_Name;
    }
}


