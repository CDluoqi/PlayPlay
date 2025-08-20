using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityEditor.NPBehaveGraph
{
    [Title("Composite", "Selector")]
    class NPSelector : NPComposite
    {
        public NPSelector()
        {
            name = "Selector";
            synonyms = new string[] { "selector"};
        }
    }
}