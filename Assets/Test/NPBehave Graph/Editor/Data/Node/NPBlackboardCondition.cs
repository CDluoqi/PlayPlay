using NPBehave;
using UnityEngine;
using UnityEditor.BehaveGraph.Drawing.Controls;
using Object = UnityEngine.Object;
namespace UnityEditor.BehaveGraph
{
    [Title("Decorator", "BlackboardCondition")]
    class NPBlackboardCondition : NPDecorator
    {
        public NPBlackboardCondition()
        {
            name = "BlackboardCondition";
            synonyms = new string[] { "decorator", "blackboardCondition" };
        }
        
        [SerializeField]
        private Operator m_Operator = Operator.IS_EQUAL;
        
        [EnumControl("Operator")]
        public Operator Operator
        {
            get { return m_Operator; }
            set
            {
                if (m_Operator == value)
                    return;

                m_Operator = value;
                Dirty(ModificationScope.Graph);
            }
        }

        [SerializeField]
        private string m_Value;
        [TextControl("Value")]
        public string Value 
        {
            get { return m_Value; }
            set 
            {
                if (m_Value == value)
                    return;

                m_Value = value;
                Dirty(ModificationScope.Graph);
            }
        }

        [SerializeField]
        private Stops m_StopsOnChange = Stops.NONE;
        [EnumControl("Stops")]
        public Stops StopsOnChange 
        {
            get { return m_StopsOnChange; }
            set 
            {
                if(m_StopsOnChange == value)
                    return;
                m_StopsOnChange = value;
                Dirty(ModificationScope.Graph);
            }
        }

    }
}
