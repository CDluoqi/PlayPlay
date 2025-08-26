using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UnityEditor.BehaveGraph
{
    class NPBehaveGraphEditorView : VisualElement, IDisposable 
    {
        EditorWindow m_EditorWindow;
        NPBehaveGraphView m_GraphView;
        GraphData m_Graph;
        
        public Action saveRequested { get; set; }
        SearchWindowProvider m_SearchWindowProvider;
        EdgeConnectorListener m_EdgeConnectorListener;
        public NPBehaveGraphView graphView 
        { 
            get { return m_GraphView; } 
        }
        
        public NPBehaveGraphEditorView(EditorWindow editorWindow, GraphData graph) 
        {
            m_EditorWindow = editorWindow;
            m_Graph = graph;

            styleSheets.Add(Resources.Load<StyleSheet>("Styles/NPBehaveGraphEditorView"));
            
            var toolbar = new IMGUIContainer(() =>
            {
                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                if(GUILayout.Button("Save Asset", EditorStyles.toolbarButton))
                {
                    if (saveRequested != null)
                        saveRequested();
                }
                GUILayout.Space(6);
                GUILayout.FlexibleSpace();
                
                GUILayout.EndHorizontal();
            });
            Add(toolbar);
            
            var content = new VisualElement { name = "content" }; 
            { 
                m_GraphView = new NPBehaveGraphView() 
                { 
                    name = "GraphView", viewDataKey = "NPBehaveGraphView" 
                }; 
                m_GraphView.SetupZoom(0.05f, 8); 
                m_GraphView.AddManipulator(new ContentDragger()); 
                m_GraphView.AddManipulator(new SelectionDragger()); 
                m_GraphView.AddManipulator(new RectangleSelector()); 
                m_GraphView.AddManipulator(new ClickSelector()); 
                content.Add(m_GraphView); 
                
                RegisterCallback<GeometryChangedEvent>(ApplySerializedWindowLayouts);
                
                m_GraphView.graphViewChanged = GraphViewChanged;
            }
            
            m_SearchWindowProvider = ScriptableObject.CreateInstance<NPBehaveSearchProvider>(); 
            m_SearchWindowProvider.Initialize(editorWindow, m_Graph, m_GraphView); 
            m_GraphView.nodeCreationRequest = NodeCreationRequest;
            m_EdgeConnectorListener = new EdgeConnectorListener(m_Graph, m_SearchWindowProvider, editorWindow);
            
            
            AddNodes(graph.GetNodes<AbstractBehaveNode>());
            AddBlocks(graph.GetNodes<NPBehaveBlockNode>());
            AddEdges(graph.edges);
            Add(content);
            
            //content.StretchToParentSize();
            this.StretchToParentSize();
        }

        void ApplySerializedWindowLayouts(GeometryChangedEvent evt)
        {
            UnregisterCallback<GeometryChangedEvent>(ApplySerializedWindowLayouts);
        }
        
        void NodeCreationRequest(NodeCreationContext c) 
        { 
            if (EditorWindow.focusedWindow == m_EditorWindow)
            {
                m_SearchWindowProvider.target = c.target; 
                var displayPosition = (c.screenMousePosition - m_EditorWindow.position.position);
                NPBehaveStackNodeView stackNodeView = c.target as NPBehaveStackNodeView;
                SearcherWindow.Show(m_EditorWindow, (m_SearchWindowProvider as NPBehaveSearchProvider).LoadSearchWindow(),
                    item => (m_SearchWindowProvider as NPBehaveSearchProvider).OnSearcherSelectEntry(item, c.screenMousePosition - m_EditorWindow.position.position, stackNodeView),
                    displayPosition, null, new SearcherWindow.Alignment(SearcherWindow.Alignment.Vertical.Center, SearcherWindow.Alignment.Horizontal.Left)); 
            } 
        }

        GraphViewChange GraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    var leftSlot = edge.output.GetSlot();
                    var rightSlot = edge.input.GetSlot();
                    if (leftSlot != null && rightSlot != null)
                    {
                        m_Graph.Connect(leftSlot.slotReference, rightSlot.slotReference);
                    }
                }
                graphViewChange.edgesToCreate.Clear();
            }
            
            if (graphViewChange.movedElements != null)
            {
                foreach (var element in graphViewChange.movedElements)
                {
                    if (element.userData is AbstractBehaveNode node)
                    {
                        var drawState = node.drawState;
                        drawState.position = element.parent.ChangeCoordinatesTo(m_GraphView.contentViewContainer, element.GetPosition());
                        node.drawState = drawState;
                    }
                }
            }
            return graphViewChange;
        }

        void AddNodes(IEnumerable<AbstractBehaveNode> nodes)
        {
            foreach (var node in nodes)
            {
                // Skip BlockNodes as we need to order them
                if (node is NPBehaveBlockNode)
                    continue;

                AddNode(node);
            }
        }
        
        void AddBlocks(IEnumerable<NPBehaveBlockNode> blocks)
        {
            foreach (var node in blocks.OrderBy(s => s.index))
            {
                AddNode(node);
            }
        }
        
        HashSet<IBehaveNodeView> m_NodeViewHashSet = new HashSet<IBehaveNodeView>();

        public void HandleGraphChanges(bool wasUndoRedoPerformed)
        {
            foreach (var node in m_Graph.addedNodes)
            {
                AddNode(node);
            }
            
            var nodesToUpdate = m_NodeViewHashSet;
            nodesToUpdate.Clear();
            
            foreach (var edge in m_Graph.removedEdges)
            {
                var edgeView = m_GraphView.graphElements.ToList().OfType<UnityEditor.Experimental.GraphView.Edge>().FirstOrDefault(p => p.userData is IEdge && Equals((IEdge)p.userData, edge));
                if (edgeView != null)
                {
                    var nodeView = (IBehaveNodeView)edgeView.input.node;
                    if (nodeView?.node != null)
                    {
                        nodesToUpdate.Add(nodeView);
                    }

                    edgeView.output.Disconnect(edgeView);
                    edgeView.input.Disconnect(edgeView);

                    edgeView.output = null;
                    edgeView.input = null;

                    m_GraphView.RemoveElement(edgeView);
                }
            }
            
            foreach (var edge in m_Graph.addedEdges)
            {
                var edgeView = AddEdge(edge);
                if (edgeView != null)
                    nodesToUpdate.Add((IBehaveNodeView)edgeView.input.node);
            }
            
        }

        void AddNode(AbstractBehaveNode node)
        {
            Node nodeView;
            
            if (node is NPBehaveStackNode stackNode)
            {
                var stackNodeView = new NPBehaveStackNodeView(stackNode, m_EditorWindow, m_EdgeConnectorListener) { userData = node };
                m_GraphView.AddStackNodeView(stackNodeView);
                nodeView = stackNodeView;
            }
            else if (node is NPBehaveBlockNode blockNode)
            {
                var blockNodeView = new NPBehaveNodeView { userData = blockNode };
                blockNodeView.Initialize(blockNode, m_EdgeConnectorListener);
                nodeView = blockNodeView;

                NPBehaveStackNodeView stackNodeView = m_GraphView.GetStackNodeView(blockNode.stackData);
                stackNodeView.InsertBlock(blockNodeView);
            }
            else
            {
                var behaveNodeView = new NPBehaveNodeView() { userData = node };
                m_GraphView.AddElement(behaveNodeView);
                behaveNodeView.Initialize(node, m_EdgeConnectorListener);
                nodeView = behaveNodeView;
            }
            nodeView.MarkDirtyRepaint();
        }
        
        void AddEdges(IEnumerable<IEdge> edges)
        {
            foreach (IEdge edge in edges)
            {
                AddEdge(edge, true, false);
            }
        }
        
        UnityEditor.Experimental.GraphView.Edge AddEdge(IEdge edge, bool useVisualNodeMap = false, bool updateNodePorts = true)
        {
            var sourceNode = edge.outputSlot.node;
            if (sourceNode == null)
            {
                return null;
            }
            var sourceSlot = sourceNode.FindOutputSlot<NPBehaveSlot>(edge.outputSlot.slotId);
            var targetNode = edge.inputSlot.node;
            if (targetNode == null)
            {
                return null;
            }
            var targetSlot = targetNode.FindInputSlot<NPBehaveSlot>(edge.inputSlot.slotId);

            var sourceNodeView = m_GraphView.nodes.ToList().OfType<IBehaveNodeView>().FirstOrDefault(x => x.node == sourceNode);

            
            if (sourceNodeView != null)
            {
                sourceNodeView.FindPort(sourceSlot.slotReference, out var sourceAnchor);

                var targetNodeView = m_GraphView.nodes.ToList().OfType<IBehaveNodeView>().First(x => x.node == targetNode);

                targetNodeView.FindPort(targetSlot.slotReference, out var targetAnchor);

                var edgeView = new UnityEditor.Experimental.GraphView.Edge
                {
                    userData = edge,
                    output = sourceAnchor,
                    input = targetAnchor
                };

                //edgeView.RegisterCallback<MouseDownEvent>(OnMouseDown);
                edgeView.output.Connect(edgeView);
                edgeView.input.Connect(edgeView);
                m_GraphView.AddElement(edgeView);

                if (updateNodePorts)
                {
                    sourceNodeView.gvNode.RefreshPorts();
                    targetNodeView.gvNode.RefreshPorts();
                    sourceNodeView.UpdatePortInputTypes();
                    targetNodeView.UpdatePortInputTypes();
                }

                return edgeView;
            }

            return null;
        }

        public void Dispose() 
        { 
            if (m_GraphView != null) 
            { 
                saveRequested = null;
                
                foreach (var node in m_GraphView.Children().OfType<IBehaveNodeView>()) 
                    node.Dispose(); 
                m_GraphView.nodeCreationRequest = null; 
                m_GraphView = null; 
            }
            
            if (m_SearchWindowProvider != null) 
            { 
                Object.DestroyImmediate(m_SearchWindowProvider); 
                m_SearchWindowProvider = null; 
            } 
        } 
    }
}

