using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Node = UnityEditor.Experimental.GraphView.Node;

namespace UnityEditor.NPBehaveGraph
{
    public class NPBehaveNodeView : Node
    {
        public void Initialize(AbstractBehaveNode node)
        {
            title = "Root";
        }
    }
}

