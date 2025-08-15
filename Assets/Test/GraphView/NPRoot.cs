using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPRoot : Node
{
    public string GUID;
    private NPBehaveGraphView graphView;

    public NPRoot(NPBehaveGraphView _graphView, string _title)
    {
        GUID = Guid.NewGuid().ToString();
        title = _title;
        graphView = _graphView;
        
        var childPort = GeneratePort(Direction.Output, Port.Capacity.Single);
        mainContainer.Add(childPort);
        
        RefreshExpandedState();
        RefreshPorts();
    }

    private Port GeneratePort(Direction direction, Port.Capacity capacity)
    {
        return InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(int));
    }
}
