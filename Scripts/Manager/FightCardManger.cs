using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManger
{
    public static FightCardManger Instance = new FightCardManger();

    public List<string> cardList;//卡堆集合

    public List<string> usedCardList;//弃牌堆

    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //定义临时集合
        List<string> tempList = new List<string>();
        //将玩家的卡牌存储到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);

        while (tempList.Count > 0)
        {
            //随机下标
            int tempIndex = Random.Range(0, tempList.Count);

            //添加到卡堆
            cardList.Add(tempList[tempIndex]);

            //临时集合删除
            tempList.RemoveAt(tempIndex);
        }

        Debug.Log(cardList.Count);
    }

    public bool HasCard()
    {
        return cardList.Count >= 4;
    }

    //抽卡
    public string DrawCard()
    {
        string id = cardList[cardList.Count - 1];

        cardList.RemoveAt(cardList.Count - 1);

        return id;
    }
    //洗牌
    public void ShuffleCard()
    {
        //定义临时集合
        List<string> tempList = new List<string>();
        //将弃牌堆的牌加入到集合
        tempList.AddRange(usedCardList);
        cardList = new List<string>();
        usedCardList = new List<string>();
        while (tempList.Count > 0)
        {
            //随机下标
            int tempIndex = Random.Range(0, tempList.Count);

            //添加到卡堆
            cardList.Add(tempList[tempIndex]);

            //临时集合删除
            tempList.RemoveAt(tempIndex);
        }
        //清除已经使用牌堆
        //usedCardList.Clear();
        Debug.Log("洗牌");

    }
    //显示牌堆
    public string ShowCard(int ID)
    {
        string id = cardList[ID];

        return id;
    }
    //显示弃牌堆
    public string ShowusedCard(int ID)
    {
        string id = usedCardList[ID];

        return id;
    }
}
