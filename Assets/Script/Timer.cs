using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

	public bool timerOn = false;
	
	public float totalTime = 0f;

	private int minute = 0;
	private int second = 0;
	private int tic = 0;
	public float alltime = 0;
	public float average = 0;

	// Update is called once per frame
	void Update()
	{
		if (timerOn)
		{
			totalTime += Time.deltaTime;
		}
		this.GetComponent<Text>().text = "시간 : " + TimerCalc();
	}

	private string TimerCalc()
	{
		tic = (int)((totalTime % 1) * 100);

		second = (int)totalTime % 60;

		minute = (int)totalTime / 60;

		return minute + " : " + second + " : " + tic;
	}

	public void AddAll()
	{

	  alltime += totalTime;
		average = alltime / 5;

	}
	public void TimerReset()
    {
		totalTime = 0;
    }

	public float GetTime()
	{
		return totalTime;
	}
}