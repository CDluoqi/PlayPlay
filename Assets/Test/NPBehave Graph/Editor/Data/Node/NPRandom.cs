using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPRandom")]
    class NPRandom : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Random;
        public NPRandom()
        {
        }
    }
}
