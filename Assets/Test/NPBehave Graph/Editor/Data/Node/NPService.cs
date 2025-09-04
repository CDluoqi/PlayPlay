using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "NPService")]
    class NPService : NPDecorator
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Service;
        public NPService()
        {
            name = "Service";
            synonyms = new string[] { "service"};
        }
    }
}
