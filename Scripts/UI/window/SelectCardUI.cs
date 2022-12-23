using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCardUI : UIBsae
{   
    void Awake()
    {
        //¹Ø±Õ
        Register("bg/content/returnBtn").onClick = onCloseUI;
        
    }

    

    
    private void onCloseUI(GameObject obj, PointerEventData pDate)
    {
        Chest.Instance.DeleteChest();
        GameUI.Instance.Close();
        Close();
    }
}
