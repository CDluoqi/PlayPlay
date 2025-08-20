using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.NPBehaveGraph
{
    class NPBehaveStackNode : AbstractBehaveNode
    {
        public NPBehaveStackNode()
        {
            UpdateNodeAfterDeserialization();
        }
        
        const int SlotId = 0;
        
        public sealed override void UpdateNodeAfterDeserialization()
        {
            AddSlot(new NPBehaveInputSlot(SlotId));
        }
        
        StackData m_StackData;
        
        public StackData stackData
        {
            get => m_StackData;
            set => m_StackData = value;
        }
    }
}
