namespace UnityEditor.BehaveGraph
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