using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPFailer")]
    class NPFailer : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Failer;
        public NPFailer()
        {
        }
    }
}
