using System.Collections;
using System.Collections.Generic;
using GameWish.Game;
using Qarth;
using UnityEngine;

public class TableModule
{
    public static IEnumerator LoadTable()
    {
        InitPreLoadTableMetaData();
        yield return ApplicationMgr.S.StartCoroutine(TableMgr.S.PreReadAll(HandleTableLoadFinish));
    }
    
    
    protected static void InitPreLoadTableMetaData()
    {
        TableConfig.preLoadTableArray = new TDTableMetaData[]
        {
            TDLanguageTable.GetLanguageMetaData(),
            TDArenaConfigTable.metaData,
        };
    }
    
    private static void HandleTableLoadFinish()
    {
        
    }
}
