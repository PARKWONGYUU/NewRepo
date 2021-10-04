using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirearmsController : MonoBehaviour
{
    public FirearmsBullets firearmsBullets;
    public FirearmsData firearmsData;
    public BulletPooling bulletPooling;
    public bool bang;
    public bool reloading;

    public float curTime;
    public float coolTime;

    public int curGun; //1.권총 , 2.샷건, 3.라이플, 4.스나이퍼, 5.미니건
    public string dataPath = "Firearms";
    public List<Dictionary<string, object>> curGunData;
    //public GameObject weaponTable; 드랍다운(쓸 수 있는 방법 구해봄)
    [Header("curGun")]//현재 들고 있는 총


    public Text noneBulletText;


    void Start()
    {
        //weaponTable = GameObject.FindGameObjectWithTag("Weapon");
        firearmsBullets = GetComponent<FirearmsBullets>();
        firearmsData = GetComponent<FirearmsData>();
        bulletPooling = GetComponent<BulletPooling>();
        curGunData = CSVReader.Read(dataPath);
        //weaponTable.GetComponents<Dropdown>().val
    }
    
    void Update()
    {
        if (curGun == 1)
        {
            
            if (reloading)
            {
                curTime += Time.deltaTime;
                if(curTime > coolTime)
                {
                    for (int i = 0; i < firearmsData.firearms[0].GetComponent<Firearms>().maxloadbullet; i++)
                    {
                        if (firearmsBullets.curPistolBullet == int.Parse(curGunData[0]["MaxLoadBullet"].ToString()))
                        {
                            break;
                        }
                        if (firearmsData.firearms[0].GetComponent<Firearms>().bulletsNum <= 0)
                        {
                            break;
                        }
                        firearmsData.firearms[0].GetComponent<Firearms>().bulletsNum -= 1;
                        firearmsBullets.curPistolBullet += 1;
                    }
                    firearmsBullets.reloadedAndCurBulletText.text = "";
                    curTime = 0;
                    reloading = false;
                    coolTime = float.Parse(curGunData[0]["AttackCoolTime"].ToString());
                }
            }
            
            firearmsBullets.reloadedAndCurBulletText.text = firearmsBullets.curPistolBullet +
            "/" + firearmsData.firearms[0].GetComponent<Firearms>().bulletsNum.ToString();
            curTime += Time.deltaTime;
            if (curTime > coolTime && firearmsBullets.curPistolBullet > 0)
            {
                if(bang)
                {
                    bulletPooling.Taeho_GetQueue();
                    firearmsBullets.curPistolBullet -= 1;
                    curTime = 0;
                    bang = false;
                }
                
            }
            else
            {
                bang = false;
            }
        }
        if (curGun == 2)
        {
            if (reloading)
            {
                curTime += Time.deltaTime;
                if (curTime > coolTime)
                {
                    for (int i = 0; i < firearmsData.firearms[1].GetComponent<Firearms>().maxloadbullet; i++)
                    {
                        if (firearmsBullets.curShotgunBullet == int.Parse(curGunData[1]["MaxLoadBullet"].ToString()))
                        {
                            break;
                        }
                        if (firearmsData.firearms[1].GetComponent<Firearms>().bulletsNum <= 0)
                        {
                            break;
                        }
                        firearmsData.firearms[1].GetComponent<Firearms>().bulletsNum -= 1;
                        firearmsBullets.curShotgunBullet += 1;
                    }
                    firearmsBullets.reloadedAndCurBulletText.text = "";
                    curTime = 0;
                    reloading = false;
                    coolTime = float.Parse(curGunData[1]["AttackCoolTime"].ToString());
                }
            }

            firearmsBullets.reloadedAndCurBulletText.text = firearmsBullets.curShotgunBullet +
            "/" + firearmsData.firearms[1].GetComponent<Firearms>().bulletsNum.ToString();
            curTime += Time.deltaTime;
            if (curTime > coolTime && firearmsBullets.curShotgunBullet > 0)
            {
                if (bang)
                {
                    bulletPooling.Taeho_GetQueue();
                    firearmsBullets.curShotgunBullet -= 1;
                    curTime = 0;
                    bang = false;
                }
            }
            else
            {
                bang = false;
            }
        } 
        if (curGun == 3)
        { 
            if (reloading)
            {
                curTime += Time.deltaTime;
                if (curTime > coolTime)
                {
                    for (int i = 0; i < firearmsData.firearms[2].GetComponent<Firearms>().maxloadbullet; i++)
                    {
                        if (firearmsBullets.curRifleBullet == int.Parse(curGunData[2]["MaxLoadBullet"].ToString()))
                        {
                            break;
                        }
                        if (firearmsData.firearms[2].GetComponent<Firearms>().bulletsNum <= 0)
                        {
                            break;
                        }
                        firearmsData.firearms[2].GetComponent<Firearms>().bulletsNum -= 1;
                        firearmsBullets.curRifleBullet += 1;
                    }
                    firearmsBullets.reloadedAndCurBulletText.text = "";
                    curTime = 0;
                    reloading = false;
                    coolTime = float.Parse(curGunData[2]["AttackCoolTime"].ToString());
                }
            }

            firearmsBullets.reloadedAndCurBulletText.text = firearmsBullets.curRifleBullet +
            "/" + firearmsData.firearms[2].GetComponent<Firearms>().bulletsNum.ToString();
            curTime += Time.deltaTime;
            if (curTime > coolTime && firearmsBullets.curRifleBullet > 0)
            {
                if (bang)
                {
                    bulletPooling.Taeho_GetQueue();
                    firearmsBullets.curRifleBullet -= 1;
                    curTime = 0;
                    bang = false;
                }

            }
            else
            {
                bang = false;
            }
        }
        if (curGun == 4)
        {
            if (reloading)
            {
                curTime += Time.deltaTime;
                if (curTime > coolTime)
                {
                    for (int i = 0; i < firearmsData.firearms[3].GetComponent<Firearms>().maxloadbullet; i++)
                    {
                        if (firearmsBullets.curSniperBullet == int.Parse(curGunData[3]["MaxLoadBullet"].ToString()))
                        {
                            break;
                        }
                        if (firearmsData.firearms[3].GetComponent<Firearms>().bulletsNum <= 0)
                        {
                            break;
                        }
                        firearmsData.firearms[3].GetComponent<Firearms>().bulletsNum -= 1;
                        firearmsBullets.curSniperBullet += 1;
                    }
                    firearmsBullets.reloadedAndCurBulletText.text = "";
                    curTime = 0;
                    reloading = false;
                    coolTime = float.Parse(curGunData[3]["AttackCoolTime"].ToString());
                }
            }

            firearmsBullets.reloadedAndCurBulletText.text = firearmsBullets.curSniperBullet +
            "/" + firearmsData.firearms[3].GetComponent<Firearms>().bulletsNum.ToString();
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                if (bang && firearmsBullets.curSniperBullet > 0)
                {
                    bulletPooling.Taeho_GetQueue();
                    firearmsBullets.curSniperBullet -= 1;
                    curTime = 0;
                    bang = false;
                }

            }
            else
            {
                bang = false;
            }
        } 
        if (curGun == 5)
        {
            if (reloading)
            { 
                curTime += Time.deltaTime;
                if (curTime > coolTime)
                {
                    for (int i = 0; i < firearmsData.firearms[4].GetComponent<Firearms>().maxloadbullet; i++)
                    {
                        if (firearmsBullets.curMinigunBullet == int.Parse(curGunData[4]["MaxLoadBullet"].ToString()))
                        {
                            break;
                        }
                        if (firearmsData.firearms[4].GetComponent<Firearms>().bulletsNum <= 0)
                        {
                            break;
                        }
                        firearmsData.firearms[4].GetComponent<Firearms>().bulletsNum -= 1;
                        firearmsBullets.curMinigunBullet += 1;

                    }
                    firearmsBullets.reloadedAndCurBulletText.text = "";
                    curTime = 0;
                    reloading = false;
                    coolTime = float.Parse(curGunData[4]["AttackCoolTime"].ToString());
                }
            }

            firearmsBullets.reloadedAndCurBulletText.text = firearmsBullets.curMinigunBullet +
            "/" + firearmsData.firearms[4].GetComponent<Firearms>().bulletsNum.ToString();
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                if (bang && firearmsBullets.curMinigunBullet > 0)
                {
                    bulletPooling.Taeho_GetQueue();
                    firearmsBullets.curMinigunBullet -= 1;
                    curTime = 0;
                    bang = false;
                }

            }
            else
            {
                bang = false;
            }
        }
            
    }

    public void Taeho_Bang()
    {
        //테스트용 나중에 아이템마다 숫자를 매겨 공격예정(바뀔수도있음)
        if (firearmsBullets.curPistolBullet > 0) // 6 > 0
        { 
            bang = true;
        }
            
        if (firearmsBullets.curShotgunBullet > 0) // 6 <= 0
            bang = true;
        if (firearmsBullets.curRifleBullet > 0) // 6 <= 0
            bang = true;
        if (firearmsBullets.curSniperBullet > 0) // 6 <= 0
            bang = true;
        if (firearmsBullets.curMinigunBullet > 0) // 6 <= 0
            bang = true;
    }

    public void Taeho_Reload()
    {
        Debug.Log(curGun);
        if (curGun == 1)
        {
            if(reloading == false)
            {
                reloading = true;
                coolTime = float.Parse(curGunData[0]["ReloadSpeed"].ToString());
                noneBulletText.text = "";
            }
        }
        else if (curGun == 2)
        {
            if (reloading == false)
            {
                reloading = true;
                coolTime = float.Parse(curGunData[1]["ReloadSpeed"].ToString());
                noneBulletText.text = "";
            }
        }
        else if (curGun == 3)
        {
            if (reloading == false)
            {
                reloading = true;
                coolTime = float.Parse(curGunData[2]["ReloadSpeed"].ToString());
                noneBulletText.text = "";
            }
        }
        else if (curGun == 4)
        {
            if (reloading == false)
            {
                reloading = true;
                coolTime = float.Parse(curGunData[3]["ReloadSpeed"].ToString());
                noneBulletText.text = "";
            }
        }
        else if (curGun == 5)
        {
            if (reloading == false)
            {
                reloading = true;
                coolTime = float.Parse(curGunData[4]["ReloadSpeed"].ToString());
                noneBulletText.text = "";
            }
        }
        else
        {
            firearmsBullets.reloadedAndCurBulletText.text = "";
        }
    }

    public void Taeho_Pistol()
    {
        coolTime = float.Parse(curGunData[0]["AttackCoolTime"].ToString());
        curTime = coolTime - 0.2f;
        curGun = 1;
    }
    public void Taeho_Shotgun()
    {
        coolTime = float.Parse(curGunData[1]["AttackCoolTime"].ToString());
        curTime = coolTime - 0.8f;
        curGun = 2;
    }
    public void Taeho_Rifle()
    {
        coolTime = float.Parse(curGunData[2]["AttackCoolTime"].ToString());
        curTime = coolTime - 0.5f;
        curGun = 3;

    }
    public void Taeho_Sniper()
    {
        coolTime = float.Parse(curGunData[3]["AttackCoolTime"].ToString());
        curTime = coolTime - 1f;
        curGun = 4;
    }
    public void Taeho_Minigun()
    {
        coolTime = float.Parse(curGunData[4]["AttackCoolTime"].ToString());
        curTime = coolTime - 1.5f;
        curGun = 5;
    }
    public void Taeho_TestCurGun()
    {
        Debug.Log("dhsnmfg");
        Debug.Log(curGun);
        curGun = -1;
        firearmsBullets.reloadedAndCurBulletText.text = "";
    }
}
