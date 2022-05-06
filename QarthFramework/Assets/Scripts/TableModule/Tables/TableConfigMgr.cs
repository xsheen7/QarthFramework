
// 此代码由工具生成，请勿修改，如需扩展请编写同名Class并加上partial关键字

using System.Collections;
using System.Collections.Generic;

namespace TableML
{
	/// <summary>
    /// All Tables list here, so you can reload all Tables manully from the list.
	/// </summary>
    public partial class TableConfigMgr
    {
        private static IReloadableTables[] _tablesList;
        public static IReloadableTables[] TablesList
        {
            get
            {
                if (_tablesList == null)
                {
                    _tablesList = new IReloadableTables[]
                    {
                     	ArenaConfigTables._instance ,

                    };
                }
                return _tablesList;
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("KEngine/Tables/Try Reload All Tables Code")]
#endif
	    public static void AllTablesReload()
	    {
	        for (var i = 0; i < TablesList.Length; i++)
	        {
	            var Tables = TablesList[i];
                if (Tables.Count > 0 // if never reload, ignore
#if UNITY_EDITOR
                    || !UnityEditor.EditorApplication.isPlaying // in editor and not playing, force load!
#endif
                    )
                {
                    Tables.ReloadAll();
                }

	        }
	    }

    }
}
