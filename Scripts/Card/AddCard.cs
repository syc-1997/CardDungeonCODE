using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//�鿨�ű�
public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if(TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);//�鿨����
            int val1 = FightCardManger.Instance.cardList.Count;
            //�Ƿ��п���
            if (FightCardManger.Instance.cardList.Count > val)
            {
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();

                //Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
                Vector3 pos = Camera.main.transform.position;
                pos.y = 0;
                PlayEffect(pos);
            }
            else
            {
                //���ʣ�µ���
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val1);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
                //ϴ��
                FightCardManger.Instance.ShuffleCard();               
                //�������ƶ�����
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val- val1);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();

                Vector3 pos = Camera.main.transform.position;
                pos.y = 0;
                PlayEffect(pos);
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }        
    }
}
