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
                    if (method.ReturnType != typeof(void) && method.GetParameters().All(p => true))
                    {
                        Delegate functionDelegate = method.CreateDelegate(GetFuncType(method), this);
                        if (!actionMap.TryGetValue(attribute.Name, out var methodList))
                        {
                            methodList = new List<object>();
                            actionMap.Add(attribute.Name, methodList);
                        }
                        methodList.Add(functionDelegate);
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Method {method.Name} has FunctionNameAttribute but is not a valid Func<> type.");
                    }
                }
            }

        }
        
        private Type GetFuncType(MethodInfo method)
        {
            Type[] parameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
            Type returnType = method.ReturnType;

            Type funcType; 
            if (parameterTypes.Length == 0)
            {
                funcType = typeof(Func<>).MakeGenericType(returnType);
            }
            else
            {
                Type[] genericArguments = parameterTypes.Concat(new[] { returnType }).ToArray();
                Type genericFuncType = null;

                switch (parameterTypes.Length)
                {
                    case 1:
                        genericFuncType = typeof(Func<,>);
                        break;
                    case 2:
                        genericFuncType = typeof(Func<,,>);
                        break;
                    case 3:
                        genericFuncType = typeof(Func<,,,>);
                        break;
                    case 4:
                        genericFuncType = typeof(Func<,,,,>);
                        break;
                    case 5:
                        genericFuncType = typeof(Func<,,,,,>);
                        break;
                    case 6:
                        genericFuncType = typeof(Func<,,,,,,>);
                        break;
                    case 7:
                        genericFuncType = typeof(Func<,,,,,,,>);
                        break;
                    case 8:
                        genericFuncType = typeof(Func<,,,,,,,,>);
                        break;
                    case 9:
                        genericFuncType = typeof(Func<,,,,,,,,,>);
                        break;
                    case 10:
                        genericFuncType = typeof(Func<,,,,,,,,,,>);
                        break;
                    case 11:
                        genericFuncType = typeof(Func<,,,,,,,,,,,>);
                        break;
                    case 12:
                        genericFuncType = typeof(Func<,,,,,,,,,,,,>);
                        break;
                    case 13:
                    genericFuncType = typeof(Func<,,,,,,,,,,,,,>);
                    break;
                    case 14:
                        genericFuncType = typeof(Func<,,,,,,,,,,,,,,>);
                        break;
                    case 15:
                        genericFuncType = typeof(Func<,,,,,,,,,,,,,,,>);
                        break;
                    case 16:
                        genericFuncType = typeof(Func<,,,,,,,,,,,,,,,,>);
                        break;
                    default:
                        throw new NotSupportedException($"Func with {parameterTypes.Length} parameters is not supported.");
                }
                funcType = genericFuncType.MakeGenericType(genericArguments);
            }
            return funcType;
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
                    Func<bool, Action.Result> func = GetFunction<Func<bool, Action.Result>>("LogTest");
                    Action action = new Action(func);
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
            Debug.LogError("Unknown node type " +  nodeConfig.nodeType);
            return null;
        }

        public T GetFunction<T>(string functionName)
        {
            Debug.LogError("GetFunction " + typeof(T) + "  " +  functionName + "  " + actionMap.Count);
            if (actionMap.TryGetValue(functionName, out List<object> functionList))
            {
                foreach (object function in functionList)
                {
                    if (function is T typeFunc)
                    {
                        return typeFunc;
                    }
                }
            }
            return default(T);
        }
        
    }
}
