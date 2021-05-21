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
    public GameObject ToDoll; //빨간점
    public GameObject ToFlag; //파란점

    int fileNum; //총 파일 개수
    string[] fileEntries; //파일명 배열
    List<string> m_ColumnHeadings = new List<string>(); //열 이름
    string m_Path = Application.streamingAssetsPath; //csv 파일 저장된 경로
    List<DATA> csvData = new List<DATA>(); //모든 데이터

    int printN = -1; //출력할 궤적 번호(임시)

    public struct DATA
    {
        public int cnt; //순서
        public  (int, int) dollPos; //인형 위치
        public List<(int, int)> mousePos1, mousePos2; //가는 궤적, 오는 궤적
    };


    void Start()
    {
        m_ColumnHeadings.Add("순서");
        m_ColumnHeadings.Add("인형 위치");
        m_ColumnHeadings.Add("가는 궤적");
        m_ColumnHeadings.Add("오는 궤적");

        fileNum = GetFileNumber(m_Path);
        Debug.Log("파일 개수 "+fileNum);
        ReadCSV(fileEntries[fileNum-1]); //0번째 파일 불러오기
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            DrawTrace(--printN);
        if (Input.GetKeyDown(KeyCode.X))
            DrawTrace(++printN);
    }

    int GetFileNumber(string targetDirectory) //파일 개수 찾기
    {
        fileEntries = Directory.GetFiles(targetDirectory, "*.csv"); //확장자가 csv인 파일만 찾기

        foreach (string fileName in fileEntries)
        {
            Debug.Log(fileName);
        }

        return fileEntries.Length;
    }

    void ReadCSV(string filePath) //csv 파일 불러오기
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
                if (head == "순서")
                    tempData.cnt = Int32.Parse(str);
                else if (head == "인형 위치")
                {
                    string[] dollStr = str.Split('/');
                    tempData.dollPos.Item1 = Int32.Parse(dollStr[0]);
                    tempData.dollPos.Item2 = Int32.Parse(dollStr[1]);
                }
                else if (head == "가는 궤적")
                {
                    tempData.mousePos1 = new List<(int, int)>();
                    string[] mouseStr = str.Split(' ');
                    foreach (string mStr in mouseStr)
                    {
                        if (mStr.Length <= 1)
                            continue;
                        //if (mStr == "\n" || mStr == " " || mStr == "")
                        //{
                        //    Debug.Log("쉬..불..");
                        //    continue;
                        //}
                        string[] mouseSplit = mStr.Split('/');
                        //foreach (string sttt in mouseSplit)
                        //    Debug.Log(sttt);
                        //Debug.Log("다행쓰..");
                        tempData.mousePos1.Add((Int32.Parse(mouseSplit[0]), Int32.Parse(mouseSplit[1])));
                    }
                }
                else if (head == "오는 궤적")
                {
                    tempData.mousePos2 = new List<(int, int)>();
                    string[] mouseStr = str.Split(' ');
                    foreach (string mStr in mouseStr)
                    {
                        if (mStr.Length <= 1) //함정임 이거
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

        Debug.Log("읽기 끝!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        foreach (DATA data in csvData)
        {
            Debug.Log("순서: " + data.cnt);
            Debug.Log("인형 위치: " + data.dollPos);
            Debug.Log("-------------------------------------Pos1!-------------------------------------");
            foreach (var pos in data.mousePos1)
                Debug.Log(pos);
            Debug.Log("-------------------------------------Pos2!-------------------------------------");
            foreach (var pos in data.mousePos2)
                Debug.Log(pos);
        }
    }

    public void DrawTrace(int cnt) //궤적 그리기
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
