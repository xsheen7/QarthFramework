using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame
{
    public class LogoPanel : AbstractPanel
    {
        [SerializeField] private Button m_CloseBtn;

        protected override void OnUIInit()
        {
            base.OnUIInit();
            m_CloseBtn.onClick.AddListener(CloseSelfPanel);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Debug.Log("logo panel show:" + Time.time);
        }

        protected override void OnClose()
        {
            base.OnClose();
        }
    }
}