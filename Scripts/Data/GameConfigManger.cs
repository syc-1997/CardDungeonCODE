using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��Ϸ���ñ������
public class GameConfigManger
{
    public static GameConfigManger Instance = new GameConfigManger();

    private GameConfigData cardData;//���Ʊ�

    private GameConfigData enemyData;//���˱�

    private GameConfigData levelData;//�ؿ���

    private GameConfigData cardTypeDate;//�������ͱ�

    private TextAsset textAsset;

    //��ʼ�������ļ���txt�洢���ڴ��У�
    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeDate = new GameConfigData(textAsset.text);
    }

    public List<Dictionary<string, string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    public List<Dictionary<string, string>> GetLeveLine()
    {
        return levelData.GetLines();
    }

    public List<Dictionary<string, string>> GetEnemyLine()
    {
        return enemyData.GetLines();
    }

    public Dictionary<string,string> GetCardByID(string id)
    {
        return cardData.GetOneById(id);
    }

    public Dictionary<string, string> GetEnemyByID(string id)
    {
        return enemyData.GetOneById(id);
    }

    public Dictionary<string, string> GetLevelByID(string id)
    {
        return levelData.GetOneById(id);
    }

    public Dictionary<string,string> GetCardTypeByID(string id)
    {
        return cardTypeDate.GetOneById(id);
    }
}
