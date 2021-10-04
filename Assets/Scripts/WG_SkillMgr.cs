using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_SkillMgr : MonoBehaviour
{
    public bool isStrExp;
    public bool isHealthExp;
    public bool isAgillityExp;
    public bool isSecurityExp;
    public bool isCarpentryExp;
    public bool isTailoringExp;
    public bool isCookExp;
    public bool isGatheringExp;
    public bool isAimExp;
    public bool isReloadExp;
    public bool isOneHandExp;
    public bool isTwoHandExp;
    public bool isWpMgrExp;

    public float strExp;
    public float healthExp;
    public float agillityExp;
    public float securityExp;
    public float carpentryExp;
    public float tailoringExp;
    public float cookExp;
    public float gatheringExp;
    public float aimExp;
    public float reloadExp;
    public float oneHandExp;
    public float twoHandExp;
    public float wpMgrExp;

    public WG_Player player;
    void Start()
    {

    }


    void Update()
    {
        if ((player.isRun && player.md_heaviness) || player.isExercise)
        {
            isStrExp = true;
        }
        else
        {
            isStrExp = false;
        }
        if (isStrExp)
        {
            strExp += Time.deltaTime;
            if (strExp >= 100 * player.playerClass.str)
            {
                strExp = 0;
                player.playerClass.str++;
            }
        } // 힘 레벨업 조건 (운동 추가 필요)
        if ((player.isAttack && player.isOneHanded) || (player.isAttack && player.isTwoHanded) || (player.isAttack && player.noWeapon) || player.isAttacked || player.isExercise)
        {
            isHealthExp = true;
        }
        else
        {
            isHealthExp = false;
        }
        if (isHealthExp)
        {
            healthExp += Time.deltaTime;
            if (healthExp >= 100 * player.playerClass.health)
            {
                healthExp = 0;
                player.playerClass.health++;
            }
        } //체력 레벨업 조건 (운동 추가 필요)
        if (player.isRun)
        {
            isAgillityExp = true;
        }
        else
        {
            isAgillityExp = false;
        }
        if (isAgillityExp)
        {
            agillityExp += Time.deltaTime;
            if (agillityExp >= 100 * player.playerClass.agillity)
            {
                agillityExp = 0;
                player.playerClass.agillity++;
            }
        } //날렵함 레벨업 조건
        if (player.isCrouch)
        {
            isSecurityExp = true;
        }
        else
        {
            isSecurityExp = false;
        }
        if (isSecurityExp)
        {
            securityExp += Time.deltaTime;
            if (securityExp >= 100 * player.playerClass.security)
            {
                securityExp = 0;
                player.playerClass.security++;
            }
        } //은밀함 레벨업 조건
        if (player.isCarpentry)
        {
            isCarpentryExp = true;
        }
        else
        {
            isCarpentryExp = false;
        }
        if (isCarpentryExp)
        {
            carpentryExp += Time.deltaTime;
            if (carpentryExp >= 100 * player.playerClass.carpentry)
            {
                carpentryExp = 0;
                player.playerClass.carpentry++;
            }
        }
        if (player.isTailoring)
        {
            isTailoringExp = true;
        }
        else
        {
            isTailoringExp = false;
        }
        if (isTailoringExp)
        {
            tailoringExp += Time.deltaTime;
            if (tailoringExp >= 100 * player.playerClass.tailoring)
            {
                tailoringExp = 0;
                player.playerClass.tailoring++;
            }
        }
        if (player.isCook)
        {
            isCookExp = true;
        }
        else
        {
            isCookExp = false;
        }
        if (isCookExp)
        {
            cookExp += Time.deltaTime;
            if (cookExp >= 100 * player.playerClass.cook)
            {
                cookExp = 0;
                player.playerClass.cook++;
            }
        }
        if (player.isGathering)
        {
            isGatheringExp = true;
        }
        else
        {
            isGatheringExp = false;
        }
        if (isGatheringExp)
        {
            gatheringExp += Time.deltaTime;
            if (gatheringExp >= 100 * player.playerClass.gathering)
            {
                gatheringExp = 0;
                player.playerClass.gathering++;
            }
        }
        if (player.isShoot && player.isAttack)
        {
            isAimExp = true;
        }
        else
        {
            isAimExp = false;
        }
        if (isAimExp)
        {
            aimExp += Time.deltaTime;
            if (aimExp >= 100 * player.playerClass.aim)
            {
                aimExp = 0;
                player.playerClass.aim++;
            }
        }
        if (player.isReload)
        {
            isReloadExp = true;
        }
        else
        {
            isReloadExp = false;
        }
        if (isReloadExp)
        {
            reloadExp += Time.deltaTime;
            if (reloadExp >= 100 * player.playerClass.reload)
            {
                reloadExp = 0;
                player.playerClass.reload++;
            }
        }
    }
}
