using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medical : MonoBehaviour
{
    public bool md_bleeding;
    public bool md_Penic;
    public bool bandage = false;
    public bool gevorin = false;
    public bool hot6 = false;
    public bool sedative = false;
    public GameObject popupUseCancel;
    public Text CancelItem;

    void BandageBtn()
    {
        bandage = true;
        popupUseCancel.SetActive(true);
    }

    void GevorinBtn()
    {
        gevorin = true;
        popupUseCancel.SetActive(true);

    }

    void Hot6Btn()
    {
        hot6 = true;
        popupUseCancel.SetActive(true);
    }

    void SedativeBtn()
    {
        sedative = true;
        popupUseCancel.SetActive(true);
    }

    void UseBtn()
    {
        if(bandage)
        {
            if(md_bleeding)
            {
                //출혈을 막아준다
                popupUseCancel.SetActive(false);
            }
            else
            {
                //출혈 안걸렸으니깐 쓰지마세요
                //CancelItem.text = "현재 출혈 상태가 아닙니다.";
                popupUseCancel.SetActive(false);
            }
        }

        if (gevorin)
        {
           //감염확률 줄여줌
        }
        if (hot6)
        {
            //피로 좀 회복해줌
        }
        if(sedative)
        {
            if (md_bleeding)
            {
                //출혈을 막아준다
                popupUseCancel.SetActive(false);
            }
            else
            {
                //패닉 안걸렸으니깐 쓰지마세요
                CancelItem.text = "현재 패닉 상태가 아닙니다.";
                popupUseCancel.SetActive(false);
            }
        }
    }

    void CancelBtn()
    {
        popupUseCancel.SetActive(false);
    }
}
