namespace UnityEditor.NPBehaveGraph
{
    class NPTask : AbstractBehaveNode
    {
        public NPTask()
        {
            UpdateNodeAfterDeserialization();
        }
        
        const int SlotId = 0;
        
        public sealed override void UpdateNodeAfterDeserialization()
        {
            AddSlot(new NPBehaveInputSlot(SlotId));
        }
    }
}