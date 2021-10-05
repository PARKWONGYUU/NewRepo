using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WG_PlayerClass
{
    public string className;
    public float str;
    public float health;
    public float agillity;
    public float security;
    public float carpentry;
    public float tailoring;
    public float cook;
    public float gathering;
    public float aim;
    public float reload;
    public float onehandExp;
    public float twohandExp;
    public float weaponManagement;
}

public class WG_Player : MonoBehaviour
{      
    //매니저
    public GameObject itemMgr;
    public WG_PlayerEquipment playerEq;
    public Inventory_Manager invenMgr;
    //플레이어 스테이터스
    public float playerMaxHP;
    public float playerMaxThirsty;
    public float playerMaxHungry;
    public float playerMaxTired;
    public float playerMaxEndurance;
    public float playerMaxWeight;

    public float playerHP;
    public float playerThirsty;
    public float playerHungry;
    public float playerTired;
    public float playerEndurance;
    public float playerWeight;

    public float playerEnduranceRecorvery;
    public float crouchSpeed;
    public float runSpeed;
    public float walkSpeed;
    public GameObject playerRecognitionRanage;
    public GameObject classSelectUI;

    //플레이어 무들

    public bool md_Hungry;
    public bool md_Thirsty;
    public bool md_Tired;
    public bool md_Exhaust;
    public bool md_Panic;
    public bool md_bleeding;
    public bool md_infection;
    public bool md_heaviness;

    //플레이어 동작
    public Animator playerAnim;
    WG_PlayerMoveController mvCon;
    public WG_SimpleTouchController touchMgr;
    public GameObject attackRange;
    public enum PLAYERSTATE
    {
        IDLE,
        WALK,
        RUN,
        ACTION,
        DEAD
    }
    public PLAYERSTATE playerState;
    public bool isWalk;
    public bool isCrouch;
    public bool isRun;
    public bool isAttack;
    public bool isAttackCoolTime;
    public bool isAttacked;
    public bool noWeapon = true;
    public bool isOneHanded;
    public bool isTwoHanded;
    public bool isShoot;
    public bool isGrenade;
    public bool isReload;
    public bool isExercise;
    public bool isCarpentry;
    public bool isTailoring;
    public bool isCook;
    public bool isGathering;
    public bool isTrowing;
    public bool isDead;
    public float attackCurTime;
    public float attackCoolTime = 1f;
    public float attackCoolCurTime;
    public float noWeaponActionTime = 1.2f;
    public float onehandedActionTime = 1.4f;
    public float twohandedActionTime = 1.633f;
    public float shootActionTime = 0.5f;
    public float grenadeActionTime;
    public FireController fireCont;
    //플레이어 직업
    public WG_PlayerClass playerClass;

    void Start()
    {
        mvCon = GetComponent<WG_PlayerMoveController>();
        playerRecognitionRanage = GameObject.Find("PlayerRange");
    }

