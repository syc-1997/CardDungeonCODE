using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ß∞‹
public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        Debug.Log(" ß∞‹");

        FightManager.Instance.StopAllCoroutines();
        
        //œ‘ æ ß∞‹ΩÁ√Ê

    }

    public override void OnUpdate()
    {
    }
}
