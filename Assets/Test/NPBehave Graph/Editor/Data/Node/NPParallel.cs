using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityEditor.BehaveGraph
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
