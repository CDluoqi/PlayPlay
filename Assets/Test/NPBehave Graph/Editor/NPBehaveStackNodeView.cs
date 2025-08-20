using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace UnityEditor.NPBehaveGraph
{
    sealed class NPBehaveStackNodeView : StackNode
    {
        StackData m_StackData;
        public StackData stackData => m_StackData;
        
        EditorWindow m_EditorWindow;

        private VisualElement m_SlotContainer;

        public NPBehaveStackNodeView(AbstractBehaveNode inNode, EditorWindow editorWindow)
        {
            name = "stackNodeViewRoot";
            VisualElement titleContainer = new VisualElement {name = "titleContainer"};

            var titleLabel = new Label
            {
                name = "titleLabel",
                text = inNode.name,
            };
            titleContainer.Add(titleLabel);
            
            headerContainer.Add(titleContainer);
            headerContainer.Add(titleContainer);
            
            m_SlotContainer  = new VisualElement {name = "slotContainer" };
            
            headerContainer.Add(m_SlotContainer);
            var slots = new List<NPBehaveSlot>();
            inNode.GetSlots(slots);
            AddSlots(slots);

            m_StackData = new StackData();
            m_EditorWindow = editorWindow;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (evt.target is NPBehaveNodeView) return;
            InsertCreateNodeAction(evt, childCount, 0);
            evt.menu.InsertSeparator(null, 1);
        }
        
        void InsertCreateNodeAction(ContextualMenuPopulateEvent evt, int separatorIndex, int itemIndex)
        {
            var mousePosition = evt.mousePosition + m_EditorWindow.position.position;
            var graphView = GetFirstAncestorOfType<NPBehaveGraphView>();

            evt.menu.InsertAction(itemIndex, "Add Child", (e) =>
            {
                var context = new NodeCreationContext
                {
                    screenMousePosition = mousePosition,
                    target = this,
                    index = separatorIndex,
                };
                graphView.nodeCreationRequest(context);
            });
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
            m_SlotContainer.Add(port);
            
            return port;
        }
        
        public void InsertBlock(NPBehaveNodeView nodeView)
        {
            AddElement(nodeView);
        }
    }
}

