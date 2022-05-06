using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Qarth;
using TableML.Compiler;
using UnityEditor;
using UnityEngine;
using Log = KEngine.Log;

public class TableBuilderEditor
{
    [MenuItem("KEngine/Settings/Force Compile Settings + Code")]
    public static void CompileSettings()
    {
        DoCompileSettings(true);
    }

    [MenuItem("KEngine/Settings/Quick Compile Settings")]
    public static void QuickCompileSettings()
    {
        DoCompileSettings(false);
    }

    /// <summary>
    /// Custom the monitor trigger compile settings behaviour
    /// </summary>
    public static Action CustomCompileSettings;

    /// <summary>
    /// do compile settings
    /// </summary>
    /// <param name="force">Whether or not,check diff.  false will be faster!</param>
    /// <param name="genCode">Generate static code?</param>
    public static void DoCompileSettings(bool force = true, string forceTemplate = null, bool canCustom = true)
    {
        if (canCustom && CustomCompileSettings != null)
        {
            CustomCompileSettings();
            return;
        }

        List<TableCompileResult> results = null;
        
        Log.Info("Start Compile to c#+tsv");

        var template = force ? (forceTemplate ?? DefaultTemplate.GenCodeTemplateOneFile) : null;
        var genParam = new GenParam()
        {
            forceAll = force, genCSharpClass = true, genCodeFilePath = ProjectPathConfig.exportTableCSPath,
            genCodeTemplateString = template, changeExtension = ".tsv",
            settingCodeIgnorePattern = ProjectPathConfig.tableBuildIgnorePattern, nameSpace = "TableML"
        };
        var compilerParam = new CompilerParam()
            { CanExportTsv = true, ExportTsvPath = ProjectPathConfig.exportTableTSVPath, ExportLuaPath = null };
        results = new BatchCompiler().CompileAll(ProjectPathConfig.tableSourcePath, ProjectPathConfig.exportTableTSVPath, genParam,
            compilerParam);

        var sb = new StringBuilder();
        foreach (var r in results)
        {
            sb.AppendLine(string.Format("Excel {0} -> {1}", r.ExcelFile, r.TabFileRelativePath));
        }

        Log.Info("TableML all Compile ok!\n{0}", sb.ToString());
        // make unity compile
        AssetDatabase.Refresh();
    }
}