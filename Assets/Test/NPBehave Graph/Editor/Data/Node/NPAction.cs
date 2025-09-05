using NPBehave;
using UnityEditor.BehaveGraph.Drawing.Controls;
using UnityEngine;

namespace UnityEditor.BehaveGraph
{
    [Title("Task", "Action")]
    class NPAction : NPTask
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Action;
        public NPAction()
        {
            name = "Action";
            synonyms = new string[] { "action" };
        }
        
        [SerializeField]
        private string m_ActionName = "";
        
        [ActionNameControl("Name")]
        public string ActionName
        {
            get { return m_ActionName; }
            set
            {
                if (m_ActionName == value)
                    return;

                m_ActionName = value;
                Dirty(ModificationScope.Graph);
            }
        }

    }
}

