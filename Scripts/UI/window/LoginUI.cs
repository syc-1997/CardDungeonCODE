using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//开始界面（要继承UIBase）
public class LoginUI : UIBsae
{
    private void Awake()
    {
        //开始游戏
        Register("bg/startBtn").onClick = onStartGameBtn;
        //退出游戏
        Register("bg/quitBtn").onClick = onQuitGameBtn;
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pDate)
    {
        //关闭login界面
        Close();

        //战斗初始化
        FightManager.Instance.ChangeType(FightType.Init);
    }

    private void onQuitGameBtn(GameObject obj, PointerEventData pDate)
    {
        Application.Quit();
    }
}
