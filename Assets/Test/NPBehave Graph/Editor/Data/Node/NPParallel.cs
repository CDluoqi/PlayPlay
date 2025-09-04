using System.Collections;
using System.Collections.Generic;
using NPBehave;
using UnityEngine;


namespace UnityEditor.BehaveGraph
{
    [Title("Composite", "Parallel")]
    class NPParallel : NPComposite
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Parallel;
        public NPParallel()
        {
            name = "Parallel";
            synonyms = new string[] { "parallel"};
        }
    }
}
