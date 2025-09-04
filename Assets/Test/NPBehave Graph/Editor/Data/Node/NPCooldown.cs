using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPCooldown")]
    class NPCooldown : AbstractBehaveNode
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Cooldown;
        public NPCooldown()
        {
        }
    }
}
