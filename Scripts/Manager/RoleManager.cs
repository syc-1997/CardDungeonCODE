using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//用户信息管理
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//存储拥有的卡牌id
    public void Init()
    {
        cardList = new List<string>();
        
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");
        cardList.Add("1000");

        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");
        cardList.Add("1001");

        cardList.Add("1002");
        cardList.Add("1002");

        cardList.Add("1003");
        cardList.Add("1003");
    }
}
