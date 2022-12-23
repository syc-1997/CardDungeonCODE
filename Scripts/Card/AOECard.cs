using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AOECard : CardItem
{
    List<Enemy> hitEnemy = new List<Enemy>();

    private void OnEnable()
    {
        hitEnemy = EnemyManager.Instance.enemyList.ConvertAll(a => a); 
        Debug.Log("ִ��һ��");
    }
    public override void OnDrag(PointerEventData eventData)
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
        for(int i = 0; i < hitEnemy.Count; i++)
        {
            hitEnemy[i].OnSelect();
        }
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        
        
        if (TryUse() == true)
        {
            
            transform.GetComponent<Enemy>();
            //����ʹ�ú������
            AudioManager.Instance.PlayEffect("Effect/sword");
            for (int i = 0; i < hitEnemy.Count; i++)
            {
                Debug.Log("AOE");
                Debug.Log(hitEnemy.Count);
                Debug.Log(i);
                //��������
                int val = int.Parse(data["Arg0"]);
                hitEnemy[i].Hit(val);
                //����δѡ��
                hitEnemy[i].OnUnSelect();
            }
            //������Ч
            PlayEffect(new Vector3(0,0,1));            
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
