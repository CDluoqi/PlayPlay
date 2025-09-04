using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "TimeMax")]
    class NPTimeMax : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.TimeMax;
        public NPTimeMax()
        {
            name = "TimeMax";
            synonyms = new string[] { "time max" };
        }
    }
}
