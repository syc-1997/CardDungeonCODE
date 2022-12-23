using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManger
{
    public static FightCardManger Instance = new FightCardManger();

    public List<string> cardList;//���Ѽ���

    public List<string> usedCardList;//���ƶ�

    //��ʼ��
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();

        //������ʱ����
        List<string> tempList = new List<string>();
        //����ҵĿ��ƴ洢����ʱ����
        tempList.AddRange(RoleManager.Instance.cardList);

        while (tempList.Count > 0)
        {
            //����±�
            int tempIndex = Random.Range(0, tempList.Count);

            //��ӵ�����
            cardList.Add(tempList[tempIndex]);

            //��ʱ����ɾ��
            tempList.RemoveAt(tempIndex);
        }

        Debug.Log(cardList.Count);
    }

    public bool HasCard()
    {
        return cardList.Count >= 4;
    }

    //�鿨
    public string DrawCard()
    {
        string id = cardList[cardList.Count - 1];

        cardList.RemoveAt(cardList.Count - 1);

        return id;
    }
    //ϴ��
    public void ShuffleCard()
    {
        //������ʱ����
        List<string> tempList = new List<string>();
        //�����ƶѵ��Ƽ��뵽����
        tempList.AddRange(usedCardList);
        cardList = new List<string>();
        usedCardList = new List<string>();
        while (tempList.Count > 0)
        {
            //����±�
            int tempIndex = Random.Range(0, tempList.Count);

            //��ӵ�����
            cardList.Add(tempList[tempIndex]);

            //��ʱ����ɾ��
            tempList.RemoveAt(tempIndex);
        }
        //����Ѿ�ʹ���ƶ�
        //usedCardList.Clear();
        Debug.Log("ϴ��");

    }
    //��ʾ�ƶ�
    public string ShowCard(int ID)
    {
        string id = cardList[ID];

        return id;
    }
    //��ʾ���ƶ�
    public string ShowusedCard(int ID)
    {
        string id = usedCardList[ID];

        return id;
    }
}
