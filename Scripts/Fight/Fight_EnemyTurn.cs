using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit 
{
    public override void Init()
    {
        //ɾ�����п���
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();
        //��ʾ���˻غ���ʾ
        Debug.Log("Plaerturn");
        UIManager.Instance.ShowTip("���Υ��`��", Color.red, delegate ()
        {
            Debug.Log("ִ�е���ai");
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });
    }

    public override void OnUpdate()
    {
        
    }
}
