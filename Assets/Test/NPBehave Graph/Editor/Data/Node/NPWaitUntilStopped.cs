using NPBehave;

namespace UnityEditor.BehaveGraph
{
    [Title("Task", "WaitUntilStopped")]
    class NPWaitUntilStopped : NPTask
    {
        public override NPBehaveNodeType nodeType => NPBehaveNodeType.WaitUntilStopped;
        public NPWaitUntilStopped()
        {
            name = "WaitUntilStopped";
            synonyms = new string[] { "wait until stopped" };
        }
    }
}
