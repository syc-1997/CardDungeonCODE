using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人管理器
public class EnemyManager 
{
    public static EnemyManager Instance = new EnemyManager();

    public
        List<Enemy> enemyList;//存储战斗中的敌人

    //加载敌人资源

    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();

        //读取关卡表
        Dictionary<string, string> levelDate = GameConfigManger.Instance.GetLevelByID(id);

        //敌人id信息
        string[] enemyIds = levelDate["EnemyIds"].Split('=');

        string[] enemyPos = levelDate["Pos"].Split('=');//敌人位置信息

        for (int i = 0; i<enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');           

            //敌人位置
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            //根据敌人id获得单个敌人信息
            Dictionary<string, string> enemyData = GameConfigManger.Instance.GetEnemyByID(enemyId);

            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//从资源路径加载对应的敌人模型
            obj.name = string.Concat(obj.name, i);
            Enemy enemy = obj.AddComponent<Enemy>();//添加敌人脚本
            enemy.Init(enemyData);//存储敌人信息

            //存储到集合
            enemyList.Add(enemy);

            obj.transform.position = new Vector3(x, y, z);
        }
    }

    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //是否击杀所有怪物判断
        if(enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.win);
        }
    }

    //执行或者的怪物的行为
    public IEnumerator DoAllEnemyAction()
    {
        for (int i = 0; i<enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //行动完后 更新 所有敌人行为
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    
}
