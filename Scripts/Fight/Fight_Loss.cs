using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ʧ��
public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        Debug.Log("ʧ��");

        FightManager.Instance.StopAllCoroutines();
        
        //��ʾʧ�ܽ���

    }

    public override void OnUpdate()
    {
    }
}
