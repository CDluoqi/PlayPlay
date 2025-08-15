using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace PlayPlay
{
    public class UGuiGroupHelper : UIGroupHelperBase
    {
        private Canvas m_CachedCanvas;
        private int m_Depth = 0;

        public const int DepthFactor = 10000;
    
        public override void SetDepth(int depth)
        {
            m_Depth  = depth;
            m_CachedCanvas.overrideSorting = true;
            m_CachedCanvas.sortingOrder = DepthFactor * depth;
        }

        private void Awake()
        {
            m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
            gameObject.GetOrAddComponent<GraphicRaycaster>();
        }


        private void Start()
        {
            m_CachedCanvas.overrideSorting = true;
            m_CachedCanvas.sortingOrder = DepthFactor * m_Depth;
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}

