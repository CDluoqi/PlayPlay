using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

namespace UnityEditor.NPBehaveGraph
{
    class NPBehaveInputSlot : NPBehaveSlot
    {
        public NPBehaveInputSlot() { }

        public NPBehaveInputSlot(int slotId, bool hidden = false)
            :base(slotId, SlotType.InputSlot, hidden)
        {
            
        }
    }
}

