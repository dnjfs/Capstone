using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using UnityEngine;

public class CSVWriteManager : MonoBehaviour
{
    string m_Path = Application.streamingAssetsPath;
    List<string> m_ColumnHeadings = new List<string>();

    int count; //���� Ƚ��
    string m_FilePath; //���� ���
    List<(int, int)> mPos; //���콺 ����
    float moveTime; //�̵��ð�
    float errorAngle; //��������
    List<string> wData; //�� �࿡ �� ������
    List<List<string>> csvData = new List<List<string>>(); //��� ������

    void Start()
    {
        m_ColumnHeadings.Add("����");
        m_ColumnHeadings.Add("���� ��ġ");
        m_ColumnHeadings.Add("���� ����");
        m_ColumnHeadings.Add("���� ����");
        m_ColumnHeadings.Add("�̵��ð�");
        m_ColumnHeadings.Add("��������");
        csvData.Add(m_ColumnHeadings);

        m_FilePath = m_Path + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".csv";
    }

    public void InitiateObject(Vector2 vec)
    {
        vec = Camera.main.WorldToScreenPoint(vec);
        int vecX = (int)vec.x, vecY = (int)vec.y;
        count++; //�� ����

        wData = new List<string>(); //csv�� ���� ������ �� ��
        wData.Add(count.ToString());
        wData.Add(vecX.ToString()+"/"+vecY.ToString());

        StartCoroutine(DrawTrace()); //���콺 ���� ����
    }

    public void TurningPoint() //��ȯ��
    {
        StopAllCoroutines();
        TraceToString();
        StartCoroutine(DrawTrace()); //���콺 ���� ����
    }

    public void EndTrace() //��� ����
    {
        StopAllCoroutines();
        TraceToString();
        moveTime = GameObject.Find("Timer").GetComponent<Timer>().GetTime();
        errorAngle = GameObject.Find("flag").GetComponent<flag>().GetMisAngle();
        wData.Add(moveTime.ToString());
        wData.Add(errorAngle.ToString());
        csvData.Add(wData);
    }

    void TraceToString() //���콺 ������ ���ڿ��� ��ȯ
    {
        string str = "";
        foreach (var mp in mPos)
        {
            str += mp.Item1.ToString() + "/" + mp.Item2.ToString() + " ";
        }
        wData.Add(str);
        Debug.Log(str);
    }

    IEnumerator DrawTrace() //���� ����
    {
        mPos = new List<(int, int)>(); //���콺 ����
        while (true)
        {
            int mouseX = (int)Input.mousePosition.x;
            int mouseY = (int)Input.mousePosition.y;

            mPos.Add((mouseX, mouseY));

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void WriteData() //������ ���
    {
        WriteCSV(csvData, m_FilePath);
    }

    void WriteCSV(List<List<string>> rowData, string filePath)
    {
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = new string[rowData[i].Count];
            for (int j = 0; j < output[i].Length; j++)
            {
                output[i][j] = rowData[i][j];
            }
        }

        StringBuilder stringBuilder = new StringBuilder();

        for (int index = 0; index < output.Length; index++)
            stringBuilder.AppendLine(string.Join(",", output[index]));

        Stream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        StreamWriter outStream = new StreamWriter(fileStream, Encoding.UTF8);
        outStream.WriteLine(stringBuilder);
        outStream.Close();

        Debug.Log("CSV���Ϸ� ����");
    }
}
