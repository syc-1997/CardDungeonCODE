using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameUI : UIBsae
{
    public static GameUI Instance;
    void Awake()
    {
        GameUI.Instance = this;
    }
    public void CloseUI(GameObject obj, PointerEventData pDate)
    {
        Close();
    }

}
