using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�������

public class UIBsae : MonoBehaviour
{
    //ע���¼�
    public UIEventTrigger Register(string name)
    {
        Transform tf = transform.Find(name);
        return UIEventTrigger.Get(tf.gameObject);
    }

    public virtual void Show()
    {
        //��ʾ
        gameObject.SetActive(true);
    }
    //����
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    //�رս���(����)
    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }
}
