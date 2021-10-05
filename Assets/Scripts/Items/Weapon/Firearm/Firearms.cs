using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Firearms : MonoBehaviour
{
    public string itemName;
    public int attackPower; //무기 공격력
    public float attackRange; // 무기 거리
    public float attackSpeed; // 무기 공속
    public float weight; //무게
    public float durability; // 내구도
    public float agility; //날렵함
    public float endurance; //지구력
    public float ReloadSpeed; //재장전
    public float AimingSpeed; //조준
    public int accuracy; //명중률
    public int bulletsNum; //총알 갯수
    public int maxloadbullet; // 총알 최대장전 수
    public float attackCoolTime; //쏘는 속도
    public int mainItemNum; //무기 분류
    public int subItemNum;
    public int durabillity;
}
