using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enemy脚本   

public enum ActionType
{
    None,
    Defend,//加防御
    Attack,//攻击

}
public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data; //敌人数据表信息

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionObj;

    //UI相关
    public Transform attackTf;
    public Transform defendTf;
    public Text defendTxt;
    public Text hpTxt;
    public Image hpImg;

    //数值相关
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //组件相关
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;


    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        ani = transform.GetComponent<Animator>();

        //Hp	Attack	Defend
        //初始化数值
        Attack = int.Parse(data["Attack"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;
        Defend = int.Parse(data["Defend"]);


        type = ActionType.None;
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionObj = UIManager.Instance.CreateActionIcon();

        //设置血条 行动位置
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down * 0.2f);
        actionObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position);

        attackTf = actionObj.transform.Find("attack");
        defendTf = actionObj.transform.Find("defend");

        defendTxt = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpTxt = hpItemObj.transform.Find("hpTxt").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        SetRandomAction();


        UpdateHp();
        UpdateDefend();
    }
    private void Update()
    {
        
    }
    //随机一个行动
    public void SetRandomAction()
    {
        int ran = Random.Range(1, 3);

        type = (ActionType)ran;

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackTf.gameObject.SetActive(false);
                defendTf.gameObject.SetActive(true);
                break;
            case ActionType.Attack:
                attackTf.gameObject.SetActive(true);
                defendTf.gameObject.SetActive(false);
                break;
        }
    }

    public void UpdateHp()
    {

        hpTxt.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //更新防御信息
    public void UpdateDefend()
    {
        defendTxt.text = Defend.ToString();
    }

    //被攻击卡选中，显示红边
    public void OnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor",Color.red);
    }

    //未选中
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor",Color.black);
    }

    //受伤
    public void Hit(int val)
    {
        //先扣护盾
        if (Defend >= val)
        {
            //扣护盾
            Defend -= val;

            //播放受伤
            ani.Play("hit", 0, 0);
        }
        else
        {
            val = val - Defend;
            Defend = 0;
            CurHp -= val;
            if(CurHp <= 0)
            {
                CurHp = 0;
                Destroy(gameObject, 1);
                Destroy(actionObj);
                Destroy(hpItemObj);
                
                //播放死亡
                ani.Play("die");

                //从敌人列表中移除
                EnemyManager.Instance.DeleteEnemy(this);            
            }
            else
            {
                //受伤
                ani.Play("hit",0,0);
            }
        }

        //刷新UI
        UpdateDefend();
        UpdateHp();
    }

    //隐藏怪物头上的行动标志
    public void HideAction()
    {
        attackTf.gameObject.SetActive(false);
        defendTf.gameObject.SetActive(false);
    }

    //执行敌人行动
    public IEnumerator DoAction()
    {
        HideAction();

        //播放对应动画（可以在excel表配置）
        ani.Play("atttack");
        //等待某一段时间后执行对应的行为（也可以配置到excel表）
        yield return new WaitForSeconds(0.5f);//目前是写死的状态

        switch (type)
        {
            case ActionType.None:
                break;
            case ActionType.Defend:

                //加防御
                Defend += 1;
                UpdateDefend();
                //可以播放对应的特效
                break;
            case ActionType.Attack:

                //玩家扣血
                FightManager.Instance.GetPlayerHit(Attack);
                
                break;
                
        }
        //等待动画播放完
        yield return new WaitForSeconds(1);
        //播放待机
        ani.Play("idle");
    }

}
