using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�û���Ϣ����
public class RoleManager
{
    public static RoleManager Instance = new RoleManager();

    public List<string> cardList;//�洢ӵ�еĿ���id
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
