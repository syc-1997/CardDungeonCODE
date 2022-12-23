using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTF;//画布变换组件

    private List<UIBsae> uiList;

    private void Awake()
    {
        Instance = this;
        //找世界中的画布
        canvasTF = GameObject.Find("Canvas").transform;
        //初始化list
        uiList = new List<UIBsae>();
    }

    public UIBsae ShowUI<T>(string uiName) where T : UIBsae
    {
        UIBsae ui = Find(uiName);
        if (ui == null)
        {
            //如果集合中没有，需要从Resources/UI文件中加载
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTF) as GameObject;

            //改名字
            obj.name = uiName;
            //添加需要的脚本
            ui = obj.AddComponent<T>();

            //添加到集合中储存
            uiList.Add(ui);
        }
        else
        {
            //显示
            ui.Show();
        }

        return ui;
    }

    //隐藏
    public void HideUI(string uiNmae)
    {
        UIBsae ui = Find(uiNmae);
        if(ui != null)
        {
            ui.Hide();
        }

    }

    //关闭所有界面
    public void CloseAllUI()
    {
        for (int i = uiList.Count - 1; i>=0; i--)
        {
            Destroy(uiList[i].gameObject);
        }

        uiList.Clear();//清空集合
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

    //从集合中找到名字对应的界面
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

    //获得某个界面的脚本
    public T GetUI<T>(string uiName) where T : UIBsae
    {
        UIBsae ui = Find(uiName);
        if(ui != null)
        {
            return ui.GetComponent <T>();
        }
        return null;
    }


    //创建敌人头部的行动图标物体
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI/actionIcon"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//设置在父级第一位
        return obj;
    }

    //创建敌人底部的血量物体
    public GameObject CreateHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/HpItem"), canvasTF) as GameObject;
        obj.transform.SetAsFirstSibling();//设置在父级最后一位
        return obj;
    }


    //提示界面
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
