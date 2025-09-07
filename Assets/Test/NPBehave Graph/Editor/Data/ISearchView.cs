using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.BehaveGraph
{
    public interface ISearchView
    {
        void FindFunction(Vector2 screenMousePosition);
    }
}

