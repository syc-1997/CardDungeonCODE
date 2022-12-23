using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Plaerturn");
        UIManager.Instance.ShowTip("�ץ쥤��`�Υ��`��", Color.green, delegate() 
        {
            //�ظ�����
            FightManager.Instance.CurPowerCount = FightManager.Instance.MaxPowerCount;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();            
            Debug.Log("playerTime");
            //����û�п�ʱ�����ƶ�ϴ���ƶ�
            if (FightCardManger.Instance.cardList.Count < 4)
            {
                int val = FightCardManger.Instance.cardList.Count;
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);

                FightCardManger.Instance.ShuffleCard();
                //�������ƶ�����
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();
                //��ʣ�µ���
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4- val);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
            }
            else
            {
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//��4��
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
            }
            //����
            Debug.Log("����");
            
            
            

            //���¿�������
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        });
    }

    public override void OnUpdate()
    {
    }
}

