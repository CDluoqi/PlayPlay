using System.Collections;
using System.Collections.Generic;
using NPBehave;
using UnityEngine;

public class MyRunner : NPBehaveGraphRunner
{
    [FunctionName("LogTest")]
    private Action.Result LogTest(bool aborted)
    {
        if (aborted)
        {
            Debug.LogError("FAILED");
            return Action.Result.FAILED;
        }
        Debug.LogError("PROGRESS");
        return Action.Result.PROGRESS;
    }
    
    [FunctionName("LogTest2")]
    private void LogTest2()
    {
        return;
    }
    
    [FunctionName("LogTest3")]
    private void LogTest3()
    {
        return;
    }
}
