using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPObserver")]
    class NPObserver : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Observer;
        public NPObserver()
        {
            name = "Observer";
            synonyms = new string[] { "observer"};
        }
    }
}
