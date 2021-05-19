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

    int count; //현재 횟수
    string m_FilePath; //파일 경로
    List<(int, int)> mPos; //마우스 궤적
    List<string> wData; //한 행에 들어갈 데이터
    List<List<string>> csvData = new List<List<string>>(); //모든 데이터

    void Start()
    {
        m_ColumnHeadings.Add("순서");
        m_ColumnHeadings.Add("인형 위치");
        m_ColumnHeadings.Add("가는 궤적");
        m_ColumnHeadings.Add("오는 궤적");
        csvData.Add(m_ColumnHeadings);

        m_FilePath = m_Path + @"\" + DateTime.Now.ToString("yyyyMMddHHmm") + ".csv";
    }

    public void InitiateObject(Vector2 vec)
    {
        vec = Camera.main.WorldToScreenPoint(vec);
        int vecX = (int)vec.x, vecY = (int)vec.y;
        count++; //행 증가

        wData = new List<string>(); //csv로 만들 데이터 한 행
        wData.Add(count.ToString());
        wData.Add(vecX.ToString()+"/"+vecY.ToString());

        StartCoroutine(DrawTrace()); //마우스 궤적 시작
    }

    public void TurningPoint() //반환점
    {
        StopAllCoroutines();
        TraceToString();
        StartCoroutine(DrawTrace()); //마우스 궤적 시작
    }

    public void EndTrace() //기록 종료
    {
        StopAllCoroutines();
        TraceToString();
        csvData.Add(wData);
    }

    void TraceToString() //마우스 궤적을 문자열로 변환
    {
        string str = "";
        foreach (var mp in mPos)
        {
            str += mp.Item1.ToString() + "/" + mp.Item2.ToString() + " ";
        }
        wData.Add(str);
        Debug.Log(str);
    }

    IEnumerator DrawTrace() //가는 궤적
    {
        mPos = new List<(int, int)>(); //마우스 궤적
        while (true)
        {
            int mouseX = (int)Input.mousePosition.x;
            int mouseY = (int)Input.mousePosition.y;

            mPos.Add((mouseX, mouseY));

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void WriteData() //데이터 기록
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

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder stringBuilder = new StringBuilder();

        for (int index = 0; index < length; index++)
            stringBuilder.AppendLine(string.Join(delimiter, output[index]));

        Stream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        StreamWriter outStream = new StreamWriter(fileStream, Encoding.UTF8);
        outStream.WriteLine(stringBuilder);
        outStream.Close();

        Debug.Log("CSV파일로 저장");
    }
}
