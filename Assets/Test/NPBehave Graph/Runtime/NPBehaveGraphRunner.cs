using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NPBehave;
using UnityEngine;

namespace NPBehave
{
    public class NPBehaveGraphRunner : MonoBehaviour
    {
        [SerializeField] 
        private NPBehaveTreeAsset behaveTree;
        
        Dictionary<string, List<object>> actionMap;

        void Start()
        {
            InitActionMap();
            
            Root behaviorTree = CreateBehaveTree();
            behaviorTree.Start();
            Debugger debugger = gameObject.AddComponent<Debugger>();
            debugger.BehaviorTree = behaviorTree;
        }

        protected virtual void InitActionMap()
        {
            actionMap = new Dictionary<string, List<object>>();
            
            MethodInfo[] methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (MethodInfo method in methods)
            {
                FunctionNameAttribute attribute = method.GetCustomAttribute<FunctionNameAttribute>();
                if (attribute != null)
                {
                    Type funcType = method.ReturnType != typeof(void) ? NPBehaveFunctionType.GetFuncType(method) : NPBehaveFunctionType.GetActionType(method);
                    Delegate functionDelegate = method.CreateDelegate(funcType, this);
                    if (!actionMap.TryGetValue(attribute.Name, out var methodList))
                    {
                        methodList = new List<object>();
                        actionMap.Add(attribute.Name, methodList);
                    }
                    methodList.Add(functionDelegate);
                }
            }

        }

        Root CreateBehaveTree()
        {
            NodeConfig nodeConfig = JsonUtility.FromJson<NodeConfig>(behaveTree.Code);
            Root root = new Root(CreateNode(nodeConfig.nodes[0]));
            return root;
        }

        Node CreateNode(NodeConfig nodeConfig)
        {
            Debug.LogError("CreateNode " + nodeConfig.nodeType);
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
                    return CreateActionNode(nodeConfig);
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
            Debug.LogError("Unknown node type " +  nodeConfig.nodeType);
            return null;
        }

        List<object> GetFunctionListByName(string functionName)
        {
            return actionMap.GetValueOrDefault(functionName);
        }

        T GetFunction<T>(List<object> functionList)
        {
            if (functionList != null)
            {
                foreach (var function in functionList)
                {
                    if (function is T typeFunc)
                    {
                        return typeFunc;
                    }
                }
            }
            return default(T);
        }

        Action CreateActionNode(NodeConfig nodeConfig)
        {
            if (string.IsNullOrEmpty(nodeConfig.param))
            {
                return null;
            }
            NPActionParam param = JsonUtility.FromJson<NPActionParam>(nodeConfig.param);
            if (param != null)
            {
                List<object> functionList = GetFunctionListByName(param.functionName);
                System.Action actionFunc = GetFunction<System.Action>(functionList);
                if (actionFunc != null)
                {
                    Action action = new Action(actionFunc);
                    return action;
                }
                Func<bool> singleFrameFunc = GetFunction<Func<bool>>(functionList);
                if (singleFrameFunc != null)
                {
                    Action action = new Action(singleFrameFunc);
                    return action;
                }
                Func<bool, Action.Result> multiframeFunc = GetFunction<Func<bool, Action.Result>>(functionList);
                if (multiframeFunc != null)
                {
                    Action action = new Action(multiframeFunc);
                    return action;
                }
                Func<Action.Request, Action.Result> multiframeFunc2 = GetFunction<Func<Action.Request, Action.Result>>(functionList);
                if (multiframeFunc2 != null)
                {
                    Action action = new Action(multiframeFunc2);
                    return action;
                }
            }
            return new Action(()=>{Debug.LogError("Action func not found! param:" + nodeConfig.param);});
        }
        

    }
}
