using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using UnityEngine;

public class CSVReadManager : MonoBehaviour
{
    public GameObject ToDoll; //������
    public GameObject ToFlag; //�Ķ���

    int fileNum; //�� ���� ����
    string[] fileEntries; //���ϸ� �迭
    List<string> m_ColumnHeadings = new List<string>(); //�� �̸�
    string m_Path = Application.streamingAssetsPath; //csv ���� ����� ���
    List<DATA> csvData = new List<DATA>(); //��� ������

    int printN = -1; //����� ���� ��ȣ(�ӽ�)

    public struct DATA
    {
        public int cnt; //����
        public  (int, int) dollPos; //���� ��ġ
        public List<(int, int)> mousePos1, mousePos2; //���� ����, ���� ����
    };


    void Start()
    {
        m_ColumnHeadings.Add("����");
        m_ColumnHeadings.Add("���� ��ġ");
        m_ColumnHeadings.Add("���� ����");
        m_ColumnHeadings.Add("���� ����");

        fileNum = GetFileNumber(m_Path);
        Debug.Log("���� ���� "+fileNum);
        ReadCSV(fileEntries[fileNum-1]); //0��° ���� �ҷ�����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            DrawTrace(--printN);
        if (Input.GetKeyDown(KeyCode.X))
            DrawTrace(++printN);
    }

    int GetFileNumber(string targetDirectory) //���� ���� ã��
    {
        fileEntries = Directory.GetFiles(targetDirectory, "*.csv"); //Ȯ���ڰ� csv�� ���ϸ� ã��

        foreach (string fileName in fileEntries)
        {
            Debug.Log(fileName);
        }

        return fileEntries.Length;
    }

    void ReadCSV(string filePath) //csv ���� �ҷ�����
    {
        string value = "";
        StreamReader reader = new StreamReader(filePath, Encoding.UTF8);
        value = reader.ReadToEnd();
        reader.Close();

        string[] lines = value.Split("\n"[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');
            if (row.Length <= 1)
                break;

            DATA tempData = new DATA();
            for(int j = 0; j < row.Length; j++)
            {
                string str = row[j], head = m_ColumnHeadings[j];
                if (head == "����")
                    tempData.cnt = Int32.Parse(str);
                else if (head == "���� ��ġ")
                {
                    string[] dollStr = str.Split('/');
                    tempData.dollPos.Item1 = Int32.Parse(dollStr[0]);
                    tempData.dollPos.Item2 = Int32.Parse(dollStr[1]);
                }
                else if (head == "���� ����")
                {
                    tempData.mousePos1 = new List<(int, int)>();
                    string[] mouseStr = str.Split(' ');
                    foreach (string mStr in mouseStr)
                    {
                        if (mStr.Length <= 1)
                            continue;
                        //if (mStr == "\n" || mStr == " " || mStr == "")
                        //{
                        //    Debug.Log("��..��..");
                        //    continue;
                        //}
                        string[] mouseSplit = mStr.Split('/');
                        //foreach (string sttt in mouseSplit)
                        //    Debug.Log(sttt);
                        //Debug.Log("���ྲ..");
                        tempData.mousePos1.Add((Int32.Parse(mouseSplit[0]), Int32.Parse(mouseSplit[1])));
                    }
                }
                else if (head == "���� ����")
                {
                    tempData.mousePos2 = new List<(int, int)>();
                    string[] mouseStr = str.Split(' ');
                    foreach (string mStr in mouseStr)
                    {
                        if (mStr.Length <= 1) //������ �̰�
                            continue;
                        string[] mouseSplit = mStr.Split('/');
                        tempData.mousePos2.Add((Int32.Parse(mouseSplit[0]), Int32.Parse(mouseSplit[1])));
                    }
                }
            }

            csvData.Add(tempData);
            Debug.Log("--------------------------------");
            foreach (var str in row)
                Debug.Log(str);
        }

        Debug.Log("�б� ��!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        foreach (DATA data in csvData)
        {
            Debug.Log("����: " + data.cnt);
            Debug.Log("���� ��ġ: " + data.dollPos);
            Debug.Log("-------------------------------------Pos1!-------------------------------------");
            foreach (var pos in data.mousePos1)
                Debug.Log(pos);
            Debug.Log("-------------------------------------Pos2!-------------------------------------");
            foreach (var pos in data.mousePos2)
                Debug.Log(pos);
        }
    }

    public void DrawTrace(int cnt) //���� �׸���
    {
        if (cnt >= csvData.Count || cnt < 0)
        {
            Debug.Log("Range Out");
            return;
        }
        var childObj = GetComponentsInChildren<Transform>();

        foreach (var iter in childObj)
        {
            if (iter != this.transform)
                Destroy(iter.gameObject);
        }
        DATA data = csvData[cnt];

        foreach (var pos in data.mousePos1)
        {
            Vector2 cPos = Camera.main.ScreenToWorldPoint(new Vector2(pos.Item1, pos.Item2));
            GameObject child = Instantiate(ToDoll, cPos, Quaternion.identity);
            child.transform.parent = this.gameObject.transform;
        }

        foreach (var pos in data.mousePos2)
        {
            Vector2 cPos = Camera.main.ScreenToWorldPoint(new Vector2(pos.Item1, pos.Item2));
            GameObject child = Instantiate(ToFlag, cPos, Quaternion.identity);
            child.transform.parent = this.gameObject.transform;
        }
    }
}
