using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPSucceeder")]
    class NPSucceeder : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Succeeder;
        public NPSucceeder()
        {
            name = "Succeeder";
            synonyms = new string[] { "succeeder"};
        }
    }
}
