using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Plaerturn");
        UIManager.Instance.ShowTip("プレイヤーのターン", Color.green, delegate() 
        {
            //回复能量
            FightManager.Instance.CurPowerCount = FightManager.Instance.MaxPowerCount;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();            
            Debug.Log("playerTime");
            //卡堆没有卡时，弃牌堆洗入牌堆
            if (FightCardManger.Instance.cardList.Count < 4)
            {
                int val = FightCardManger.Instance.cardList.Count;
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);

                FightCardManger.Instance.ShuffleCard();
                //更新弃牌堆数量
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();
                //抽剩下的牌
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4- val);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
            }
            else
            {
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(4);//抽4张
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
            }
            //抽牌
            Debug.Log("抽牌");
            
            
            

            //更新卡牌数量
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        });
    }

    public override void OnUpdate()
    {
    }
}

