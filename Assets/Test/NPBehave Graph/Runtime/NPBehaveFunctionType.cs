using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPBehave
{
    public static class NPBehaveFunctionType
    {
        public delegate void ActionNormal();
        public delegate bool ActionSingleFrameFunc();
        public delegate Action.Result ActionMultiframeFunc(bool  cancel);
        public delegate Action.Result ActionMultiframeFunc2(Action.Request  request);
    }
}

