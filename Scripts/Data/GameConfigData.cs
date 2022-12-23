using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigData
{
    private List<Dictionary<string, string>> dataDic;//�洢���ñ��е���������

    public GameConfigData(string str)
    {
        dataDic = new List<Dictionary<string, string>>();

        //�����и�
        string[] lines = str.Split('\n');
        //��һ�д洢��������
        string[] title = lines[0].Trim().Split('\t');
        //�ӵ����л��Ǳ������ݣ��ڶ���������˵��
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
