using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace PlayPlay 
{
    public class GameCenterMainForm : UGuiForm
    {
        private ProcedureGameCenter m_ProcedureGameCenter = null;


        public void OnStartButtonClick()
        {
            
        }


        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureGameCenter = (ProcedureGameCenter) userData;
            if (m_ProcedureGameCenter == null)
            {
                Log.Warning("ProcedureGameCenter is invalid when open GameCenterMainForm.");
                return;
            }

        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            m_ProcedureGameCenter = null;
            base.OnClose(isShutdown, userData);
        }

    }
}

