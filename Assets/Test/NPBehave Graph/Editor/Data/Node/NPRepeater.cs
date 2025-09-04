using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPRepeater")]
    class NPRepeater : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Repeater;
        public NPRepeater()
        {
            name = "Repeater";
            synonyms = new string[] { "repeater"};
        }
    }
}
