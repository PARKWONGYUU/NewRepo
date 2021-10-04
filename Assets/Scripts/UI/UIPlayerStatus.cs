using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : MonoBehaviour
{
    public WG_Player player;
    public GameObject playerInfo;
    public GameObject playerStatus;
    public GameObject playerSkill;
    public Text playerName;
    public Text playerClassName;
    public Text hp;
    public Text endurance;
    public Text hungry;
    public Text thirsty;
    public Text tired;
    public Text str;
    public Text health;
    public Text agillity;
    public Text security;
    public Text carpentry;
    public Text tailoring;
    public Text cook;
    public Text gathering;
    public Text aim;
    public Text reload;
    public Text oneHandedExpert;
    public Text twoHandedExpert;
    public Text weaponRepair;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<WG_Player>();
        playerInfo = GameObject.Find("PlayerStatus");
        playerStatus = playerInfo.transform.GetChild(1).gameObject;
        playerSkill = playerInfo.transform.GetChild(0).gameObject;
        playerName = GameObject.Find("PlayerName").GetComponent<Text>();
        playerClassName = GameObject.Find("PlayerClass").GetComponent<Text>();
        hp = GameObject.Find("HP").GetComponent<Text>();
        endurance = GameObject.Find("Endurance").GetComponent<Text>();
        hungry = GameObject.Find("Hungry").GetComponent<Text>();
        thirsty = GameObject.Find("Thirsty").GetComponent<Text>();
        tired = GameObject.Find("Tired").GetComponent<Text>();
        str = GameObject.Find("STR").GetComponent<Text>();
        health = GameObject.Find("Health").GetComponent<Text>();
        agillity = GameObject.Find("Agillity").GetComponent<Text>();
        security = GameObject.Find("Security").GetComponent<Text>();
        carpentry = GameObject.Find("Carpentry").GetComponent<Text>();
        tailoring = GameObject.Find("Tailoring").GetComponent<Text>();
        cook = GameObject.Find("Cook").GetComponent<Text>();
        gathering = GameObject.Find("Gathering").GetComponent<Text>();
        aim = GameObject.Find("Aim").GetComponent<Text>();
        reload = GameObject.Find("Reload").GetComponent<Text>();
        oneHandedExpert = GameObject.Find("OneHandedExpert").GetComponent<Text>();
        twoHandedExpert = GameObject.Find("TwoHandedExpert").GetComponent<Text>();
        weaponRepair = GameObject.Find("WeaponRepairExpert").GetComponent<Text>();
        playerInfo.SetActive(false);
    }


    void Update()
    {
        playerName.text = "이름은 구글연동?";
        playerClassName.text = "Class : " + player.playerClass.className;
        hp.text = ((int)player.playerHP).ToString() + " / " + player.playerMaxHP.ToString();
        endurance.text = ((int)player.playerEndurance).ToString() + " / " + player.playerMaxEndurance.ToString();
        hungry.text = player.playerHungry.ToString() + " / " + player.playerMaxHungry.ToString();
        thirsty.text = player.playerThirsty.ToString() + " / " + player.playerMaxThirsty.ToString();
        tired.text = player.playerTired.ToString() + " / " + player.playerMaxTired.ToString();
        str.text = player.playerClass.str.ToString() + "Lv";
        health.text = player.playerClass.health.ToString() + "Lv";
        agillity.text = player.playerClass.agillity.ToString() + "Lv";
        security.text = player.playerClass.security.ToString() + "Lv";
        carpentry.text = player.playerClass.carpentry.ToString() + "Lv";
        tailoring.text = player.playerClass.tailoring.ToString() + "Lv";
        cook.text = player.playerClass.cook.ToString() + "Lv";
        gathering.text = player.playerClass.gathering.ToString() + "Lv";
        aim.text = player.playerClass.aim.ToString() + "Lv";
        reload.text = player.playerClass.reload.ToString() + "Lv";
        oneHandedExpert.text = player.playerClass.onehandExp.ToString() + "Lv";
        twoHandedExpert.text = player.playerClass.twohandExp.ToString() + "Lv";
        weaponRepair.text = player.playerClass.weaponManagement.ToString() + "Lv";









    }

    public void PlayerInfoButton()
    {
        if (playerInfo.activeInHierarchy)
        {
            playerInfo.SetActive(false);
        }
        else
        {
            playerInfo.SetActive(true);
        }
    }
    public void SkillButton()
    {
        playerSkill.SetActive(true);
        playerStatus.SetActive(false);
    }
    public void StatusButton()
    {
        playerSkill.SetActive(false);
        playerStatus.SetActive(true);
    }
}
