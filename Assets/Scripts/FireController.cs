using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject shotgunBullet;
    public BulletPooling bulletMaker;
    public WG_Player player;
    public WG_PlayerEquipment weaponKindOf;
    public Firearms weaponInfo;
    public int weaponNum;

    public int curBullet;
    public int maxBullet;

    public float curTime;
    public float coolTime = 0.5f;
    public float shotgunCurTime;
    public float shotgunCoolTime = 0.5f;
    void Start()
    {
        bulletMaker = GameObject.Find("ItemManager").GetComponent<BulletPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.playerEndurance < 2 - player.playerClass.health / 10)
        {
            FireUp();
        }
        if (player.isShoot && player.isAttack && !player.isAttackCoolTime && curBullet > 0 && player.playerEndurance > 2 - player.playerClass.health / 10 && weaponNum != 1)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                bulletMaker.Taeho_GetQueue();
                curBullet--;
                curTime = 0;
                player.playerEndurance -= 2 - player.playerClass.health / 10;
            }
        } //¼¦°Ç Á¦¿Ü
        if (player.isShoot && player.isAttack && !player.isAttackCoolTime && curBullet > 0 && player.playerEndurance > 2 - player.playerClass.health / 10 && weaponNum == 1)
        {
            curTime += Time.deltaTime;
            if (curTime > coolTime)
            {
                shotgunBullet.SetActive(true);
                curTime = 0;
                player.playerEndurance -= 2 - player.playerClass.health / 10;
            }
        } // ¼¦°Ç
        if(shotgunBullet.activeInHierarchy)
        {
            shotgunCurTime += Time.deltaTime;
            if(shotgunCurTime > shotgunCoolTime)
            {
                shotgunCurTime = 0;
                shotgunBullet.SetActive(false);
                curBullet--;
            }
        }
        if (curBullet == 0)
        {
            FireUp();
        }
    }

    public void Reload()
    {
        if (curBullet < maxBullet && 3 - (player.playerClass.health / 10) - player.playerClass.reload / 10 < player.playerEndurance)
        {
            weaponInfo.bulletsNum -= maxBullet - curBullet;
            curBullet = maxBullet;
            player.isReload = true;
            player.playerAnim.SetBool("ISRELOAD", true);
            player.playerEndurance -= 3 - (player.playerClass.health / 10) - player.playerClass.reload / 10; 
        }
    }
    public void FireUp()
    {
        if(weaponNum == 0 || weaponNum == 2)
        {
            curTime = 0.4f;
        }
        else if(weaponNum == 1)
        {
            curTime = 0.7f;
        }
        else if (weaponNum == 3)
        {
            curTime = 0.7f;
        }
        else if(weaponNum == 4)
        {
            curTime = 0.2f;
        }
        player.playerAnim.SetBool("ISSHOOT", false);
    }
}
