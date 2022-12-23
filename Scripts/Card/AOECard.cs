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
        Debug.Log("执行一次");
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
            //播放使用后的声音
            AudioManager.Instance.PlayEffect("Effect/sword");
            for (int i = 0; i < hitEnemy.Count; i++)
            {
                Debug.Log("AOE");
                Debug.Log(hitEnemy.Count);
                Debug.Log(i);
                //敌人受伤
                int val = int.Parse(data["Arg0"]);
                hitEnemy[i].Hit(val);
                //敌人未选中
                hitEnemy[i].OnUnSelect();
            }
            //播放特效
            PlayEffect(new Vector3(0,0,1));            
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
