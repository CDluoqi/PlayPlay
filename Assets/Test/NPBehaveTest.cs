using System.Collections;
using System.Collections.Generic;
using NPBehave;
using UnityEngine;

public class NPBehaveTest : MonoBehaviour
{
    Root behaveTree;
    
    
    void Start()
    {
        MyNode myNode = new MyNode();
        Sequence logAndWait = new Sequence(new Action(() => { Debug.LogError("You fucked!");}), new WaitUntilStopped());
        
        BlackboardCondition blackboardCondition = new BlackboardCondition("QQ", Operator.IS_EQUAL,true, Stops.SELF, logAndWait);

        Action node = new Action(() => { Debug.LogError("end"); });
        Sequence sequence = new Sequence(blackboardCondition, node);

        Service service = new Service(2, () => { Debug.LogError("QQ: server");}, new Sequence(node, new WaitUntilStopped()));
        
        behaveTree = new Root(service);
        behaveTree.Blackboard.Set("QQ", true);
        Debugger debugger = gameObject.AddComponent<Debugger>();
        debugger.BehaviorTree = behaveTree;
        behaveTree.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            behaveTree.Blackboard.Set("QQ", false);
        }
    }
}
