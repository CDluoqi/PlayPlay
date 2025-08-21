using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Node = UnityEditor.Experimental.GraphView.Node;

namespace UnityEditor.BehaveGraph
{
    sealed class NPBehaveNodeView : Node
    {
        VisualElement m_TitleContainer;

        public NPBehaveNodeView()
        {
            name = "nodeView";
        }

        public void Initialize(AbstractBehaveNode inNode)
        {
            if (inNode == null)
                return;
            
            title = inNode.name;
            var slots = new List<NPBehaveSlot>();
            inNode.GetSlots(slots);
            AddSlots(slots);
            
            m_TitleContainer = this.Q("title");

            if (inNode is NPBehaveBlockNode blockData)
            {
                AddToClassList("blockData");
                m_TitleContainer.RemoveFromHierarchy();
            }
            else
            {
                SetPosition(new Rect(inNode.drawState.position.x, inNode.drawState.position.y, 0, 0));
            }
        }
        
        void AddSlots(IEnumerable<NPBehaveSlot> slots)
        {
            foreach (var slot in slots)
                AddSlot(slot);
            RefreshPorts();
        }

        Port AddSlot(NPBehaveSlot slot)
        {
            if (slot.hidden)
                return null;
            Direction direction = slot.isInputSlot ? Direction.Input : Direction.Output;
            Port port = InstantiatePort(Orientation.Horizontal, direction, Port.Capacity.Single, null);
            port.portName = slot.displayName;
            port.portColor = NPBehaveSlot.slotColor;
            if (slot.isInputSlot)
            {
                inputContainer.Add(port);
            }
            else
            {
                outputContainer.Add(port);
            }
            
            return port;
        }

    }
}

