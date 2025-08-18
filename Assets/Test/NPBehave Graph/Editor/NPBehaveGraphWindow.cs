using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor.NPBehaveGraph
{
    public class NPBehaveGraphEditorWindow : EditorWindow
    {
        [NonSerialized]
        bool m_FrameAllAfterLayout;
        [SerializeField]
        string m_Selected = "AAAA";
        [NonSerialized]
        bool m_HasError;
        [SerializeField]
        GraphObject m_GraphObject;
        
        internal GraphObject graphObject
        {
            get { return m_GraphObject; }
            set
            {
                if (m_GraphObject != null)
                    DestroyImmediate(m_GraphObject);
                m_GraphObject = value;
            }
        }
        
        public string selectedGuid
        {
            get { return m_Selected; }
            private set
            {
                m_Selected = value;
            }
        }
        
        NPBehaveGraphEditorView m_GraphEditorView;
        internal NPBehaveGraphEditorView graphEditorView
        {
            get { return m_GraphEditorView; }
            
            private set
            {
                if (m_GraphEditorView != null)
                {
                    m_GraphEditorView.RemoveFromHierarchy();
                    m_GraphEditorView.Dispose();
                }
    
                m_GraphEditorView = value;
    
                if (m_GraphEditorView != null)
                {
                    m_FrameAllAfterLayout = true;
                    this.rootVisualElement.Add(m_GraphEditorView);
                }
            }
        }
        
        [MenuItem("Window/NPBehaveGraphWindow")]
        public static void OpenWindow()
        {
            var window = GetWindow<NPBehaveGraphEditorWindow>();
            window.titleContent = new GUIContent("NPBehaveGraph");
        }
    
        private void Update()
        {
            if (m_HasError)
                return;
            
            if (graphObject == null && selectedGuid != null)
            {
                var guid = selectedGuid;
                selectedGuid = null;
                Initialize(guid);
            }
            
            var materialGraph = graphObject.graph as GraphData;
            if (materialGraph == null)
                return;
            
            if (graphEditorView == null)
            {
                graphEditorView = new NPBehaveGraphEditorView(this, materialGraph);
            }
            graphEditorView.HandleGraphChanges(false);
            graphObject.graph.ClearChanges();
        }
        
        void OnGeometryChanged(GeometryChangedEvent evt)
        {
            if (graphEditorView == null)
                return;

            // this callback is only so we can run post-layout behaviors after the graph loads for the first time
            // we immediately unregister it so it doesn't get called again
            graphEditorView.UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            if (m_FrameAllAfterLayout)
                graphEditorView.graphView.FrameAll();
            m_FrameAllAfterLayout = false;
        }

        public void Initialize(string assetGuid)
        {
            try
            {
                graphObject = CreateInstance<GraphObject>();
                graphObject.hideFlags = HideFlags.HideAndDontSave;
                graphObject.graph = new GraphData();
                Repaint();
            }
            catch (Exception e)
            {
                m_HasError = true;
                m_GraphEditorView = null;
                graphObject = null;
                throw;
            }
        }

        void OnEnable()
        {
            this.SetAntiAliasing(4);
        }
    
        void OnDisable()
        {
            graphEditorView = null;
        }

        void OnDestroy()
        {
            graphObject = null;
            graphEditorView = null;
        }

    }
}