    //플레이어 소모값
    public float runEndu;
    public float crouchEndu;
    public float bleedingCur;
    public float bleedingCool;
    void Update()
    {
        //플레이어 스테이터스

        playerMaxWeight = 100 + 30 * playerClass.str;
        playerWeight = invenMgr.invenWeight;
        if (!md_heaviness)
        {
            crouchSpeed = 1 + playerClass.agillity / 4;
            runSpeed = 5 + 2 * playerClass.agillity / 4;
            walkSpeed = 3 + playerClass.agillity / 4;
        }
        else
        {
            crouchSpeed = 1 + playerClass.agillity / 8;
            runSpeed = 5 + 2 * playerClass.agillity / 8;
            walkSpeed = 3 + playerClass.agillity / 8;
        }
        onehandedActionTime = 1.4f / playerAnim.GetFloat("attackSpeed");
        twohandedActionTime = 1.633f / playerAnim.GetFloat("attackSpeed");
        shootActionTime = 0.5f / playerAnim.GetFloat("attackSpeed");
        grenadeActionTime = 1.4f / playerAnim.GetFloat("attackSpeed");

        if (!md_bleeding && !isCrouch)
        {
            playerRecognitionRanage.transform.localScale = new Vector3(8f - 0.2f * playerClass.security, 0.5f, 8f - 0.2f * playerClass.security);
        }
        else if (!md_bleeding && isCrouch)
        {
            playerRecognitionRanage.transform.localScale = new Vector3(4f - 0.2f * playerClass.security, 0.5f, 4f - 0.2f * playerClass.security);
        }
        if (playerThirsty <= 0)
        {
            playerThirsty = 0;
        }
        if (playerHungry <= 0)
        {
            playerHungry = 0;
        }
        if (playerTired <= 0)
        {
            playerTired = 0;
        }
        if (playerHP <= 0)
        {
            playerHP = 0;
        }

        playerTired += Time.deltaTime * (0.15f - playerClass.health * 0.01f);
        playerHungry += Time.deltaTime * (0.14f - playerClass.health * 0.01f);
        playerThirsty += Time.deltaTime * (0.18f - playerClass.health * 0.01f);
        //플레이어 무들 상태 감지

        if ((float)playerHungry / (float)playerMaxHungry >= 0.7f)
        {
            md_Hungry = true;
        }
        else
        {
            md_Hungry = false;
        }
        if ((float)playerThirsty / (float)playerMaxThirsty >= 0.7f)
        {
            md_Thirsty = true;
        }
        else
        {
            md_Thirsty = false;
        }
        if ((float)playerTired / (float)playerMaxTired >= 0.7f)
        {
            md_Tired = true;
        }
        else
        {
            md_Tired = false;
        }
        if (playerEndurance <= 0)
        {
            md_Exhaust = true;
            isRun = false;
            isCrouch = false;
        }
        if(playerWeight / playerMaxWeight >= 0.7f)
        {
            md_heaviness = true;
        }
        else
        {
            md_heaviness = false;
        }
        //플레이어으 행동들은 지구력을 소모한다
        if(isRun)
        {
            playerEndurance -= (runEndu - playerClass.health / 4) * Time.deltaTime;
        }
        if(isCrouch)
        {
            playerEndurance -= (crouchEndu - playerClass.health / 4) * Time.deltaTime;
        }


        //플레이어의 지구력은 자동회복된다
        if (playerEndurance < playerMaxEndurance)
        {
            playerEndurance += (playerEnduranceRecorvery + playerClass.health) * Time.deltaTime * playerClass.health / 2;
        }
        else
        {
            playerEndurance = 100f;
        }

        //플레이어의 무들 효과

        if(md_Exhaust) //탈진 시 지구력 회복량 감소, 뛰는 속도 감소
        {
            playerEndurance -= playerClass.health *Time.deltaTime / 8;
            runSpeed /= 1.5f;
            if (playerEndurance >= playerMaxEndurance)
            {
                runSpeed *= 1.5f;
                md_Exhaust = false;
            }
        }
        if(playerEndurance < 0)
        {
            playerEndurance = 0;
            if (isRun)
            {
                isRun = false;
                playerAnim.SetBool("ISRUN", false);
            }
            else if (isCrouch)
            {
                isCrouch = false;
                playerAnim.SetBool("ISCROUCH", false);
            }
            else if (isAttack)
            {
                isAttack = false;
                attackRange.SetActive(false);
                attackCurTime = 0;
                isAttackCoolTime = true;
            }
        }
        if(md_bleeding) //좀비의 플레이어 인식 범위 늘어남, 체력 지속 감소
        {
            bleedingCur += Time.deltaTime;
            playerRecognitionRanage.transform.localScale = new Vector3(15f - 0.2f * playerClass.security, 0.5f, 15f - 0.2f * playerClass.security);
            if (bleedingCur >= bleedingCool)
            {
                playerHP--;
                bleedingCur = 0;
            }
        }
        if(md_Hungry)
        {

        }
        if(md_Thirsty)
        {

        }
        if(md_Tired)
        {

        }
        if(md_Panic)
        {
            GetComponentInChildren<WG_PlayerSight>().angleRange = 75;
        }
        else
        {
            GetComponentInChildren<WG_PlayerSight>().angleRange = 130;
        }
        if(md_infection)
        {
            playerHP -= 0.01f;
        }
        if(playerHP <= 0)
        {
            playerState = PLAYERSTATE.DEAD;
        }
        if(isExercise)
        {
            isAttack = false;
        }


        //플레이어의 행동 상태
        switch (playerState)
        {
            case PLAYERSTATE.IDLE:
                isWalk = false;
                playerAnim.SetBool("ISWALK", false);
                if (mvCon.leftController.GetTouchPosition.x != 0 && mvCon.leftController.GetTouchPosition.y != 0)
                {
                    playerState = PLAYERSTATE.WALK;
                }
                break;
            case PLAYERSTATE.WALK:
                if (isCrouch)
                    mvCon.speedMovements = crouchSpeed;
                else
                {
                    if(isRun)
                    {
                        isRun = false;
                        playerAnim.SetBool("ISRUN", false);
                    }
                    isWalk = true;
                    playerAnim.SetBool("ISWALK", true);
                    mvCon.speedMovements = walkSpeed;
                }
                break;
            case PLAYERSTATE.RUN:
                isWalk = false;
                mvCon.speedMovements = runSpeed;
                break;
            case PLAYERSTATE.ACTION:

                break;
            case PLAYERSTATE.DEAD:
                isDead = true;
                playerAnim.SetBool("ISDEAD", true);
                break;
        }
        //플레이어 공격
        if (!isDead)
        {
            if (isReload)
            {
                attackCurTime += Time.deltaTime;
                if (attackCurTime >= fireCont.weaponInfo.ReloadSpeed - playerClass.reload / 10)
                {
                    isReload = false;
                    playerAnim.SetBool("ISRELOAD", false);
                    attackCurTime = 0;
                }
            }
            if (isAttackCoolTime)
            {
                attackCoolCurTime += Time.deltaTime;
                if (attackCoolCurTime >= 0.5f / playerAnim.GetFloat("attackSpeed"))
                {
                    attackCoolCurTime = 0;
                    isAttackCoolTime = false;
                }
            }
            if (noWeapon && isAttack && !isAttackCoolTime && playerEndurance > 0)
            {
                attackCurTime += Time.deltaTime;
                if (attackCurTime < noWeaponActionTime / 5)
                {
                    attackRange.SetActive(false);
                }
                else if (attackCurTime > noWeaponActionTime / 5 && attackCurTime < noWeaponActionTime / 2)
                {
                    attackRange.SetActive(true);
                }
                else if (attackCurTime >= noWeaponActionTime / 2)
                {
                    attackRange.SetActive(false);
                }
                if (attackCurTime >= noWeaponActionTime)
                {
                    playerEndurance -= 2 - playerClass.health / 10;
                    attackCurTime = 0;
                }
            }
            if (isOneHanded && isAttack && !isAttackCoolTime && playerEndurance > 0)
            {
                attackCurTime += Time.deltaTime;
                if (attackCurTime < onehandedActionTime * 3 / 10)
                {
                    attackRange.SetActive(false);
                }
                else if (attackCurTime > onehandedActionTime * 3 / 10 && attackCurTime < onehandedActionTime / 2)
                {
                    attackRange.SetActive(true);
                }
                else if (attackCurTime >= onehandedActionTime / 2)
                {
                    attackRange.SetActive(false);
                }
                if (attackCurTime >= onehandedActionTime)
                {
                    playerEndurance -= 2 - playerClass.health / 10;
                    attackCurTime = 0;
                }
            }
            if (isTwoHanded && isAttack && !isAttackCoolTime && playerEndurance > 0)
            {
                attackCurTime += Time.deltaTime;
                if (attackCurTime < twohandedActionTime * 2 / 5)
                {
                    attackRange.SetActive(false);
                }
                else if (attackCurTime > twohandedActionTime * 2 / 5 && attackCurTime < twohandedActionTime * 3 / 5)
                {
                    attackRange.SetActive(true);
                }
                else if (attackCurTime >= twohandedActionTime * 3 / 5)
                {
                    attackRange.SetActive(false);
                }
                if (attackCurTime >= twohandedActionTime)
                {
                    playerEndurance -= 2 - playerClass.health / 10;
                    attackCurTime = 0;
                }
            }
        }
    }

