using NPBehave;

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
    }
}

