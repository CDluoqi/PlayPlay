using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor.NPBehaveGraph
{
    class SearchWindowProvider : ScriptableObject
    {
        internal EditorWindow m_EditorWindow;
        internal GraphData m_Graph;
        internal GraphView m_GraphView;
        
        public VisualElement target { get; internal set; }
        
        public void Initialize(EditorWindow editorWindow, GraphData graph, GraphView graphView)
        {
            m_EditorWindow = editorWindow;
            m_Graph = graph;
            m_GraphView = graphView;
        }
    }

    class NPBehaveSearchProvider : SearchWindowProvider
    {
        public Searcher.Searcher LoadSearchWindow()
        {
            var root = new List<SearcherItem>();
            
            var composite = new List<SearcherItem>();
            composite.Add(new SearcherItem("Root"));
            composite.Add(new SearcherItem("Sequence"));
            
            SearcherItem item = new SearcherItem("Composite Nodes", "fuck zhao li ping ", composite);
            root.Add(item);
            var nodeDatabase = SearcherDatabase.Create(root, string.Empty, false);

            return new Searcher.Searcher(nodeDatabase, new NPBehaveSearchWindowAdapter("Create Node"));
        }

        public bool OnSearcherSelectEntry(SearcherItem entry, Vector2 screenMousePosition)
        {
            m_Graph.AddNode(new AbstractBehaveNode());
            return true;
        }
    }
}

