using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerEquipment : MonoBehaviour
{
    public WG_Player player;
    public List<GameObject> weaponEquips;
    public List<GameObject> usableWeapons;
    public GameObject weapon;
    public GameObject cap;
    public GameObject shoes;
    public GameObject pants;
    public GameObject shirts;
    public GameObject itemMgr;
    public int mainItemNum;
    public int subItemNum;
    public FireController fireCont;

    public void WG_WeaponEquipment()
    {
        if (weapon != null)
        {
            // 무기 분류
            if (weapon.GetComponent<OneHandedWeapons>() != null)
            {
                mainItemNum = 2;
                subItemNum = weapon.GetComponent<OneHandedWeapons>().subItemNum;
            }
            else if (weapon.GetComponent<TwoHandedWeapons>() != null)
            {
                mainItemNum = 3;
                subItemNum = weapon.GetComponent<TwoHandedWeapons>().subItemNum;
            }
            else if (weapon.GetComponent<Firearms>() != null)
            {
                mainItemNum = 4;
                subItemNum = weapon.GetComponent<Firearms>().subItemNum;
            }
            else if (weapon.GetComponent<Grenades>() != null)
            {
                mainItemNum = 5;
                subItemNum = weapon.GetComponent<Grenades>().subItemNum;
            }
            WeaponSetActive();
        }
    }
    void WeaponSetActive()
    {
        for (int i = 0; i < weaponEquips.Count; i++)
        {
            if(weaponEquips[i].activeInHierarchy)
            {
                weaponEquips[i].SetActive(false);
            }
            if (weaponEquips[i].GetComponent<OneHandedWeapons>() != null && weaponEquips[i].GetComponent<OneHandedWeapons>().mainItemNumm == mainItemNum && weaponEquips[i].GetComponent<OneHandedWeapons>().subItemNum == subItemNum)
            {
                weaponEquips[i].SetActive(true);
                weapon = weaponEquips[i];
                player.isTwoHanded = false;
                player.isOneHanded = true;
                player.isGrenade = false;
                player.isShoot = false;
                player.noWeapon = false;
                player.attackRange.transform.localScale = new Vector3(weapon.GetComponent<OneHandedWeapons>().attackRange, 1, weapon.GetComponent<OneHandedWeapons>().attackRange);
            }
            else if (weaponEquips[i].GetComponent<TwoHandedWeapons>() != null && weaponEquips[i].GetComponent<TwoHandedWeapons>().mainItemNum == mainItemNum && weaponEquips[i].GetComponent<TwoHandedWeapons>().subItemNum == subItemNum)
            {
                weaponEquips[i].SetActive(true);
                weapon = weaponEquips[i];
                player.isTwoHanded = true;
                player.isOneHanded = false;
                player.isGrenade = false;
                player.isShoot = false;
                player.noWeapon = false;
                player.attackRange.transform.localScale = new Vector3(weapon.GetComponent<TwoHandedWeapons>().attackRange, 1, weapon.GetComponent<TwoHandedWeapons>().attackRange);
            }
            else if (weaponEquips[i].GetComponent<Firearms>() != null && weaponEquips[i].GetComponent<Firearms>().mainItemNum == mainItemNum && weaponEquips[i].GetComponent<Firearms>().subItemNum == subItemNum)
            {
                weaponEquips[i].SetActive(true);
                weapon = weaponEquips[i];
                player.isTwoHanded = false;
                player.isOneHanded = false;
                player.isGrenade = false;
                player.isShoot = true;
                player.noWeapon = false;
                fireCont.weaponNum = subItemNum;
                player.playerAnim.SetInteger("GUNNUM", subItemNum);
                fireCont.weaponInfo = weaponEquips[i].GetComponent<Firearms>();
                fireCont.curBullet = fireCont.weaponInfo.maxloadbullet;
                fireCont.maxBullet = fireCont.weaponInfo.maxloadbullet;
                if(subItemNum == 0 || subItemNum == 2)
                {
                    player.playerAnim.SetFloat("attackSpeed", 1);
                    fireCont.coolTime = 0.5f;
                }
                else if(subItemNum == 1)
                {
                    player.playerAnim.SetFloat("attackSpeed", 0.5f);
                    fireCont.coolTime = 1f;
                }
                else if(subItemNum == 3)
                {
                    player.playerAnim.SetFloat("attackSpeed", 0.5f);
                    fireCont.coolTime = 1f;
                }
                else if(subItemNum == 4)
                {
                    player.playerAnim.SetFloat("attackSpeed", 2);
                    fireCont.coolTime = 0.25f;
                }
            }
            else if (weaponEquips[i].GetComponent<Grenades>() != null && weaponEquips[i].GetComponent<Grenades>().mainItemNum == mainItemNum && weaponEquips[i].GetComponent<Grenades>().subItemNum == subItemNum)
            {
                weaponEquips[i].SetActive(true);
                weapon = weaponEquips[i];
                player.isTwoHanded = false;
                player.isOneHanded = false;
                player.isGrenade = true;
                player.isShoot = false;
                player.noWeapon = false;
            }
        }
    }

    public void TakeOffEquip()
    {
        if(weapon != null)
        {
            weapon.SetActive(false);
            weapon = null;
            player.isOneHanded = false;
            player.isTwoHanded = false;
            player.isShoot = false;
            player.isGrenade = false;
            player.noWeapon = true;
            player.attackRange.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
