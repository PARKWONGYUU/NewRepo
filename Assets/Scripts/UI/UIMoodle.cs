using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoodle : MonoBehaviour
{
    public WG_Player player;
    public List<GameObject> moodles;
    public List<GameObject> moodleImages;
    public List<GameObject> moodleExpress;
    public GameObject temp;
    public GameObject noneImage;
    public int moodleCnt;

    //����Ʈ ��ȯ(�׽�Ʈ WG_Player���� md_? �� ����)
    public bool hungry;
    public bool thirsty;
    public bool tired;
    public bool exhaust;
    public bool panic;
    public bool bleending;
    public bool infection;
    public bool heaviness;
    void Start()
    {
        for (int i = 0; i < moodleExpress.Count; i++)
        {
            moodleExpress[i].SetActive(false);
        }
        player = GameObject.Find("Player").GetComponent<WG_Player>();
    }

    void Update()
    {
         
        if(player.md_Hungry)
        {
            if (hungry)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[0]);
                hungry = false;
                moodleCnt++;
            }
            


        }
        if(player.md_Thirsty)
        {
            if (thirsty)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[1]);
                thirsty = false;
                moodleCnt++;
            }
            
        }
        if(player.md_Tired)
        {
            if (tired)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[2]);
                tired = false;
                moodleCnt++;
            }
            
        }
        if(player.md_Exhaust)
        {
            if (exhaust)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[3]);
                exhaust = false;
                moodleCnt++;
            }
        }
        if(player.md_Panic)
        {
            if (panic)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[4]);
                panic = false;
                moodleCnt++;
            }
        }
        if(player.md_bleeding)
        {
            if (bleending)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[5]);
                bleending = false;
                moodleCnt++;
            }
        }
        if(player.md_infection)
        {
            if (infection)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[6]);
                infection = false;
                moodleCnt++;
            }
        }
        if(player.md_heaviness)
        {
            if (heaviness)
            {
                moodleExpress[moodleCnt].SetActive(true);
                moodles.Add(moodleImages[7]);
                heaviness = false;
                moodleCnt++;
            }
        }
        // ������ �������� ����Ʈ ���� ���鰹�� ����
        if (hungry == false && player.md_Hungry == false)
        {   
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages1")
                {
                    Debug.Log("����� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    hungry = true;
                }
            }
           
        }
        if (thirsty == false && player.md_Thirsty == false)
        {
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages2")
                {
                    Debug.Log("���� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    thirsty = true;
                }
            }
            
        }
       
        if(tired == false && player.md_Tired == false)
        {
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages3")
                {
                    Debug.Log("����� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    tired = true;
                }
            }
        }
        if(exhaust == false && player.md_Exhaust == false)
        {
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages4")
                {
                    Debug.Log("Ż�� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    exhaust = true;
                }
            }
        }
        if(panic == false && player.md_Panic == false)
        {
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages5")
                {
                    Debug.Log("����� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    panic = true;
                }
            }
        }
        if(bleending == false && player.md_bleeding == false)
        {
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages6")
                {
                    Debug.Log("����� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    bleending = true;
                }
            }
        }
        if(infection == false && player.md_infection == false)
        {
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages7")
                {
                    Debug.Log("����� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    infection = true;
                }
            }
        }
        if(heaviness == false && player.md_heaviness == false)
        {
            for (int i = 0; i < moodleCnt; i++)
            {
                if (moodles[i].gameObject.name == "MoodleImages8")
                {
                    Debug.Log("���ſ� ����");
                    moodles.Remove(moodles[i]);
                    moodleCnt--;
                    heaviness = true;
                }
            }
        }

        //���鰳���� 0�� �ƴҶ�
        if(moodleCnt != 0)
        {
            int MC = moodleCnt;
            //���鰳����ŭ ��������Ʈ�� �ٲ��ش�.
            for (int i = 0; i < moodleCnt; i++)
            {
                moodleExpress[i].GetComponent<Image>().sprite = moodles[i].GetComponent<Image>().sprite;
               
            }
            for (int i = 0; i < moodleExpress.Count; i++)
            {
                if(MC <= i) //���鰳���� �Ѿ�� ex)3�ΰ�� 3���� Ű�� false
                {
                    moodleExpress[i].SetActive(false);
                }
            }
        }
        else
        {
            //���鰹���� 0�� �� �� ����� ��Ȱ
            for (int i = 0; i < moodleExpress.Count; i++)
            {
                moodleExpress[i].SetActive(false);
            }
        }
    }
}
