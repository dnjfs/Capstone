using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CSVReadManager : MonoBehaviour
{
    public GameObject ToDoll; //빨간점
    public GameObject ToFlag; //파란점

    int curFile;
    int fileNum; //총 파일 개수
    string[] fileEntries; //파일명 배열
    List<string> m_ColumnHeadings = new List<string>(); //열 이름
    string m_Path = Application.streamingAssetsPath; //csv 파일 저장된 경로
    List<DATA> csvData = new List<DATA>(); //모든 데이터

    int DollNum; //인형 개수
    int printN = 0; //출력할 궤적 번호
    float averMoveTime; //평균 이동시간
    float averErrorAngle; //평균 오차각도

    public struct DATA
    {
        public int cnt; //순서
        public  (int, int) dollPos; //인형 위치
        public List<(int, int)> mousePos1, mousePos2; //가는 궤적, 오는 궤적
        public float moveTime; //이동시간
        public float errorAngle; //오차각도
    };


    void Start()
    {
        m_ColumnHeadings.Add("순서");
        m_ColumnHeadings.Add("인형 위치");
        m_ColumnHeadings.Add("가는 궤적");
        m_ColumnHeadings.Add("오는 궤적");
        m_ColumnHeadings.Add("이동시간");
        m_ColumnHeadings.Add("오차각도");

        fileNum = GetFileNumber(m_Path);
        Debug.Log("파일 개수 "+fileNum);
        curFile = fileNum - 1;
        ReadCSV(fileEntries[curFile]); //가장 최근 파일 불러오기
    }

    void Update()
    {
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
        csvData = new List<DATA>(); //데이터 리스트 초기화
        DollNum = 0;
        averMoveTime = 0;
        averErrorAngle = 0;

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

            DollNum++; //인형 개수 증가
            DATA tempData = new DATA();
            for (int j = 0; j < row.Length; j++)
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
                else if (head == "이동시간")
                {
                    tempData.moveTime = float.Parse(str);
                    averMoveTime += tempData.moveTime;
                }
                else if (head == "오차각도")
                {
                    tempData.errorAngle = float.Parse(str);
                    averErrorAngle += tempData.errorAngle;
                }
            }

            csvData.Add(tempData);
            //Debug.Log("--------------------------------");
            //foreach (var str in row)
            //    Debug.Log(str);
        }

        averMoveTime /= DollNum;
        averErrorAngle /= DollNum;
        ViewResult();
        //Debug.Log("읽기 끝!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        //foreach (DATA data in csvData)
        //{
        //    Debug.Log("순서: " + data.cnt);
        //    Debug.Log("인형 위치: " + data.dollPos);
        //    Debug.Log("-------------------------------------Pos1!-------------------------------------");
        //    foreach (var pos in data.mousePos1)
        //        Debug.Log(pos);
        //    Debug.Log("-------------------------------------Pos2!-------------------------------------");
        //    foreach (var pos in data.mousePos2)
        //        Debug.Log(pos);
        //    Debug.Log("이동시간: " + data.moveTime);
        //    Debug.Log("오차각도: " + data.errorAngle);
        //}
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

    public Text TFileName;
    public Text TAverageTime;
    public Text TAverageErrorAngle;
    public Text TDollNum;
    public Text TMoveTime;
    public Text TErrorAngle;
    public void ViewResult()
    {
        string fName = fileEntries[curFile];
        fName = fName.Replace(m_Path+ @"\", "");
        fName = fName.Replace(".csv", "");
        fName = 
        TFileName.text = fName;
        Debug.Log(fName);
        TAverageTime.text = "평균시간: " + TimerToString(averMoveTime);
        TAverageErrorAngle.text = "평균오차: " + ((int)((averErrorAngle) * 10) / 10f).ToString() + "도";

        printN = 0;
        ChangePrintN();
    }
    private void ChangePrintN()
    {
        TDollNum.text = (printN+1).ToString()+"번째 인형";
        TMoveTime.text = "시간: "+TimerToString(csvData[printN].moveTime);
        TErrorAngle.text = ((int)((csvData[printN].errorAngle)*10)/10f).ToString()+"도";
        DrawTrace(printN);
    }

    private string TimerToString(float totalTime)
    {
        //int minute = (int)totalTime / 60;
        int second = (int)totalTime;
        int tic = (int)((totalTime % 1) * 100);

        return second + " : " + tic;
    }

    public void ClickLeft()
    {
        if (printN > 0)
        {
            printN--;
            ChangePrintN();
        }
    }
    public void ClickRight()
    {
        if (printN < DollNum-1)
        {
           printN++;
            ChangePrintN();
        }
    }
    public void ClickUp()
    {
        if (curFile > 0)
            ReadCSV(fileEntries[--curFile]);
    }
    public void ClickDown()
    {
        if (curFile < fileNum-1)
            ReadCSV(fileEntries[++curFile]);
    }
}
