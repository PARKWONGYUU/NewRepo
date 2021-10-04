using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UITimeStamp1 : MonoBehaviour
{
    public bool night;
    public bool afternoon;
    public float oneDay;
    public bool am;
    public bool pm;
    public bool ampm;
    public int hour = 10;
    public float test;
    public TextMeshProUGUI TimeText;
    // public GameObject nightImage;
    public GameObject afternoonImage;
    void Start()
    {


    }

    void Update()
    {
        if (ampm)
        {
            am = true;
            pm = false;
        }
        else
        {
            pm = true;
            am = false;
        }

        test += Time.deltaTime;
        oneDay += Time.deltaTime * 1.5f;
        if (oneDay > 60f)
        {

            hour += 1;

            oneDay = 0;
        }
        if (hour >= 12)
        {
            if (am)
            {
                ampm = false;
                hour = 1;
            }
            else
            {
                ampm = true;
                hour = 1;
            }

            hour = 0;
        }


        if (pm && hour < 10)
        {
            afternoon = true;
            night = false;
        }
        else if (am && hour > 7)
        {
            afternoon = true;
            night = false;
        }
        else if (am && hour <= 7)
        {
            if (hour == 7 && oneDay <= 30)
            {
                night = true;
                afternoon = false;
            }
            else if(hour == 7 && oneDay > 30)
            {
                afternoon = true;
                night = false;
            }
            else
            {
                night = true;
                afternoon = false;
            }

        }
        if (pm && hour >= 10)
        {
            night = true;
            afternoon = false;
        }


        //if(night)
        //{
        //    nightImage.SetActive(true);
        //    afternoonImage.SetActive(false);
        //}
        //if (afternoon)
        //{
        //    nightImage.SetActive(false);
        //    afternoonImage.SetActive(true);
        //}
        if (oneDay > 10)
        {

            if (am)
            {
                TimeText.text = "AM " + hour + ":" + Mathf.Floor(oneDay).ToString();
            }
            if (pm)
            {
                TimeText.text = "PM " + hour + ":" + Mathf.Floor(oneDay).ToString();
            }
        }
        if (oneDay < 10)
        {
            if (am)
            {

                TimeText.text = "AM " + hour + ":0" + Mathf.Floor(oneDay).ToString();
            }
            if (pm)
            {
                TimeText.text = "PM " + hour + ":0" + Mathf.Floor(oneDay).ToString();
            }
        }

        
        
    }
public void TEst()
    {
        Debug.Log("´­¸²!");
    }
}
