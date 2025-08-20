using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityEditor.NPBehaveGraph
{
    [Title("Composite", "Parallel")]
    class NPParallel : NPComposite
    {
        public NPParallel()
        {
            name = "Parallel";
            synonyms = new string[] { "parallel"};
        }
    }
}
