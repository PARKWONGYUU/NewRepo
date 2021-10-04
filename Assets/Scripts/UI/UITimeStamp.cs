using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeStamp : MonoBehaviour
{
    public bool night;
    public bool afternoon;
    public float oneDay;
    public int hour = 10;
    public float test;
    public Text TimeText;
    public GameObject nightImage;
    public GameObject afternoonImage;
    void Start()
    {
        
    }

    void Update()
    {
        test += Time.deltaTime;
        oneDay += Time.deltaTime*2.8f;
        if(oneDay > 60f)
        {
            hour += 1;
            oneDay = 0;
        }
        if(hour >= 7 && hour <=  22) // 7~22
        {
            afternoonImage.SetActive(true);
            nightImage.SetActive(false);
            afternoon = true;
            night = false;
        }
        if(hour >= 22) //22~6
        {
            nightImage.SetActive(true);
            afternoonImage.SetActive(false);
            night = true;
            afternoon = false;
        }
        if(hour >= 24)
        {
            hour = 0;
        }
        if(oneDay > 10)
        {
            TimeText.text = hour + ":" + Mathf.Floor(oneDay).ToString();
        }
        if(oneDay < 10)
        {
            TimeText.text = hour + ":0" + Mathf.Floor(oneDay).ToString(); 
        }
          
    }

    public void TEst()
    {
        Debug.Log("´­¸²!");
    }
}
