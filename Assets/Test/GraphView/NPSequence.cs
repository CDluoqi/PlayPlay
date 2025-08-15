using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPSequence : Node
{
    public string GUID;
    private NPBehaveGraphView graphView;

    public NPSequence(NPBehaveGraphView _graphView, string _title)
    {
        GUID = Guid.NewGuid().ToString();
        title = _title;
        graphView = _graphView;
        
        var parentPort = GeneratePort(Direction.Input, Port.Capacity.Single);
        var childPort = GeneratePort(Direction.Output, Port.Capacity.Multi);
        mainContainer.Add(parentPort);
        mainContainer.Add(childPort);
        RefreshExpandedState();
        RefreshPorts();
    }

    private Port GeneratePort(Direction direction, Port.Capacity capacity)
    {
        return InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(int));
    }
}
