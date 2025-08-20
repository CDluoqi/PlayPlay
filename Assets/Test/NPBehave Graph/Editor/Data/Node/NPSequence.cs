using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace UnityEditor.NPBehaveGraph
{
    [Title("Composite", "Sequence")]
    class NPSequence : NPComposite
    {
        public NPSequence()
        {
            name = "Sequence";
            synonyms = new string[] { "sequence", "arr" };
        }
    }
}

