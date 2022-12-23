using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Dictionary<string, string> data;//������Ϣ

    public void Init(Dictionary<string,string>data)
    {
        this.data =  data;
    }

    private int index;
   

    private void Start()
    {
        //        Id Name    Script Type    Des BgIcon  Icon Expend  Arg0 Effects
        //Ψһ�ı�ʶ�������ظ���	���� ������ӵĽű� �������͵�Id ����  ���Ƶı���ͼ��Դ·�� ͼ����Դ��·�� ���ĵķ��� ����ֵ ��Ч
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"],data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];

        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManger.Instance.GetCardTypeByID(data["Type"])["Name"];

        //���ÿ��Ʊ�����߿����
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outLine"));
    }
    //������
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.5f, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    //����뿪
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    Vector2 initPos;//��ק��ʼʱ��¼���Ƶ�λ��
    //��ʼ��ק
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //��������
        AudioManager.Instance.PlayEffect("Cards/draw");
    }

    //��ק��
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos
            ))
        {
            //pos.y += Screen.height -300f;
            //transform.GetComponent<RectTransform>().sizeDelta.y / 2
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    //������ק

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    //����ʹ�ÿ���
    public virtual bool TryUse()
    {
        //������Ҫ�ķ���
        int cost = int.Parse(data["Expend"]);

        if (cost > FightManager.Instance.CurPowerCount)
        {
            //û�ò���
            AudioManager.Instance.PlayEffect("Effect/lose");//ʹ��ʧ����Ч

            //��ʾ
            UIManager.Instance.ShowTip("�����Ȥ����ʤ�", Color.red);

            return false;
        }
        else
        {
            //���ٷ���
            FightManager.Instance.CurPowerCount -= cost;
            //ˢ�·����ı�
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();


            //ʹ�õĿ���ɾ��
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);

            return true;
        }
    }

    //��������ʹ�ú����Ч
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectObj.transform.position = pos;
        Destroy(effectObj, 2);
    }
}
