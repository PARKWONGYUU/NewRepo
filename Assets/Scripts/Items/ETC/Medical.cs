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
                //������ �����ش�
                popupUseCancel.SetActive(false);
            }
            else
            {
                //���� �Ȱɷ����ϱ� ����������
                //CancelItem.text = "���� ���� ���°� �ƴմϴ�.";
                popupUseCancel.SetActive(false);
            }
        }

        if (gevorin)
        {
           //����Ȯ�� �ٿ���
        }
        if (hot6)
        {
            //�Ƿ� �� ȸ������
        }
        if(sedative)
        {
            if (md_bleeding)
            {
                //������ �����ش�
                popupUseCancel.SetActive(false);
            }
            else
            {
                //�д� �Ȱɷ����ϱ� ����������
                CancelItem.text = "���� �д� ���°� �ƴմϴ�.";
                popupUseCancel.SetActive(false);
            }
        }
    }

    void CancelBtn()
    {
        popupUseCancel.SetActive(false);
    }
}
