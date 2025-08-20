using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

namespace UnityEditor.NPBehaveGraph
{
    class NPBehaveOutputSlot : NPBehaveSlot
    {
        public NPBehaveOutputSlot() { }

        public NPBehaveOutputSlot(int slotId, bool hidden = false)
            :base(slotId, SlotType.OutputSlot, hidden)
        {
            
        }
    }
}