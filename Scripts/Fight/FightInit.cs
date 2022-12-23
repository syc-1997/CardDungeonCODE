using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ս����ʼ��
public class FightInit : FightUnit
{
    public override void Init()
    {
        //��ʼ��ս����ֵ
        FightManager.Instance.Init();

        //�л�BGM
        AudioManager.Instance.PlayBGM("battle");

        // ��������
        EnemyManager.Instance.LoadRes("10003");//��ȡ�ؿ�3�ĵ�����Ϣ

        //��ʼ��ս������
        FightCardManger.Instance.Init();

        //��ʾս������
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }
}
