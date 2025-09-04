using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPInverter")]
    class NPInverter : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Inverter;
        public NPInverter()
        {
        }
    }
}