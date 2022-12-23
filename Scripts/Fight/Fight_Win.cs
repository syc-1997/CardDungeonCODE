using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//胜利
public class Fight_Win : FightUnit
{
    Camera mainCamera = Camera.main;
    private Vector3 CamerDestination = new Vector3(0f, 1.5f, 7.5f);
    public override void Init()
    {
        Debug.Log("成功");
        //显示成功界面
        UIManager.Instance.ShowTip("Clear", Color.green, delegate ()
        {
        });
        UIManager.Instance.CloseUI("FightUI");
        UIManager.Instance.ShowUI<GameUI>("GameUI");
    }


    public override void OnUpdate()
    {
        CamerMoveManger.Instance.MoveToDestination(CamerDestination);
    }
}
