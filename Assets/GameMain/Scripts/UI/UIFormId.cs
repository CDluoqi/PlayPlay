using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayPlay
{
        /// <summary>
        /// 界面编号。
        /// </summary>
        public enum UIFormId : byte
        {
            Undefined = 0,
    
            /// <summary>
            /// 弹出框。
            /// </summary>
            DialogForm = 1,
    
            /// <summary>
            /// 主菜单。
            /// </summary>
            GameCenterMainForm = 100,
    
            /// <summary>
            /// 设置。
            /// </summary>
            SettingForm = 101,
    
            /// <summary>
            /// 关于。
            /// </summary>
            AboutForm = 102,
        }
}
