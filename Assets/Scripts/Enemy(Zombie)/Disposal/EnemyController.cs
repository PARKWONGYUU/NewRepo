using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyInfo
{
    // 플레이어 찾기용
    public GameObject player;
    ATT_Range ear_range;
    ATT_Range eyesight;
    // 플레이어 위치 확인
    public Transform target;
    // Enemy의 케릭터 컨트롤러
    private CharacterController enemyCharacterController;
    // 플레이어 따라다니는 시간
    public float cutTime;
    public float stalkingTime;
    // 몬스터 행동
    ENEMYSTATE eNEMYSTATE = ENEMYSTATE.NONE;
    ENEMY_KINDS eNEMY_KINDS = ENEMY_KINDS.NONE;
    // CSV 데이터 스크립트
    EnemyCSV enemyCSV;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyCharacterController = gameObject.GetComponent<CharacterController>();
        ear_range = transform.GetChild(0).gameObject.GetComponent<ATT_Range>();
        eyesight = transform.GetChild(1).gameObject.GetComponent<ATT_Range>();
        enemyCSV = GameObject.Find("EnemyCSV").GetComponent<EnemyCSV>();
        target = player.transform;
    }

    
    void Update()
    {
        // 범위 안에 시야나 청각중 하나라도 히트하면 AN_Enemy_Move 실행
        if (ear_range.searchPlayer || eyesight.searchPlayer)
		{
            AN_Enemy_Move();
            cutTime += Time.deltaTime;
            if (cutTime > stalkingTime)
			{
                ear_range.searchPlayer = false;
                eyesight.searchPlayer = false;
                cutTime = 0;
            }
        }
    }

    // 좀비 이동
    public void AN_Enemy_Move()
	{
        Vector3 dir = target.position - transform.position;
        dir.y = 0.5f;
        dir.Normalize();
        enemyCharacterController.SimpleMove(dir * moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
    }
    // 좀비 종류
    public void Zombie_Kinds()
	{
		switch (eNEMY_KINDS)
		{
			case ENEMY_KINDS.NONE:
				break;
			case ENEMY_KINDS.ZOMBIE:
                hp = enemyCSV.enemyHP[1];
                moveSpeed = enemyCSV.moveSpeed[1];
                ear_Range = enemyCSV.range[1];
                break;
			case ENEMY_KINDS.SPEED_ZOMBIE:
				break;
			case ENEMY_KINDS.TANK_ZOMBIE:
				break;
			case ENEMY_KINDS.NIGHT_ZOMBIE:
				break;
			case ENEMY_KINDS.SUPER_ZOMBIE:
				break;
			default:
				break;
		}
	}


    private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
            Debug.Log("플레이어랑 만남");
		}
	}
}
