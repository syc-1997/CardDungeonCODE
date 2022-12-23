using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//抽卡脚本
public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if(TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);//抽卡数量
            int val1 = FightCardManger.Instance.cardList.Count;
            //是否有卡抽
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
                //抽出剩下的牌
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val1);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
                //洗牌
                FightCardManger.Instance.ShuffleCard();               
                //更新弃牌堆数量
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
