using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPTimeMin")]
    class NPTimeMin : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.TimeMin;
        public NPTimeMin()
        {
            name = "TimeMin";
            synonyms = new string[] { "time min" };
        }
    }
}
