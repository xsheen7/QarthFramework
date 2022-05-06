using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameWish.Game;
using Qarth;
using UnityEngine;

//会在场景中生成对应的GameObject
[TMonoSingletonAttribute("[App]/ApplicationMgr")]
public class ApplicationMgr : AbstractApplicationMgr<ApplicationMgr>
{
    public void Init()
    {
        Log.i("ApplicationMgr init");
    }

    //初始化第三方插件
    protected override void ShowLogoPanel()
    {
        UIMgr.S.OpenPanel(UIID.LogoPanel);
    }

    protected override IEnumerator InitThirdLibConfig()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        yield return null;
    }

    //初始化运行环境
    protected override IEnumerator InitAppEnvironment()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.multiTouchEnabled = false;
        yield return null;
    }

    //初始化框架
    protected override IEnumerator InitFramework()
    {
        AppConfig.S.InitAppConfig();
        ResMgr.S.Init();
        UIRegister.RegisterUIPanel();
        TableRegister.RegisterTable();
        yield return TableModule.PreLoadTable(null);
        ShowLogoPanel();
    }

    //开始游戏
    protected override void StartGame()
    {
        UIMgr.S.ClosePanelAsUIID(UIID.LogoPanel);
        Log.i("game start");

        //延迟加载表格
        if (TDArenaConfigTable.count <= 0)
        {
            TDTableMetaData[] table = new[] { TDArenaConfigTable.metaData };
            StartCoroutine(TableModule.LoadTable(table, () =>
            {
                Log.i("arena table load finish:" +TDArenaConfigTable.GetData(1).id);
            }));
        }
        
        I18Mgr.S.SwitchLanguage(SystemLanguage.English);
        Log.i(TDLanguageTable.Get("Common_Build"));
    }
}
