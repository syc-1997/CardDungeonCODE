using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


//战斗UI
public class FightUI : UIBsae
{

    private Text cardCountTxt;//卡牌数量
    private Text noCardCountTxt;//弃牌堆数量
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;
    private List<CardItem> cardItemList;//存储卡牌的集合

    private void Awake()
    {
        cardItemList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
        transform.Find("hasCard").GetComponent<Button>().onClick.AddListener(OpenhasCardList);
        transform.Find("noCard").GetComponent<Button>().onClick.AddListener(OpenUsedCardList);
    }

    //切换到敌人回合
    private void onChangeTurnBtn()
    {
        //只有玩家回合才能切换
        if (FightManager.Instance.fightUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);
        }
    }

    private void Start()
    {
        UpdateHP();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }
    public void UpdateHP()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
    }

    //更新能量

    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
    }

    //防御更新
    public void UpdateDefense()
    {
        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
    }

    //更新卡堆数量
    public void UpdateCardCount()

    {
        cardCountTxt.text = FightCardManger.Instance.cardList.Count.ToString();
    }


    //更新弃牌堆
    public void UpdateUsedCardCount()
    {
        noCardCountTxt.text = FightCardManger.Instance.usedCardList.Count.ToString();
    }

    //创建卡牌物体
    public void CreateCardItem(int count)
    {
        if (count > FightCardManger.Instance.cardList.Count)
        {
            count = FightCardManger.Instance.cardList.Count;
        }
        for (int i=0; i< count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"),transform)as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width/-2, Screen.height/-2);
            //var item = obj.AddComponent<CardItem>();

            string cardId = FightCardManger.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManger.Instance.GetCardByID(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }
    }

    //更新卡牌位置
    public void UpdateCardItemPos()
    {
        //Debug.Log(cardItemList.Count);
        float offset = Screen.width * 0.5f / (cardItemList.Count+1);
        Vector2 startPos = new Vector2(-(cardItemList.Count+1)/ 2.0f * offset + offset * 1f, -Screen.height/2.0f - 100);
        for (int i = 0; i < cardItemList.Count; i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x = startPos.x + offset;
        }
    }

    //删除卡牌物体
    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");//移除音效

        item.enabled = false;//禁用卡牌逻辑

        //添加到弃牌合集
        FightCardManger.Instance.usedCardList.Add(item.data["Id"]);

        //更新使用后的卡牌数量
        noCardCountTxt.text = FightCardManger.Instance.usedCardList.Count.ToString();

        //从集合中删除
        cardItemList.Remove(item);

        //刷新卡牌位置
        UpdateCardItemPos();

        //卡牌移动到弃牌堆效果
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(Screen.width , -Screen.height), 0.25f);

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject, 1);
    }

    public void RemoveAllCards()
    {
        //Debug.Log(cardItemList.Count);
        //Debug.Log(cardItemList);
        for (int i = cardItemList.Count-1; i > 0; i-=1)
        {
            RemoveCard(cardItemList[i]);
            Debug.Log(cardItemList.Count);
        }
        RemoveCard(cardItemList[0]);
    }
    //看牌库中的牌
    private void OpenhasCardList()
    {
        UIManager.Instance.ShowUI<hasCardUI>("BagUI");
        Debug.Log("打开背包");
    }
    //看弃牌堆中的牌
    private void OpenUsedCardList()
    {
        UIManager.Instance.ShowUI<UsedCardUI>("BagUI");
        Debug.Log("打开背包");
    }
    public void CloseFightUI()
    {
        Close();
    }
}
