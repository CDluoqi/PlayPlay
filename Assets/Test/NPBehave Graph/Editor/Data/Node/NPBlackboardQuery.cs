using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPBlackboardQuery")]
    class NPBlackboardQuery : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.BlackboardQuery;
        public NPBlackboardQuery()
        {
        }
    }
}