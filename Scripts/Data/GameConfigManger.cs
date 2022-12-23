using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//游戏配置表管理器
public class GameConfigManger
{
    public static GameConfigManger Instance = new GameConfigManger();

    private GameConfigData cardData;//卡牌表

    private GameConfigData enemyData;//敌人表

    private GameConfigData levelData;//关卡表

    private GameConfigData cardTypeDate;//卡牌类型表

    private TextAsset textAsset;

    //初始化配置文件（txt存储到内存中）
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
