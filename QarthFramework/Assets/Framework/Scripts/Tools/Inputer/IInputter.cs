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


namespace Qarth
{
    public interface IInputter
    {
        bool isEnabled
        {
            get;
            set;
        }
        void RegisterKeyCodeMonitor(KeyCode code, Run begin, Run end, Run press);
        void Release();
        void LateUpdate();
    }
}

