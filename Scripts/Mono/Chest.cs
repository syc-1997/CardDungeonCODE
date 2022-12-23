using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static Chest Instance;
    private void Awake()
    {
        Chest.Instance = this;
        Transform transform = gameObject.transform.Find("ChestTD");
        
    }
    private void Update()
    {
        //Debug.Log(transform.position.z);
        if(transform.position.z - Camera.main.transform.position.z < 3f)
        {
            OpenChest();
        }
    }
    public void OpenChest()
    {
        Transform transform = gameObject.transform.Find("ChestTD");
        var target = Quaternion.Euler(new Vector3(-120, 180, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
        float x = transform.eulerAngles.x;
        //Debug.Log(x);
        if (x < 296f)
        {
            UIManager.Instance.ShowUI<SelectCardUI>("SelectCardUI");
            //Debug.Log("打开宝箱");
        }
        else
        {
            //Debug.Log("没打开");
        }
    }

    public void DeleteChest()
    {
        gameObject.SetActive(false);
    }
}
