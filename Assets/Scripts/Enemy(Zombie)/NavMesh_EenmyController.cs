using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh_EenmyController : MonoBehaviour
{
	// 좀비 종류
	public enum ENEMY_KINDS
	{
		NONE = 0,
		ZOMBIE,
		TANK_ZOMBIE,
		SPEED_ZOMBIE,
		NIGHT_ZOMBIE,
		SUPER_ZOMBIE
	}
	// 좀비 상태
	public enum ENEMYSTATE
	{
		NONE = 0,
		IDLE,
		WALK,
		MOVE,
		ATTACK,
		DAMAGE,
		DEAD
	}
	public ENEMYSTATE eNEMYSTATE = ENEMYSTATE.NONE;
	public ENEMY_KINDS eNEMY_KINDS = ENEMY_KINDS.NONE;

	// 능력치
	public float strength; // 힘 (높을수록 물기 확률 올림)
	public int enemyhp; // 좀비 체력
	public float enemyMove_Speed; // 좀비 뛰는속도
	public float enemyWalk_Speed; // 좀비 걷는속도
	public float enemyStop_Speed; // 좀비 멈춤
	private float enemy_STF; // 경직시간 체크
	public float enemy_Stiffness; // 경직 시간
	private float enemy_IE_CH; // 대기시간 체크
	public float enemy_Idle; // 대기시간


	[SerializeField] private float enemeyDead; // 좀비 죽는 시간 체크용
	[SerializeField] private float enemeyDeadsTime = 100f; // 좀비 죽는 맥스 시간


	[SerializeField] private float enemy_attTime; //공격시간 채크용
	public float enemy_attCoolTime = 2.4f; //공격 속도
	public float attack_Range = 3;

	public NavMeshAgent navAgent;
	public GameObject zombieItemBox; // 좀비 아이템 박스
	public WG_PlayerEquipment playerItem; // 플레이어 아이템 클래스

	public GameObject[] players; // 플레이어들 확인
	private Animator enemyAnim; // 좀비 애니메이션
	public int get_Player_ATT = 100; //플레이어 공격력 받아 오기용


	public float accuracy; // 총알 명중률 받아 오는 변수
	public bool is_damageed; //플레이어한테 맞음
	public bool is_Bullet_Hit = false; // 총알이랑 부딛힘
	public bool is_Bleeding = false; //플레이어 출혈
	public bool is_Infection = false; //플레이어 감염
	public bool player_Get = false; // 플레이어를 시야로 찾음
	public bool player_Sound_Get = false; // 플레이어를 소리로 찾음
	private bool daed;

	public bool enemd_Dead = false; // 좀비 죽음
	public float enemey_MaxWALK; // 좀비 걷는 최대
	private float enemey_Walk_x; //좀비 x 걷기
	private float enemey_Walk_z; // 좀비 y 걷기


	EnemyCSV enemyCSV; //CSV 파일
	[SerializeField] private float curTime; // Time add 용
	[SerializeField] private float coolTime; //  Time  채크용
	[SerializeField] private Vector3 view_Y;  // 시야 높이
	[SerializeField] private Transform target; // 플레이어 위치
	[SerializeField] private Transform enemyTrans; // 좀비 위치
	[SerializeField] private float viewAngle;  // 시야 각도
	[SerializeField] private float viewDistance; // 시야 거리 
	[SerializeField] private LayerMask targetMask;  // 타겟 마스크

	public AudioClip zombieIdleSound; // 평상시 좀비 사운드
	public AudioClip zombieAttSound; // 공격시 사운드
	public AudioClip zombieDamgeSound; // 맞았을때 사운드
	public AudioClip zombieDeadSound; // 죽었을때 사운드

	private void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
		enemyTrans = GetComponent<Transform>();
		enemyCSV = GameObject.Find("EnemyCSV").GetComponent<EnemyCSV>();
		players = GameObject.FindGameObjectsWithTag("Player");
		zombieItemBox = gameObject.transform.GetChild(1).gameObject;
		enemyAnim = transform.GetChild(0).GetComponent<Animator>();
		target = players[0].transform;
		playerItem = players[0].GetComponent<WG_PlayerEquipment>();
		zombieItemBox.GetComponent<GetInventory_Manver>().boxInvenButton = GameObject.FindGameObjectWithTag("GetButton");
	}

	void Start()
	{
		eNEMYSTATE = ENEMYSTATE.IDLE;
		coolTime = UnityEngine.Random.Range(3, 10);
		if (gameObject.name == "Enemy_Zom_V2(Clone)")
		{
			eNEMY_KINDS = ENEMY_KINDS.ZOMBIE;
		}
		else if (gameObject.name == "Enemy_Zom_Tank_V2(Clone)")
		{
			eNEMY_KINDS = ENEMY_KINDS.TANK_ZOMBIE;
		}
		else if (gameObject.name == "Enemy_Zom_Speed_V2(Clone)")
		{
			eNEMY_KINDS = ENEMY_KINDS.SPEED_ZOMBIE;
		}
		AN_Enemy_Kinds();
		//AudioManager.Instance().PlaySfx(zombieIdleSound);
		//zombieItemBox.SetActive(false);
		daed = true;
	}
	void Update()
	{
		AN_Enmey_State();
		if (!enemd_Dead)
		{
			AN_Enmey_View();
		}
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance > 100)
		{
			gameObject.SetActive(false);
			eNEMYSTATE = ENEMYSTATE.IDLE;
		}
	}
	// 좀비 상태 체크( 대기 걷기 이동 공격 데미지받음 죽음)
	private void AN_Enmey_State()
	{

		// Debug.Log(eNEMYSTATE);
		switch (eNEMYSTATE)
		{

			case ENEMYSTATE.NONE:
				break;

			case ENEMYSTATE.IDLE:
				enemyAnim.SetBool("IDLE", true);
				enemyAnim.SetBool("ATTACK", false);
				enemyAnim.SetBool("WALK", false);
				// AudioManager.Instance().PlaySfx(zombieIdleSound);
				float distance = Vector3.Distance(target.position, transform.position);
				if (!player_Get || !player_Sound_Get)
				{
					navAgent.speed = 0;
					AN_Enemy_RandomMove();
					if (player_Get || player_Sound_Get)
					{
						return;
					}
					enemy_IE_CH += Time.deltaTime;
					if (enemy_IE_CH > enemy_Idle)
					{
						enemy_IE_CH = 0;
					}
				}
				else
				{
					if (distance < attack_Range)
					{
						eNEMYSTATE = ENEMYSTATE.ATTACK;
						enemyAnim.SetBool("ATTACK", true);
						enemyAnim.SetBool("IDLE", false);
						enemy_attTime = 0;
					}
				}
				if (enemyhp <= 0)
				{
					eNEMYSTATE = ENEMYSTATE.DEAD;
					enemy_STF = 0;
				}
				break;

			case ENEMYSTATE.WALK:
				enemyAnim.SetBool("ATTACK", false);
				enemyAnim.SetBool("WALK", true);
				enemyAnim.SetBool("IDLE", false);
				// AudioManager.Instance().PlaySfx(zombieIdleSound);
				distance = Vector3.Distance(target.position, transform.position);
				if (!player_Get || !player_Sound_Get)
				{
					AN_Enemy_RandomMove();
					navAgent.speed = enemyWalk_Speed;
					navAgent.destination = new Vector3(enemey_Walk_x, transform.position.y, enemey_Walk_z);
					if (player_Get || player_Sound_Get)
					{
						return;
					}
					// eNEMYSTATE = ENEMYSTATE.IDLE;
				}
				else
				{
					if (distance < attack_Range)
					{
						eNEMYSTATE = ENEMYSTATE.ATTACK;
						enemyAnim.SetBool("ATTACK", true);
						enemyAnim.SetBool("WALK", false);
						enemy_attTime = 0;
					}
				}
				if (enemyhp <= 0)
				{
					eNEMYSTATE = ENEMYSTATE.DEAD;
					enemy_STF = 0;
				}
				break;

			case ENEMYSTATE.MOVE:
				enemyAnim.SetBool("IDLE", false);
				enemyAnim.SetBool("ATTACK", false);
				enemyAnim.SetBool("WALK", true);
				// AudioManager.Instance().PlaySfx(zombieIdleSound);
				navAgent.speed = enemyMove_Speed;
				distance = Vector3.Distance(target.position, transform.position);
				if (distance < attack_Range)
				{
					eNEMYSTATE = ENEMYSTATE.ATTACK;
					enemyAnim.SetBool("ATTACK", true);
					enemyAnim.SetBool("WALK", false);
				}
				else if (!player_Get || !player_Sound_Get)
				{
					eNEMYSTATE = ENEMYSTATE.IDLE;
					enemyAnim.SetBool("IDLE", true);
					enemyAnim.SetBool("WALK", false);
				}
				if (enemyhp <= 0)
				{
					eNEMYSTATE = ENEMYSTATE.DEAD;
					enemy_STF = 0;
				}
				break;

			case ENEMYSTATE.ATTACK:
				if (!is_damageed)
				{
					// Debug.Log("들어옴");
					enemyAnim.SetBool("ATTACK", true);
					enemyAnim.SetBool("WALK", false);
					enemyAnim.SetBool("IDLE", false);
					// AudioManager.Instance().PlaySfx(zombieAttSound);
					float dist = Vector3.Distance(target.position, transform.position);
					if (dist > attack_Range && player_Get)
					{
						// Debug.Log("공격 나감");
						navAgent.speed = enemyMove_Speed;
						eNEMYSTATE = ENEMYSTATE.MOVE;
						enemyAnim.SetBool("WALK", true);
						enemyAnim.SetBool("ATTACK", false);
					}
					else
					{

						navAgent.speed = 0;
						navAgent.destination = transform.position;
						enemy_attTime += Time.deltaTime;
						if (enemy_attTime > enemy_attCoolTime)
						{
							AN_Att_Enemey();
							player_Get = false;
							eNEMYSTATE = ENEMYSTATE.IDLE;
							enemyAnim.SetBool("IDLE", true);
							enemyAnim.SetBool("ATTACK", false);
							enemy_attTime = 0;
						}
					}
				}
				else
				{
					eNEMYSTATE = ENEMYSTATE.DAMAGE;
				}
				if (enemyhp <= 0)
				{
					eNEMYSTATE = ENEMYSTATE.DEAD;
					enemy_STF = 0;
				}
				break;

			case ENEMYSTATE.DAMAGE:

				enemy_STF += Time.deltaTime;
				navAgent.speed = 0;
				enemyAnim.SetBool("DAMAGED", true);
				enemyAnim.SetBool("WALK", false);
				enemyAnim.SetBool("ATTACK", false);
				enemyAnim.SetBool("IDLE", false);
				if (enemy_STF > enemy_Stiffness)
				{
					// Debug.Log("데미지");
					if (is_Bullet_Hit)
					{
						AN_On_Bullet_Damaged();
						is_Bullet_Hit = false;
						enemy_STF = 0;
						is_damageed = false;
					}
					else
					{
						// Debug.Log("근접무기에 맞음");
						int rnd = UnityEngine.Random.Range(0, 100);
						Double dmg = rnd + (get_Player_ATT * 0.02f);
						enemyhp -= (int)dmg;
						enemy_STF = 0;
						is_damageed = false;
					}
					enemy_STF = 0;
					navAgent.speed = enemyMove_Speed;
					eNEMYSTATE = ENEMYSTATE.MOVE;
					enemyAnim.SetBool("WALK", true);
					enemyAnim.SetBool("DAMAGED", false);
				}
				if (enemyhp <= 0)
				{
					eNEMYSTATE = ENEMYSTATE.DEAD;
				}
				break;

			case ENEMYSTATE.DEAD:
				enemd_Dead = true;
				navAgent.speed = 0;
				//if (daed)
				//{
				//	zombieItemBox.SetActive(true);
				//	daed = false;
				//}
				zombieItemBox.SetActive(true);
				enemyAnim.SetBool("DEAD", true);
				enemyAnim.SetBool("DAMAGED", false);
				GameObject.Find("SightRange").GetComponent<WG_PlayerSight>().targets.Remove(gameObject);
				gameObject.tag = "EnemyDead";
				// AudioManager.Instance().PlaySfx(zombieDeadSound);
				enemeyDead += Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
				// navAgent.updateRotation = false;
				if (enemeyDead > enemeyDeadsTime)
				{
					// transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
					gameObject.tag = "Enemy";
					navAgent.updateRotation = true;
					gameObject.SetActive(false);
					AN_Enemy_Kinds();
					eNEMYSTATE = ENEMYSTATE.IDLE;
					enemd_Dead = false;
					enemeyDead = 0;
					Destroy(zombieItemBox.GetComponent<GetInventory_Manver>().obj);
					zombieItemBox.GetComponent<GetInventory_Manver>().AN_ItemDestroy();
				}
				break;

			default:
				Debug.Log("eNEMYSTATE 값이 없습니다.");
				break;
		}
	}
	// 각 좀비별 스탯(스타트용)
	void AN_Enemy_Kinds()
	{
		switch (eNEMY_KINDS)
		{
			case ENEMY_KINDS.NONE:
				break;
			case ENEMY_KINDS.ZOMBIE:
				AN_Enemy_Info(0);
				break;
			case ENEMY_KINDS.TANK_ZOMBIE:
				AN_Enemy_Info(1);
				break;
			case ENEMY_KINDS.SPEED_ZOMBIE:
				AN_Enemy_Info(2);
				break;
			case ENEMY_KINDS.NIGHT_ZOMBIE:
				AN_Enemy_Info(3);
				break;
			case ENEMY_KINDS.SUPER_ZOMBIE:
				AN_Enemy_Info(4);
				break;
			default:
				break;
		}
	}
	// 각 좀비 스탯 가져오기
	void AN_Enemy_Info(int i)
	{
		enemyhp = enemyCSV.enemyHP[i];
		enemyMove_Speed = enemyCSV.moveSpeed[i];
		strength = enemyCSV.enemy_Strength[i];
		enemyWalk_Speed = enemyCSV.walkSpeed[i];
	}
	// 일정시간 마다 랜덤으로 걷기 또는 대기상태로 만듬
	void AN_Enemy_RandomMove()
	{
		curTime += Time.deltaTime;
		if (curTime > coolTime)
		{
			enemey_Walk_x = UnityEngine.Random.Range(-enemey_MaxWALK, enemey_MaxWALK);
			enemey_Walk_z = UnityEngine.Random.Range(-enemey_MaxWALK, enemey_MaxWALK);
			coolTime = UnityEngine.Random.Range(3, 10);

			if (eNEMYSTATE == ENEMYSTATE.IDLE || eNEMYSTATE == ENEMYSTATE.WALK)
			{
				int rad = UnityEngine.Random.Range(0, 2);
				if (rad == 0)
				{
					//Debug.Log(rad);
					eNEMYSTATE = ENEMYSTATE.IDLE;
					enemyAnim.SetBool("IDLE", true);
					enemyAnim.SetBool("WALK", false);

					curTime = 0;
				}
				else
				{
					//Debug.Log(rad);
					eNEMYSTATE = ENEMYSTATE.WALK;
					enemyAnim.SetBool("WALK", true);
					enemyAnim.SetBool("IDLE", false);
					curTime = 0;
				}
			}
		}
	}
	// 공격
	void AN_Att_Enemey()
	{
		if (playerItem.cap == null && playerItem.shoes == null && playerItem.pants == null && playerItem.shirts == null)
		{
			AN_EnemyPlayerATT();
		}
		else
		{
			int rand = UnityEngine.Random.Range(0, 4);
			if (rand == 0)
			{
				if(playerItem.cap == null)
                {
					AN_EnemyPlayerATT();
					return;
				}
				playerItem.cap.GetComponent<Armors>().armorHp--;
				if(playerItem.cap.GetComponent<Armors>().armorHp == 0)
				{
					playerItem.cap = null;
				}
			}
			else if (rand == 1)
			{
				if (playerItem.shoes == null)
				{
					AN_EnemyPlayerATT();
					return;
				}
				playerItem.shoes.GetComponent<Armors>().armorHp--;
				if (playerItem.shoes.GetComponent<Armors>().armorHp == 0)
				{
					playerItem.shoes = null;
				}
			}
			else if (rand == 2)
			{
				if (playerItem.pants == null)
				{
					AN_EnemyPlayerATT();
					return;
				}
				playerItem.pants.GetComponent<Armors>().armorHp--;
				if (playerItem.pants.GetComponent<Armors>().armorHp == 0)
				{
					playerItem.pants = null;
				}
			}
			else if (rand == 3)
			{
				if (playerItem.shirts == null)
				{
					AN_EnemyPlayerATT();
					return;
				}
				playerItem.shirts.GetComponent<Armors>().armorHp--;
				if (playerItem.shirts.GetComponent<Armors>().armorHp == 0)
				{
					playerItem.shirts = null;
				}
			}
			else
			{
				Debug.Log(rand);
			}
		}
	}

	void AN_EnemyPlayerATT()
    {
		int rand = UnityEngine.Random.Range(0, 100);
		if (rand >= 0 && rand <= 50)
		{
			// Debug.Log("긁힘 :: 약한 출혈");
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().playerHP -= 0.7f * strength;
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_bleeding = true;
		}
		else if (rand >= 61 && rand <= 97)
		{
			// Debug.Log("찢어짐 :: 큰 출혈");
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().playerHP -= 0.9f * strength;
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_bleeding = true;
			AN_is_Infection();
		}
		else
		{
			// Debug.Log("물림 :: 감염");
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().playerHP -= 1.5f * strength;
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_infection = true;
		}
	}
	// 일정확률로 감염
	void AN_is_Infection()
	{
		int rand = UnityEngine.Random.Range(0, 100);
		if (rand >= 0 && rand <= 5 + strength)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_infection = true;
		}
	}
	// 좀비 플레이어에게 이동
	private void AN_Enemy_Move()
	{
		//float dist = (target.position - enemyTrans.position).sqrMagnitude;
		//foreach (GameObject player in players)
		//{
		//	if ((player.transform.position - transform.position).sqrMagnitude < dist)
		//	{
		//		Debug.Log(dist);
		//		target = player.transform;

		//		break;
		//	}
		//}
		// 플레이어에게 이동
		float dist2 = Vector3.Distance(target.position, transform.position);
		if (dist2 < attack_Range)
		{
			navAgent.destination = transform.position;
		}
		else
		{
			if(enemyhp > 0)
            {
				navAgent.speed = enemyMove_Speed;
				navAgent.angularSpeed = 120; 
				navAgent.destination = target.position;
				if (!player_Get)
				{
					eNEMYSTATE = ENEMYSTATE.IDLE;
					enemyAnim.SetBool("IDLE", true);
					enemyAnim.SetBool("ATTACK", false);
					enemyAnim.SetBool("WALK", false);
				}
			}
			else if(enemyhp <= 0)
            {
				navAgent.angularSpeed = 0;
				navAgent.destination = transform.position;
			}
		}
	}
	private Vector3 BoundaryAngle(float angle)
	{
		// DrawRay로 보여주는 길이 및 시야 거리표시
		angle += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
	}
	// 플레이어 시야에 들어온지 확인
	private void AN_Enmey_View()
	{
		Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);  // 시야각의 왼쪽 경계
		Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);  // 시야각의 오른쪽 경계선

		// 시야 범위를 DrawRay로 보여줌
		Debug.DrawRay(transform.position + view_Y, leftBoundary, Color.green);
		Debug.DrawRay(transform.position + view_Y, rightBoundary, Color.green);

		Collider[] target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

		// 현제 서치한 target를 전부 찾아 준다.
		for (int i = 0; i < target.Length; i++)
		{
			Transform targetTf = target[i].transform;
			// 찾은 target를 중 Plaeyr 태그를 가주고 있는 오브젝트를 따라간다.
			if (targetTf.CompareTag("Player"))
			{
				// 현제 찾은 오브젝트의 위치를 노멀라이즈하여 위치값을 찾는다.
				Vector3 direction = (targetTf.position - transform.position).normalized;
				float angle = Vector3.Angle(direction, transform.forward);
				// 만약 플레이어 오브젝트가 현제  Enemey의 viewAngle각도 안에 들어 왔다면
				if (angle < viewAngle * 0.5f)
				{
					RaycastHit hit;
					//viewDistance의 크기 안에 좀비가 있다면 (direction = 플레이어와 좀비의 거리)
					if (Physics.Raycast(transform.position + view_Y, direction, out hit, viewDistance))
					{
						//Debug.Log(hit.transform.name);
						// Debug.Log("들어옴 1");
						// 히트한 트렌스폼의 태그가 플레이어면 실행
						if (hit.transform.CompareTag("Player"))
						{
							if (enemyhp >= 0)
							{
								player_Get = true;
								player_Sound_Get = true;
								float dist = Vector3.Distance(this.target.position, transform.position);
								if (dist < attack_Range)
								{
									if (is_damageed && !enemd_Dead)
									{
										eNEMYSTATE = ENEMYSTATE.DAMAGE;
									}
									else if (!enemd_Dead && !is_damageed)
									{
										eNEMYSTATE = ENEMYSTATE.ATTACK;
										// Debug.Log("공격 들어옴 _히트쪽");
										enemyAnim.SetBool("ATTACK", true);
										enemyAnim.SetBool("WALK", false);
									}
								}
								else
								{
									if (!enemd_Dead)
									{
										eNEMYSTATE = ENEMYSTATE.MOVE;
										enemyAnim.SetBool("IDLE", false);
										enemyAnim.SetBool("ATTACK", false);
										enemyAnim.SetBool("WALK", true);
										if (dist > viewDistance)
										{
											player_Get = false;
										}
										AN_Enemy_Move();
									}
								}
							}
							if (enemyhp <= 0)
							{
								// targetTf = null;
								// target = null;
								eNEMYSTATE = ENEMYSTATE.DEAD;
								// transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
								enemy_STF = 0;
							}
							Debug.DrawRay(transform.position + view_Y, direction, Color.blue);
						}
					}
				}
			}
		}
	}

	void AN_On_Bullet_Damaged()
	{
		int rnd = UnityEngine.Random.Range(0, 100);
		if (rnd <= 60 + accuracy)
		{
			Debug.Log("총 맞음");
			Double dmg = rnd + (get_Player_ATT * 0.5);
			enemyhp -= (int)dmg;
		}
		else
		{
			Debug.Log("빗맞음");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!enemd_Dead)
		{

			if (other.gameObject.CompareTag("Carrion"))
			{
				player_Sound_Get = true;
				eNEMYSTATE = ENEMYSTATE.MOVE;
				enemyAnim.SetBool("IDLE", false);
				enemyAnim.SetBool("ATTACK", false);
				enemyAnim.SetBool("WALK", true);
			}
			//else if (other.gameObject.CompareTag("PlayerSound"))
			//{
			//	player_Sound_Get = true;
			//	eNEMYSTATE = ENEMYSTATE.MOVE;
			//	enemyAnim.SetBool("IDLE", false);
			//	enemyAnim.SetBool("ATTACK", false);
			//	enemyAnim.SetBool("WALK", true);
			//}

			if (other.gameObject.CompareTag("Bullet"))
			{
				is_Bullet_Hit = true;
				eNEMYSTATE = ENEMYSTATE.DAMAGE;
				enemyAnim.SetBool("DAMAGED", true);
				enemyAnim.SetBool("WALK", false);
				enemyAnim.SetBool("ATTACK", false);
				enemyAnim.SetBool("IDLE", false);
			}
			else if (other.gameObject.CompareTag("PlayerAttack"))
			{
				if (enemyhp >= 0)
				{
					is_damageed = true;
					// Debug.Log("맞음");
					eNEMYSTATE = ENEMYSTATE.DAMAGE;
					enemyAnim.SetBool("DAMAGED", true);
					enemyAnim.SetBool("WALK", false);
					enemyAnim.SetBool("ATTACK", false);
					enemyAnim.SetBool("IDLE", false);
					AudioManager.Instance().PlaySfx(zombieDamgeSound);
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!enemd_Dead)
		{
			if (other.gameObject.CompareTag("PlayerSound"))
			{
				//Destroy(zombieItemBox.GetComponent<GetInventory_Manver>().obj);
				//zombieItemBox.GetComponent<GetInventory_Manver>().AN_ItemDestroy();
				player_Sound_Get = false;
				eNEMYSTATE = ENEMYSTATE.IDLE;
				enemyAnim.SetBool("IDLE", true);
				enemyAnim.SetBool("ATTACK", false);
				enemyAnim.SetBool("WALK", false);
			}
		}
	}
}