    public void WG_ClassSelect(int i)  //직업 선택 함수
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Job");

        playerClass.className = data_Dialog[i]["class"].ToString();
        playerClass.str = int.Parse(data_Dialog[i]["str"].ToString());
        playerClass.health = int.Parse(data_Dialog[i]["health"].ToString());
        playerClass.agillity = int.Parse(data_Dialog[i]["agillity"].ToString());
        playerClass.security = int.Parse(data_Dialog[i]["security"].ToString());
        playerClass.carpentry = int.Parse(data_Dialog[i]["carpentry"].ToString());
        playerClass.tailoring = int.Parse(data_Dialog[i]["tailoring"].ToString());
        playerClass.cook = int.Parse(data_Dialog[i]["cook"].ToString());
        playerClass.gathering = int.Parse(data_Dialog[i]["gathering"].ToString());
        playerClass.aim = int.Parse(data_Dialog[i]["aim"].ToString());
        playerClass.reload = int.Parse(data_Dialog[i]["reload"].ToString());
        playerClass.onehandExp = int.Parse(data_Dialog[i]["onehand"].ToString());
        playerClass.twohandExp = int.Parse(data_Dialog[i]["twohand"].ToString());
        playerClass.weaponManagement = int.Parse(data_Dialog[i]["weaponMg"].ToString());
        classSelectUI.SetActive(false);
    }
    public void WG_RandomClassSelect()  //랜덤 직업 선택 함수
    {
        int i = Random.Range(0, 15);
        Debug.Log(i);
        i = i / 2;
        Debug.Log(i);
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Job");

        playerClass.className = data_Dialog[i]["class"].ToString();
        playerClass.str = int.Parse(data_Dialog[i]["str"].ToString());
        playerClass.health = int.Parse(data_Dialog[i]["health"].ToString());
        playerClass.agillity = int.Parse(data_Dialog[i]["agillity"].ToString());
        playerClass.security = int.Parse(data_Dialog[i]["security"].ToString());
        playerClass.carpentry = int.Parse(data_Dialog[i]["carpentry"].ToString());
        playerClass.tailoring = int.Parse(data_Dialog[i]["tailoring"].ToString());
        playerClass.cook = int.Parse(data_Dialog[i]["cook"].ToString());
        playerClass.gathering = int.Parse(data_Dialog[i]["gathering"].ToString());
        playerClass.aim = int.Parse(data_Dialog[i]["aim"].ToString());
        playerClass.reload = int.Parse(data_Dialog[i]["reload"].ToString());
        playerClass.onehandExp = int.Parse(data_Dialog[i]["onehand"].ToString());
        playerClass.twohandExp = int.Parse(data_Dialog[i]["twohand"].ToString());
        playerClass.weaponManagement = int.Parse(data_Dialog[i]["weaponMg"].ToString());
        classSelectUI.SetActive(false);
    }

    public void CrouchEvent()
    {
        if (playerEndurance > 0 && !isDead)
        {
            isCrouch = true;
            playerAnim.SetBool("ISCROUCH", true);
        }
    }
    public void RunEvent()
    {
        if (playerEndurance > 0 && !isDead)
        {
            if (playerState == PLAYERSTATE.WALK && !isCrouch)
            {
                playerState = PLAYERSTATE.RUN;
                isRun = true;
                playerAnim.SetBool("ISRUN", true);
            }
        }
    }
    public void AttackEvent()
    {
        if (playerEndurance > 0 && !isDead)
        {
            isAttack = true;
            if (isOneHanded)
                playerAnim.SetBool("ISONEHANDED", true);
            else if (isTwoHanded)
                playerAnim.SetBool("ISTWOHANDED", true);
            else if (isShoot)
                playerAnim.SetBool("ISSHOOT", true);
            else if (isGrenade)
                playerAnim.SetBool("ISGRENADE", true);
            else
                playerAnim.SetBool("NOWEAPON", true);
        }
    }
    public void AttackUp()
    {
        if (!isDead)
        {
            playerEndurance -= 2 - playerClass.health / 10;
            isAttack = false;
            attackRange.SetActive(false);
            attackCurTime = 0;
            if (isOneHanded)
                playerAnim.SetBool("ISONEHANDED", false);
            else if (isTwoHanded)
                playerAnim.SetBool("ISTWOHANDED", false);
            else if (isShoot)
                fireCont.FireUp();
            else if (isGrenade)
                playerAnim.SetBool("ISGRENADE", false);
            else
                playerAnim.SetBool("NOWEAPON", false);
            isAttackCoolTime = true;
        }
    }
    public void IdleEvent()
    {
        if (!isDead)
            playerState = PLAYERSTATE.IDLE;
        if(isCrouch)
        {
            isCrouch = false;
            playerAnim.SetBool("ISCROUCH", false);
        }
    }
    public void WalkEvent()
    {
        if (playerState == PLAYERSTATE.RUN)
        {
            playerState = PLAYERSTATE.WALK;
        }


        if (isRun)
        {
            isRun = false;
            playerAnim.SetBool("ISRUN", false);
        }
    }
    public void ExerciseEvent()
    {
        isExercise = true;
        playerAnim.SetBool("ISEXERCISE", true);
    }
}
