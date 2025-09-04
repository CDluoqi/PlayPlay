using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Task", "NPNavWalkTo")]
    class NPNavWalkTo : NPTask
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.NavWalkTo;
        public NPNavWalkTo()
        {
            name = "NavWalkTo";
            synonyms = new string[] { "nav" };
        }
    }
}
