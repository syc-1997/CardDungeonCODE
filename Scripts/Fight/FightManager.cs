using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//ս��ö��
public enum FightType
{
    None,
    Init,
    Player,
    Enemy,
    win,
    Loss
}

//ս��������
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;

    public FightUnit fightUnit;//ս����Ԫ

    public int MaxHp;//���HP
    public int CurHp;//��ǰѪ��

    public int MaxPowerCount;//��������������ʹ�û�����������
    public int CurPowerCount;//��ǰ����
    public int DefenseCount;//����ֵ


    //��ʼ��
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

    //�л�ս������
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

        fightUnit.Init();//��ʼ��
    }

    //��������߼�
    public void GetPlayerHit(int hit)
    {
        //���������
        Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
        //�ۻ���
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

                //�л�����Ϸʧ��
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
