using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "Condition")]
    class NPCondition : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Condition;
        public NPCondition()
        {
            name = "Condition";
            synonyms = new string[] { "condition"};
        }
    }
}
