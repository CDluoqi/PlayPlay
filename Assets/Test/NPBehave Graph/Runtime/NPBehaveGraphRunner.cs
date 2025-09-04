using System.Collections;
using System.Collections.Generic;
using NPBehave;
using UnityEngine;

namespace NPBehave
{
    public class NPBehaveGraphRunner : MonoBehaviour
    {
        [SerializeField] 
        private NPBehaveTreeAsset behaveTree;

        void Start()
        {
            Root behaviorTree = CreateBehaveTree();
            behaviorTree.Start();
            Debugger debugger = gameObject.AddComponent<Debugger>();
            debugger.BehaviorTree = behaviorTree;
        }

        Root CreateBehaveTree()
        {
            NodeConfig nodeConfig = JsonUtility.FromJson<NodeConfig>(behaveTree.Code);
            Root root = new Root(CreateNode(nodeConfig.nodes[0]));
            return root;
        }

        Node CreateNode(NodeConfig nodeConfig)
        {
            switch (nodeConfig.nodeType)
            {
                case NPBehaveNodeType.Selector:
                    return null;
                case NPBehaveNodeType.Sequence:
                    List<Node> childNodes = new List<Node>();
                    foreach (var child in nodeConfig.nodes)
                    {
                        childNodes.Add(CreateNode(child)); 
                    }
                    Sequence node = new Sequence(childNodes.ToArray());
                    return node;
                case NPBehaveNodeType.Parallel:
                    return null;
                case NPBehaveNodeType.RandomSelector:
                    return null;
                case NPBehaveNodeType.RandomSequence:
                    return null;
            
                case NPBehaveNodeType.Action:
                    //Action action = new Action(() => DoAction("aaaa"));
                    string name = "";
                    //Action action = new Action(() => DoAction(name));
                    Action action = new Action((bool aborted) => ActionMultiframeFunc(name, aborted));
                    return action;
                case NPBehaveNodeType.NavWalkTo:return null;
                case NPBehaveNodeType.Wait:return null;
                case NPBehaveNodeType.WaitUntilStopped:
                    WaitUntilStopped waitUntilStopped = new WaitUntilStopped();
                    return waitUntilStopped;
            
                case NPBehaveNodeType.BlackboardCondition:return null;
                case NPBehaveNodeType.BlackboardQuery:return null;
                case NPBehaveNodeType.Condition:return null;
                case NPBehaveNodeType.Cooldown:return null;
                case NPBehaveNodeType.Failer:return null;
                case NPBehaveNodeType.Inverter:return null;
                case NPBehaveNodeType.Observer:return null;
                case NPBehaveNodeType.Random:return null;
                case NPBehaveNodeType.Repeater:return null;
                case NPBehaveNodeType.Service:return null;
                case NPBehaveNodeType.Succeeder:return null;
                case NPBehaveNodeType.TimeMax:return null;
                case NPBehaveNodeType.TimeMin:return null;
                case NPBehaveNodeType.WaitForCondition:return null;
            }
            return null;
        }

        private bool DoAction(string actionName)
        {
            switch (actionName)
            {
                case "":

                    break;
            }
            return false;
        }
        
        private Action.Result ActionMultiframeFunc(string actionName, bool aborted)
        {
            switch (actionName)
            {
                case "":

                    break;
            }

            if (aborted)
            {
                return Action.Result.FAILED;
            }
            return Action.Result.PROGRESS;
        }
        
    }
}
