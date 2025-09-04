using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Task", "Wait")]
    class NPWait : NPTask
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.Wait;
        public NPWait()
        {
            name = "Wait";
            synonyms = new string[] { "wait" };
        }
    }
}