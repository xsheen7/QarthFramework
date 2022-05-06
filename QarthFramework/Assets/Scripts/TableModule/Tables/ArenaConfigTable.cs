
// 此代码由工具生成，请勿修改，如需扩展请编写同名Class并加上partial关键字

using System.Collections;
using System.Collections.Generic;
using KEngine;
namespace TableML
{

	/// <summary>
	/// Auto Generate for Tab File: "ArenaConfig.tsv"
    /// Excel File: ArenaConfig.xlsx
    /// No use of generic and reflection, for better performance,  less IL code generating
	/// </summary>>
    public partial class ArenaConfigTables : IReloadableTables
    {
        /// <summary>
        /// How many reload function load?
        /// </summary>>
        public static int ReloadCount { get; private set; }

		public static readonly string[] TabFilePaths = 
        {
            "ArenaConfig.tsv"
        };
        public static ArenaConfigTables _instance = new ArenaConfigTables();
        Dictionary<int, ArenaConfigTable> _dict = new Dictionary<int, ArenaConfigTable>();

        /// <summary>
        /// Trigger delegate when reload the Tables
        /// </summary>>
	    public static System.Action OnReload;

        /// <summary>
        /// Constructor, just reload(init)
        /// When Unity Editor mode, will watch the file modification and auto reload
        /// </summary>
	    private ArenaConfigTables()
	    {
        }

        /// <summary>
        /// Get the singleton
        /// </summary>
        /// <returns></returns>
	    public static ArenaConfigTables GetInstance()
	    {
            if (ReloadCount == 0)
            {
                _instance._ReloadAll(true);
    #if UNITY_EDITOR
                if (TableModule.IsFileSystemMode)
                {
                    for (var j = 0; j < TabFilePaths.Length; j++)
                    {
                        var tabFilePath = TabFilePaths[j];
                        TableModule.WatchTable(tabFilePath, (path) =>
                        {
                            if (path.Replace("\\", "/").EndsWith(path))
                            {
                                _instance.ReloadAll();
                                Log.LogConsole_MultiThread("File Watcher! Reload success! -> " + path);
                            }
                        });
                    }

                }
    #endif
            }

	        return _instance;
	    }
        
        public int Count
        {
            get
            {
                return _dict.Count;
            }
        }

        /// <summary>
        /// Do reload the Table file: ArenaConfig, no exception when duplicate primary key
        /// </summary>
        public void ReloadAll()
        {
            _ReloadAll(false);
        }

        /// <summary>
        /// Do reload the Table class : ArenaConfig, no exception when duplicate primary key, use custom string content
        /// </summary>
        public void ReloadAllWithString(string context)
        {
            _ReloadAll(false, context);
        }

        /// <summary>
        /// Do reload the Table file: ArenaConfig
        /// </summary>
	    void _ReloadAll(bool throwWhenDuplicatePrimaryKey, string customContent = null)
        {
            for (var j = 0; j < TabFilePaths.Length; j++)
            {
                var tabFilePath = TabFilePaths[j];
                TableFile tableFile;
                if (customContent == null)
                    tableFile = TableModule.Get(tabFilePath, false);
                else
                    tableFile = TableFile.LoadFromString(customContent);

                using (tableFile)
                {
                    foreach (var row in tableFile)
                    {
                        var pk = ArenaConfigTable.ParsePrimaryKey(row);
                        ArenaConfigTable Table;
                        if (!_dict.TryGetValue(pk, out Table))
                        {
                            Table = new ArenaConfigTable(row);
                            _dict[Table.ID] = Table;
                        }
                        else 
                        {
                            if (throwWhenDuplicatePrimaryKey) throw new System.Exception(string.Format("DuplicateKey, Class: {0}, File: {1}, Key: {2}", this.GetType().Name, tabFilePath, pk));
                            else Table.Reload(row);
                        }
                    }
                }
            }

	        if (OnReload != null)
	        {
	            OnReload();
	        }

            ReloadCount++;
            Log.Info("Reload Tables: {0}, Row Count: {1}, Reload Count: {2}", GetType(), Count, ReloadCount);
        }

	    /// <summary>
        /// foreachable enumerable: ArenaConfig
        /// </summary>
        public static IEnumerable GetAll()
        {
            foreach (var row in GetInstance()._dict.Values)
            {
                yield return row;
            }
        }

        /// <summary>
        /// GetEnumerator for `MoveNext`: ArenaConfig
        /// </summary> 
	    public static IEnumerator GetEnumerator()
	    {
	        return GetInstance()._dict.Values.GetEnumerator();
	    }
         
	    /// <summary>
        /// Get class by primary key: ArenaConfig
        /// </summary>
        public static ArenaConfigTable Get(int primaryKey)
        {
            ArenaConfigTable Table;
            if (GetInstance()._dict.TryGetValue(primaryKey, out Table)) return Table;
            return null;
        }

        // ========= CustomExtraString begin ===========
        
        // ========= CustomExtraString end ===========
    }

	/// <summary>
	/// Auto Generate for Tab File: "ArenaConfig.tsv" 
    /// Excel File: ArenaConfig.xlsx
    /// Singleton class for less memory use
	/// </summary>
	public partial class ArenaConfigTable : TableRowFieldParser
	{
		
        /// <summary>
        /// Id
        /// </summary>
        public int ID { get; private set;}
        
        /// <summary>
        /// Range
        /// </summary>
        public string 排名范围 { get; private set;}
        
        /// <summary>
        /// Reward
        /// </summary>
        public int 擂台币结算奖励 { get; private set;}
        

        internal ArenaConfigTable(TableFileRow row)
        {
            Reload(row);
        }

        internal void Reload(TableFileRow row)
        { 
            ID = row.Get_int(row.Values[0], ""); 
            排名范围 = row.Get_string(row.Values[1], ""); 
            擂台币结算奖励 = row.Get_int(row.Values[2], ""); 
        }

        /// <summary>
        /// Get PrimaryKey from a table row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static int ParsePrimaryKey(TableFileRow row)
        {
            var primaryKey = row.Get_int(row.Values[0], "");
            return primaryKey;
        }
	}
 
}
