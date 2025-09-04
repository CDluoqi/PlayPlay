using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPWaitForCondition")]
    class NPWaitForCondition : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.WaitForCondition;
        public NPWaitForCondition()
        {
            name = "WaitForCondition";
            synonyms = new string[] { "wait for condition" };
        }
    }
}

