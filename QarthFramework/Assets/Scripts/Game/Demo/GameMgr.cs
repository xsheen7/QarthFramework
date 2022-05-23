using System;
using System.Collections;
using System.Collections.Generic;
using Qarth;
using UnityEngine;


namespace GameWish.Game
{
	[TMonoSingletonAttribute("[App]/GameMgr")]
	public class GameMgr : TMonoSingleton<GameMgr>
	{
		public SaveDataTable saveDataTable;
		
		public void StartGame()
		{
			Log.i("game start");
			//加载存档数据
			saveDataTable = new SaveDataTable();
			Log.i("user name:"+saveDataTable.gameDataHandler.data.userName);
			Log.i("user name:"+saveDataTable.otherDataHandler.data.age);
			
			//运行时更换语言
			Log.i(TDLanguageTable.Get("Common_Build"));
			I18Mgr.S.SwitchLanguage(SystemLanguage.English);
			EnumEventSystem.S.Register(EngineEventID.OnLanguageChange, (id, args) =>
			{
				Log.i(TDLanguageTable.Get("Common_Build"));
			});
			
			//加载资源
			ResLoader loader = ResLoader.Allocate("res_test");
			GameObject cubePrefab = loader.LoadSync("Cube") as GameObject;
			Instantiate(cubePrefab);

			this.CallWithDelay(() =>
			{
				loader.ReleaseRes("Cube");
			}, 3);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				saveDataTable.gameDataHandler.data.SetUserName("haha");
			}

			if (Input.GetKeyDown(KeyCode.B))
			{
				saveDataTable.otherDataHandler.data.SetAge(100);
				//手动保存
				saveDataTable.otherDataHandler.Save(true);
			}
		}
	}
	
}