//  Desc:        Framework For Game Develop with Unity3d
//  Copyright:   Copyright (C) 2017 SnowCold. All rights reserved.
//  WebSite:     https://github.com/SnowCold/Qarth
//  Blog:        http://blog.csdn.net/snowcoldgame
//  Author:      SnowCold
//  E-mail:      snowcold.ouyang@gmail.com
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

namespace Qarth
{
    public class SortingOrderObserver : MonoBehaviour
    {
        [SerializeField]
        protected Canvas m_TargetCanvas;
        [SerializeField]
        protected int m_OrderOffset;
        [SerializeField]
        protected Renderer m_Renderer;

        public virtual void OnSortingOrderUpdate()
        {
            if (m_Renderer == null || m_TargetCanvas == null)
            {
                return;
            }

            m_Renderer.sortingOrder = m_TargetCanvas.sortingOrder + m_OrderOffset;
        }
    }
}
