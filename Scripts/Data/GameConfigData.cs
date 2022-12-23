using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigData
{
    private List<Dictionary<string, string>> dataDic;//存储配置表中的所有数据

    public GameConfigData(string str)
    {
        dataDic = new List<Dictionary<string, string>>();

        //换行切割
        string[] lines = str.Split('\n');
        //第一行存储数据类型
        string[] title = lines[0].Trim().Split('\t');
        //从第三行还是遍历数据，第二行是数据说明
        for(int i = 2;i < lines.Length; i++)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string[] tempArr = lines[i].Trim().Split('\t');

            for(int j = 0; j < tempArr.Length; j++)
            {
                dic.Add(title[j], tempArr[j]);
            }

            dataDic.Add(dic);
        }
    }

    public List<Dictionary<string, string>> GetLines()
    {
        return dataDic;
    }

    public Dictionary<string,string>GetOneById(string id)
    {
        for(int i = 0; i < dataDic.Count; i++)
        {
            Dictionary<string, string> dic = dataDic[i];
            if (dic["Id"] == id)
            {
                return dic;
            }
        }
        return null;
    }
}
