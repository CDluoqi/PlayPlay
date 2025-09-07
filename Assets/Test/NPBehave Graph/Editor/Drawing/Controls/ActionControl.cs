using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Assertions;
using UnityEditor.Graphing;

namespace UnityEditor.BehaveGraph.Drawing.Controls 
{
    [AttributeUsage(AttributeTargets.Property)]
    class ActionNameControlAttribute : Attribute, IControlAttribute
    {
        string m_Label;
        public ActionNameControlAttribute(string label = null)
        {
            m_Label = label;
        }

        public VisualElement InstantiateControl(AbstractBehaveNode node, PropertyInfo propertyInfo, ISearchView searchView)
        {
            if (!ActionNameControlView.validTypes.Contains(propertyInfo.PropertyType))
                return null;
            return new ActionNameControlView(m_Label, node, propertyInfo, searchView);
        }
    }

    class ActionNameControlView : VisualElement
    {
        public static Type[] validTypes = { typeof(string) };
        AbstractBehaveNode m_Node;
        PropertyInfo m_PropertyInfo;
        string m_Value;
        int m_UndoGroup = -1;
        public ActionNameControlView(string label, AbstractBehaveNode node, PropertyInfo propertyInfo, ISearchView searchView)
        {
            name = "controlAttribute";
            m_Node = node;
            m_PropertyInfo = propertyInfo;
            label = label ?? ObjectNames.NicifyVariableName(propertyInfo.Name);
            var thisLabel = new Label(label);
            Add(thisLabel);
            m_Value = GetValue();
            string value = null;
            var field = new TextField { value = m_Value };
            field.RegisterCallback<MouseDownEvent>(Repaint);
            field.RegisterCallback<MouseMoveEvent>(Repaint);
            field.RegisterValueChangedCallback(evt =>
            {
                value = GetValue();
                value = evt.newValue;
                m_PropertyInfo.SetValue(m_Node, value, null);
                m_UndoGroup = -1;
            });

            // Pressing escape while we are editing causes it to revert to the original value when we gained focus
            field.Q("unity-text-input").RegisterCallback<KeyDownEvent>(evt =>
            {
                if (evt.keyCode == KeyCode.Escape && m_UndoGroup > -1)
                {
                    Undo.RevertAllDownToGroup(m_UndoGroup);
                    m_UndoGroup = -1;
                    evt.StopPropagation();
                }
                this.MarkDirtyRepaint();
            });
            field.Q("unity-text-input").RegisterCallback<FocusOutEvent>(evt =>
            {
                this.MarkDirtyRepaint();
            });
            Add(field);
            
            Action action = () =>
            {
                searchView.FindFunction(Vector2.zero);
            };
            
            var csharpButton = new Button(() => { searchView.FindFunction(Vector2.zero);}) { text = "Set" };
            csharpButton.AddToClassList("some-styled-button");
            Add(csharpButton);
        }

        string GetValue()
        {
            var value = m_PropertyInfo.GetValue(m_Node, null);
            Assert.IsNotNull(value);
            return (string)value;
        }

        void Repaint<T>(MouseEventBase<T> evt) where T : MouseEventBase<T>, new()
        {
            evt.StopPropagation();
            this.MarkDirtyRepaint();
        }
    }
}


