using NPBehave;
using UnityEngine;
using UnityEditor.BehaveGraph.Drawing.Controls;
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
    }
}
