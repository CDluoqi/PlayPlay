namespace UnityEditor.BehaveGraph
{
    [Title("Root")]
    class NPRoot : AbstractBehaveNode
    {
        public NPRoot()
        {
            name = "Root";
            synonyms = new string[] { "time max" };
            UpdateNodeAfterDeserialization();
        }
        const int SlotId = 0;
        
        public sealed override void UpdateNodeAfterDeserialization()
        {
            AddSlot(new NPBehaveOutputSlot(SlotId));
        }
    }
}
        

