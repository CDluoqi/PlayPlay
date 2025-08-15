
using UnityEngine;
using UnityGameFramework.Runtime;

namespace PlayPlay
{
    public class UGuiForm : UIFormLogic
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = Vector2.zero;
        }
    }
}

