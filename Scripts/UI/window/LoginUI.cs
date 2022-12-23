using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//��ʼ���棨Ҫ�̳�UIBase��
public class LoginUI : UIBsae
{
    private void Awake()
    {
        //��ʼ��Ϸ
        Register("bg/startBtn").onClick = onStartGameBtn;
        //�˳���Ϸ
        Register("bg/quitBtn").onClick = onQuitGameBtn;
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pDate)
    {
        //�ر�login����
        Close();

        //ս����ʼ��
        FightManager.Instance.ChangeType(FightType.Init);
    }

    private void onQuitGameBtn(GameObject obj, PointerEventData pDate)
    {
        Application.Quit();
    }
}
