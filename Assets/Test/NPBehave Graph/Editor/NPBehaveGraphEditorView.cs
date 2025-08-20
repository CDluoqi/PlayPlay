using System;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UnityEditor.NPBehaveGraph
{
    class NPBehaveGraphEditorView : VisualElement, IDisposable 
    {
        EditorWindow m_EditorWindow;
        NPBehaveGraphView m_GraphView;
        GraphData m_Graph;
        
        SearchWindowProvider m_SearchWindowProvider;
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
                    Debug.LogError("Save");
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
                //m_GraphView.StretchToParentSize();
                content.Add(m_GraphView); 
                
                RegisterCallback<GeometryChangedEvent>(ApplySerializedWindowLayouts);
            }
            
            m_SearchWindowProvider = ScriptableObject.CreateInstance<NPBehaveSearchProvider>(); 
            m_SearchWindowProvider.Initialize(editorWindow, m_Graph, m_GraphView); 
            m_GraphView.nodeCreationRequest = NodeCreationRequest;
            
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

        public void HandleGraphChanges(bool wasUndoRedoPerformed)
        {
            foreach (var node in m_Graph.addedNodes)
            {
                AddNode(node);
            }
        }

        void AddNode(AbstractBehaveNode node)
        {
            Node nodeView;
            
            if (node is NPBehaveStackNode stackNode)
            {
                var stackNodeView = new NPBehaveStackNodeView(node, m_EditorWindow) { userData = node };
                m_GraphView.AddStackNodeView(stackNodeView);
                nodeView = stackNodeView;
            }
            else if (node is NPBehaveBlockNode blockNode)
            {
                var blockNodeView = new NPBehaveNodeView { userData = blockNode };
                blockNodeView.Initialize(blockNode);
                nodeView = blockNodeView;

                NPBehaveStackNodeView stackNodeView = m_GraphView.GetStackNodeView(blockNode.stackData);
                stackNodeView.InsertBlock(blockNodeView);
            }
            else
            {
                var behaveNodeView = new NPBehaveNodeView() { userData = node };
                m_GraphView.AddElement(behaveNodeView);
                behaveNodeView.Initialize(node);
                nodeView = behaveNodeView;
            }
            nodeView.MarkDirtyRepaint();
        }

        public void Dispose() 
        { 
            if (m_GraphView != null) 
            { 
                foreach (var node in m_GraphView.Children().OfType<INPBehaveNodeView>()) 
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

