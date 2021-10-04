using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirearmsBullets : MonoBehaviour
{
    public List<Dictionary<string, object>> bulletData;

    [Header("Items")] // 보급품으로 떨어지는 총알
    public int dropPistolBullet;
    public int dropShotgunBullet;
    public int dropRifleBullet;
    public int dropSniperBullet;
    public int dropMinigunBullet;
    [Header("Reloaded")] //현재 장전된 발수
    public int curPistolBullet;
    public int curShotgunBullet;
    public int curRifleBullet;
    public int curSniperBullet;
    public int curMinigunBullet;
   
    public string dataPath;

    public Text reloadedAndCurBulletText;
    public FirearmsData firearmsData;
    public FirearmsController firearmsController;
    void Start()
    {
        
        firearmsData = GetComponent<FirearmsData>();
        firearmsController = GetComponent<FirearmsController>();
        bulletData = CSVReader.Read(dataPath);


        dropPistolBullet = int.Parse(bulletData[0]["DropBullets"].ToString());
        dropShotgunBullet = int.Parse(bulletData[1]["DropBullets"].ToString());
        dropRifleBullet = int.Parse(bulletData[2]["DropBullets"].ToString());
        dropSniperBullet = int.Parse(bulletData[3]["DropBullets"].ToString());
        dropMinigunBullet = int.Parse(bulletData[4]["DropBullets"].ToString());
        
       /* Debug.Log(pistolBullet);
        Debug.Log(shotgunBullet);
        Debug.Log(rifleBullet);
        Debug.Log(sniperBullet);
        Debug.Log(minigunBullet);
*/    }

    // 테스트용 버튼
    public void Taeho_PistolBullets()
    {
        firearmsData.firearms[0].GetComponent<Firearms>().bulletsNum += dropPistolBullet;
        //reloadedAndCurBulletText.text = firearmsData.firearms[0].GetComponent<Firearms>().bulletsNum.ToString();
        //curGuns.curGun = 1;
    }
    public void Taeho_ShotgunBullets()
    {
        firearmsData.firearms[1].GetComponent<Firearms>().bulletsNum += dropShotgunBullet;
        //reloadedAndCurBulletText.text = firearmsData.firearms[1].GetComponent<Firearms>().bulletsNum.ToString();
        //curGuns.curGun = 2;
    }
    public void Taeho_RifleBullets()
    {
        firearmsData.firearms[2].GetComponent<Firearms>().bulletsNum += dropRifleBullet;
        //reloadedAndCurBulletText.text = firearmsData.firearms[2].GetComponent<Firearms>().bulletsNum.ToString();
        //curGuns.curGun = 3;
    }
    public void Taeho_SniperBullets()
    {
        firearmsData.firearms[3].GetComponent<Firearms>().bulletsNum += dropSniperBullet;
        //reloadedAndCurBulletText.text = firearmsData.firearms[3].GetComponent<Firearms>().bulletsNum.ToString();
        //curGuns.curGun = 4;
    }
    public void Taeho_MinigunBullets()
    {
        firearmsData.firearms[4].GetComponent<Firearms>().bulletsNum += dropMinigunBullet;
        //reloadedAndCurBulletText.text = firearmsData.firearms[4].GetComponent<Firearms>().bulletsNum.ToString();
        //curGuns.curGun = 5;
    }
   
}
