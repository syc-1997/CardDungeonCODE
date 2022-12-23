using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���˹�����
public class EnemyManager 
{
    public static EnemyManager Instance = new EnemyManager();

    public
        List<Enemy> enemyList;//�洢ս���еĵ���

    //���ص�����Դ

    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();

        //��ȡ�ؿ���
        Dictionary<string, string> levelDate = GameConfigManger.Instance.GetLevelByID(id);

        //����id��Ϣ
        string[] enemyIds = levelDate["EnemyIds"].Split('=');

        string[] enemyPos = levelDate["Pos"].Split('=');//����λ����Ϣ

        for (int i = 0; i<enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');           

            //����λ��
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            //���ݵ���id��õ���������Ϣ
            Dictionary<string, string> enemyData = GameConfigManger.Instance.GetEnemyByID(enemyId);

            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//����Դ·�����ض�Ӧ�ĵ���ģ��
            obj.name = string.Concat(obj.name, i);
            Enemy enemy = obj.AddComponent<Enemy>();//��ӵ��˽ű�
            enemy.Init(enemyData);//�洢������Ϣ

            //�洢������
            enemyList.Add(enemy);

            obj.transform.position = new Vector3(x, y, z);
        }
    }

    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //�Ƿ��ɱ���й����ж�
        if(enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.win);
        }
    }

    //ִ�л��ߵĹ������Ϊ
    public IEnumerator DoAllEnemyAction()
    {
        for (int i = 0; i<enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //�ж���� ���� ���е�����Ϊ
        for(int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }

    
}
