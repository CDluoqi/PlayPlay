using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.UIElements;

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
        
        public VisualElement InstantiateControl(AbstractBehaveNode node, PropertyInfo propertyInfo)
        {
            if (!ActionNameControlView.validTypes.Contains(propertyInfo.PropertyType))
                return null;
            return new ActionNameControlView(m_Label, node, propertyInfo);
        }
    }
    
    class ActionNameControlView : VisualElement
    {
        public static Type[] validTypes = { typeof(string) };
        
        AbstractBehaveNode m_Node;
        PropertyInfo m_PropertyInfo;
        
        public ActionNameControlView(string label, AbstractBehaveNode node, PropertyInfo propertyInfo)
        {
            //name = "controlAttribute";
            m_Node = node;
            m_PropertyInfo = propertyInfo;
            Add(new Label(label ?? ObjectNames.NicifyVariableName(propertyInfo.Name)));

            List<string> names = new List<string>();
            if (NPBehaveFunctionCache.KnownFunctionLookupTable.TryGetValue(typeof(MyRunner), out var attributeList))
            {
                foreach (var attribute in attributeList)
                {
                    names.Add(attribute.Name);
                }
            }
            var dropField = new DropdownField("name", names, 0);
            dropField.AddToClassList("some-styled-field");
            dropField.RegisterValueChangedCallback(OnValueChanged);
            
            Add(dropField);
        }

        void OnValueChanged(ChangeEvent<string> evt)
        {
            var value = (string)m_PropertyInfo.GetValue(m_Node, null);
            if (!evt.newValue.Equals(value))
            {
                m_PropertyInfo.SetValue(m_Node, evt.newValue, null);
            }
        }
    }
}


