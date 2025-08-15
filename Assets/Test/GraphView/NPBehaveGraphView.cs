using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NPBehave;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NPBehaveGraphView : GraphView
{
    public NPBehaveGraphView()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();
    }

    public NPRoot CreateNPRoot(string nodeName)
    {
        var root = new NPRoot(this, nodeName);
        AddElement(root);
        return root;
    }

    public NPSequence CreateNPSequence(string nodeName)
    {
        var sequence = new NPSequence(this, nodeName);
        AddElement(sequence);
        return sequence;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }
}
