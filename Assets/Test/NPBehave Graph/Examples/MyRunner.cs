using System.Collections;
using System.Collections.Generic;
using NPBehave;
using UnityEngine;

public class MyRunner : NPBehaveGraphRunner
{
    [FunctionName("LogTest0", FuncPurpose.Condition)]
    private bool LogTest0()
    {
        return true;
    }
    
    [FunctionName("LogTest1", FuncPurpose.Any, "This text is used to explain how to use this function")]
    private Action.Result LogTest1(bool aborted)
    {
        if (aborted)
        {
            Debug.LogError("FAILED");
            return Action.Result.FAILED;
        }
        Debug.LogError("PROGRESS");
        return Action.Result.PROGRESS;
    }

    private int runCount = 0;
    [FunctionName("LogTest2")]
    private void LogTest2()
    {
        Debug.LogError("Using Action");
    }

    [FunctionName("LogTest3")]
    private void LogTest3()
    {
        return;
    }
}
