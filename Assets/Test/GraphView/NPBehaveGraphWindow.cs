using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class NPBehaveGraphWindow : EditorWindow
{
    private NPBehaveGraphView graphView;
    
    [MenuItem("Window/NPBehaveGraphWindow")]
    public static void OpenWindow()
    {
        var window = GetWindow<NPBehaveGraphWindow>();
        window.titleContent = new GUIContent("NPBehaveGraph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
    }


    void ConstructGraphView()
    {
        graphView = new NPBehaveGraphView
        {
            name = "NPBehave Graph"
        };
        
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    void GenerateToolbar()
    {
        var toolbar = new Toolbar();
        var createRootBtn = new Button(() => graphView.CreateNPRoot("Root"))
        {
            text = "Root"
        };

        var createSequence = new Button(() => graphView.CreateNPSequence("Sequence"))
        {
            text = "Sequence"
        };
        
        toolbar.Add(createRootBtn);
        toolbar.Add(createSequence);
        rootVisualElement.Add(toolbar);
    }

}
