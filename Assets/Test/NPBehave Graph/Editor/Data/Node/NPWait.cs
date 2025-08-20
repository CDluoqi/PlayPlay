namespace UnityEditor.NPBehaveGraph
{
    [Title("Task", "Wait")]
    class NPWait : NPTask
    {
        public NPWait()
        {
            name = "Wait";
            synonyms = new string[] { "wait" };
        }
    }
}