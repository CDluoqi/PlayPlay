using System;
using System.Collections.Generic;
using UnityEditor.BehaveGraph.Serialization;
using UnityEngine;
using System.Linq;

namespace UnityEditor.BehaveGraph
{
    sealed class GraphData : JsonObject
    {
        [SerializeField]
        List<JsonData<AbstractBehaveNode>> m_Nodes = new List<JsonData<AbstractBehaveNode>>();
        
        public IEnumerable<T> GetNodes<T>()
        {
            return m_Nodes.SelectValue().OfType<T>();
        }
        
        [NonSerialized]
        List<AbstractBehaveNode> m_AddedNodes = new List<AbstractBehaveNode>();

        public IEnumerable<AbstractBehaveNode> addedNodes
        {
            get { return m_AddedNodes; }
        }

        [NonSerialized]
        List<AbstractBehaveNode> m_RemovedNodes = new List<AbstractBehaveNode>();

        public IEnumerable<AbstractBehaveNode> removedNodes
        {
            get { return m_RemovedNodes; }
        }
        
        public GraphObject owner { get; set; }
        
        public void ClearChanges()
        {
            m_AddedNodes.Clear();
            m_RemovedNodes.Clear();
        }

        public void AddNode(AbstractBehaveNode node)
        {
            m_Nodes.Add(node);
            m_AddedNodes.Add(node);
        }
        
        public void OnEnable()
        {

        }

        public void OnDisable()
        {
           
        }

        public void ValidateGraph()
        {
            
        }
    }
}


