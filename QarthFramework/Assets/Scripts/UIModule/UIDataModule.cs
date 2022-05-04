using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qarth;

public class UIDataModule
{
    public static void RegisterUIPanel()
    {
        UIDataTable.AddPanelData(UIID.LogoPanel, null, "LogoPanel/LogoPanel");
    }
}
