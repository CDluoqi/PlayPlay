using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPBehave
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FunctionNameAttribute : Attribute
    {
        public string Name { get; }

        public FunctionNameAttribute(string name)
        {
            Name = name;
        }
    }
    
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ActionNameAttribute : FunctionNameAttribute
    {
        public ActionNameAttribute(string name) : base(name)
        {
        }
    }
}


