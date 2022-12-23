using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UsedCardUI : UIBsae
{
    public Transform Content;
    void Awake()
    {
        //关闭
        Register("bg/returnBtn").onClick = onCloseUI;
    }
    private void Start()
    {
        int count1 = FightCardManger.Instance.usedCardList.Count;
        Content = transform.Find("bg/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        CreatCardItem1(count1);
    }
    public void CreatCardItem1(int count1)
    {

        for (int i = 0; i < count1; i++)
        {
            Debug.Log(i);
            int nmbX = i % 6;
            int nmbY = i / 6;
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(nmbX * 370 - 900, nmbY * -500+400);
            var item = obj.AddComponent<ShowCard>();
            obj.transform.SetParent(Content.transform,true);
            string cardId = FightCardManger.Instance.ShowusedCard(i);
            Dictionary<string, string> data1 = GameConfigManger.Instance.GetCardByID(cardId);
            //CardUI item = obj.AddComponent(System.Type.GetType(data1["Script"])) as CardUI;
            item.Init(data1);
        }
    }

    //关闭界面
    private void onCloseUI(GameObject obj, PointerEventData pDate)
    {
        Close();
    }
}
