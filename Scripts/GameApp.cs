using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ϸ��ڽű�
public class GameApp : MonoBehaviour
{
    void Start()
    {
        //��ʼ�����ñ�
        GameConfigManger.Instance.Init();

        //��ʼ����Ƶ������
        AudioManager.Instance.Init();

        //��ʼ���û���Ϣ
        RoleManager.Instance.Init();

        //��ʾloginUI
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //����BGM
        AudioManager.Instance.PlayBGM("bgm1");

        //test
        string name = GameConfigManger.Instance.GetCardByID("1002")["Des"];

        print(name);
    }
}
