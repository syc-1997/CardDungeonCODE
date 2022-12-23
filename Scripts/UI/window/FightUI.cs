using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


//ս��UI
public class FightUI : UIBsae
{

    private Text cardCountTxt;//��������
    private Text noCardCountTxt;//���ƶ�����
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;
    private List<CardItem> cardItemList;//�洢���Ƶļ���

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

    //�л������˻غ�
    private void onChangeTurnBtn()
    {
        //ֻ����һغϲ����л�
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

    //��������

    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
    }

    //��������
    public void UpdateDefense()
    {
        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
    }

    //���¿�������
    public void UpdateCardCount()

    {
        cardCountTxt.text = FightCardManger.Instance.cardList.Count.ToString();
    }


    //�������ƶ�
    public void UpdateUsedCardCount()
    {
        noCardCountTxt.text = FightCardManger.Instance.usedCardList.Count.ToString();
    }

    //������������
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

    //���¿���λ��
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

    //ɾ����������
    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");//�Ƴ���Ч

        item.enabled = false;//���ÿ����߼�

        //��ӵ����ƺϼ�
        FightCardManger.Instance.usedCardList.Add(item.data["Id"]);

        //����ʹ�ú�Ŀ�������
        noCardCountTxt.text = FightCardManger.Instance.usedCardList.Count.ToString();

        //�Ӽ�����ɾ��
        cardItemList.Remove(item);

        //ˢ�¿���λ��
        UpdateCardItemPos();

        //�����ƶ������ƶ�Ч��
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
    //���ƿ��е���
    private void OpenhasCardList()
    {
        UIManager.Instance.ShowUI<hasCardUI>("BagUI");
        Debug.Log("�򿪱���");
    }
    //�����ƶ��е���
    private void OpenUsedCardList()
    {
        UIManager.Instance.ShowUI<UsedCardUI>("BagUI");
        Debug.Log("�򿪱���");
    }
    public void CloseFightUI()
    {
        Close();
    }
}
