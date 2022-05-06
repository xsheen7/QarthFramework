using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Qarth;
using UnityEngine;
using TableML;

namespace TableML
{
    public class TableModule : TableModuleBase
    {
        private static readonly bool IsEditor;

        static TableModule()
        {
            IsEditor = Application.isEditor;
        }

        /// <summary>
        /// internal constructor
        /// </summary>
        internal TableModule()
        {
        }


        /// <summary>
        /// Singleton
        /// </summary>
        private static TableModule _instance;

        /// <summary>
        /// Quick method to get TableFile from instance
        /// </summary>
        /// <param name="path"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public static TableFile Get(string path, bool useCache = true)
        {
            if (_instance == null)
                _instance = new TableModule();
            return _instance.GetTableFile(path, useCache);
        }

        /// <summary>
        /// Unity Resources.Load Table file in Resources folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected override string LoadTable(string path)
        {
            byte[] fileContent = FileMgr.S.ReadSync(GetTableFilePath(path));
            return Encoding.UTF8.GetString(fileContent);
        }

        /// <summary>
        /// 获取配置表的路径，都在Tables目录下
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetTableFilePath(string path)
        {
            return ProjectPathConfig.tableFolder + "/" + path;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Cache all the FileSystemWatcher, prevent the duplicated one
        /// </summary>
        private static Dictionary<string, FileSystemWatcher> _cacheWatchers;

        /// <summary>
        /// Watch the Table file, when changed, trigger the delegate
        /// </summary>
        /// <param name="path"></param>
        /// <param name="action"></param>
        public static void WatchTable(string path, System.Action<string> action)
        {
            if (!IsFileSystemMode)
            {
                Log.e("[WatchTable] Available in Unity Editor mode only!");
                return;
            }

            if (_cacheWatchers == null)
                _cacheWatchers = new Dictionary<string, FileSystemWatcher>();
            FileSystemWatcher watcher;
            var dirPath = Path.GetDirectoryName(ProjectPathConfig.tableSourcePath + path);
            dirPath = dirPath.Replace("\\", "/");

            if (!Directory.Exists(dirPath))
            {
                Log.e("[WatchTable] Not found Dir: {0}", dirPath);
                return;
            }

            if (!_cacheWatchers.TryGetValue(dirPath, out watcher))
            {
                _cacheWatchers[dirPath] = watcher = new FileSystemWatcher(dirPath);
                Log.i("Watching Table Dir: {0}", dirPath);
            }

            watcher.IncludeSubdirectories = false;
            watcher.Path = dirPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*";
            watcher.EnableRaisingEvents = true;
            watcher.InternalBufferSize = 2048;
            watcher.Changed += (sender, e) =>
            {
                Log.i("Table changed: {0}", e.FullPath);
                action.Invoke(path);
            };
        }
#endif

        /// <summary>
        /// whether or not using file system file, in unity editor mode only
        /// </summary>
        public static bool IsFileSystemMode
        {
            get
            {
                if (IsEditor)
                    return true;
                return false;
            }
        }
    }
}