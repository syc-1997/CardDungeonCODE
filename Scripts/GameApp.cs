using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏入口脚本
public class GameApp : MonoBehaviour
{
    void Start()
    {
        //初始化配置表
        GameConfigManger.Instance.Init();

        //初始化音频管理器
        AudioManager.Instance.Init();

        //初始化用户信息
        RoleManager.Instance.Init();

        //显示loginUI
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //播放BGM
        AudioManager.Instance.PlayBGM("bgm1");

        //test
        string name = GameConfigManger.Instance.GetCardByID("1002")["Des"];

        print(name);
    }
}
