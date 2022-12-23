using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTF;//�����任���

    private List<UIBsae> uiList;

    private void Awake()
    {
        Instance = this;
        //�������еĻ���
        canvasTF = GameObject.Find("Canvas").transform;
        //��ʼ��list
        uiList = new List<UIBsae>();
    }

    public UIBsae ShowUI<T>(string uiName) where T : UIBsae
    {
        UIBsae ui = Find(uiName);
        if (ui == null)
        {
            //���������û�У���Ҫ��Resources/UI�ļ��м���
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTF) as GameObject;

            //������
            obj.name = uiName;
            //�����Ҫ�Ľű�
            ui = obj.AddComponent<T>();

            //��ӵ������д���
            uiList.Add(ui);
        }
        else
        {
            //��ʾ
            ui.Show();
        }

        return ui;
    }

    //����
    public void HideUI(string uiNmae)
    {
        UIBsae ui = Find(uiNmae);
        if(ui != null)
        {
            ui.Hide();
        }

    }

    //�ر����н���
    public void CloseAllUI()
    {
        for (int i = uiList.Count - 1; i>=0; i--)
        {
            Destroy(uiList[i].gameObject);
        }

        uiList.Clear();//��ռ���
    }

    public void CloseUI(string uiNmae)
    {
        UIBsae ui = Find(uiNmae);
        if (ui != null)
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);
        }

    }

    //�Ӽ������ҵ����ֶ�Ӧ�Ľ���
    public UIBsae Find(string uiNmae)
    {
        for (int i = 0; i<uiList.Count; i++)
        {
            if (uiList[i].name == uiNmae)
            {
                return uiList[i];
            }
        }
        return null;
    }

    //���ĳ������Ľű�
    public T GetUI<T>(string uiName) where T : UIBsae
    {
        UIBsae ui = Find(uiName);
        if(ui != null)
        {
            return ui.GetComponent <T>();
        }
        return null;
    }


    //��������ͷ�����ж�ͼ������
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//�����ڸ�����һλ
        return obj;
    }

    //�������˵ײ���Ѫ������
    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//�����ڸ������һλ
        return obj;
    }


    //��ʾ����
    public void ShowTip(string msg, Color color,System.Action callback = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/Tips"), canvasTF) as GameObject;
        Text text = obj.transform.Find("bg/Text").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("bg").DOScale(1, 0.4f);
        Tween scale2 = obj.transform.Find("bg").DOScale(1, 0.4f);

        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate ()
        {
            if(callback != null)
            {
                callback();
            }
        });
        MonoBehaviour.Destroy(obj, 1);
    }
}
