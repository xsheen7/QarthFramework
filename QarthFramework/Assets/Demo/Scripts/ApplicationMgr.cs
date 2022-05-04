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
        UIDataModule.RegisterUIPanel();
        yield return TableModule.LoadTable();
        //ShowLogoPanel();
        Log.i(TDArenaConfigTable.GetData(1).range);
        yield return null;
    }

    //开始游戏
    protected override void StartGame()
    {
        UIMgr.S.ClosePanelAsUIID(UIID.LogoPanel);
        Log.i("game start");
    }
}
