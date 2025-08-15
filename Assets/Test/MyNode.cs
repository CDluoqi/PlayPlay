using System.Collections;
using System.Collections.Generic;
using NPBehave;
using UnityEngine;

public class MyNode : Node
{
    public MyNode() : base("MyNode")
    {
        
    }
    
    protected override void DoStop()
    {
        Debug.LogError("DoStop ");
    }
}
