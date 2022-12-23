using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//战斗枚举
public enum FightType
{
    None,
    Init,
    Player,
    Enemy,
    win,
    Loss
}

//战斗管理器
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit;//战斗单元

    public int MaxHp;//最大HP
    public int CurHp;//当前血量

    public int MaxPowerCount;//最大的能量（卡牌使用会消耗能量）
    public int CurPowerCount;//当前能量
    public int DefenseCount;//防御值


    //初始化
    public void Init()
    {
        MaxHp = 10;
        CurHp = 10;
        MaxPowerCount = 3;
        CurPowerCount = 3;
        DefenseCount = 1;
    }

    private void Awake()
    {
        Instance = this;
    }

    //切换战斗类型
    public void ChangeType(FightType type)
    {
        switch(type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                fightUnit = new Fight_Loss();
                break;
        }

        fightUnit.Init();//初始化
    }

    //玩家受伤逻辑
    public void GetPlayerHit(int hit)
    {
        //摄像机抖动
        Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
        //扣护盾
        if (DefenseCount >= hit)
        {
            DefenseCount -= hit;
        }
        else
        {
            hit = hit - DefenseCount;
            DefenseCount = 0;
            CurHp -= hit;
            if (CurHp <= 0)
            {
                CurHp = 0;

                //切换到游戏失败
                ChangeType(FightType.Loss);
            }
        }

        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHP();       
    }

    private void Update()
    {
        if(fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }

    
}
