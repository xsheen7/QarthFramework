using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;

public class TableRegister
{
    public static void RegisterTable()
    {
        //预加载表格
        TableConfig.preLoadTableArray = new TDTableMetaData[]
        {
            TDLanguageTable.GetLanguageMetaData()
        };

        TableConfig.delayLoadTableArray = new TDTableMetaData[]
        {

        }; 
    }
}
