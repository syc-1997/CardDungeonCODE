using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit 
{
    public override void Init()
    {
        //删除所有卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();
        //显示敌人回合提示
        Debug.Log("Plaerturn");
        UIManager.Instance.ShowTip("敵のターン", Color.red, delegate ()
        {
            Debug.Log("执行敌人ai");
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });
    }

    public override void OnUpdate()
    {
        
    }
}
