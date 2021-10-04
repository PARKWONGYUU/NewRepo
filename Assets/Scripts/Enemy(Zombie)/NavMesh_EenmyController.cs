using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh_EenmyController : MonoBehaviour
{
	// ���� ����
	public enum ENEMY_KINDS
	{
		NONE = 0,
		ZOMBIE,
		TANK_ZOMBIE,
		SPEED_ZOMBIE,
		NIGHT_ZOMBIE,
		SUPER_ZOMBIE
	}
	// ���� ����
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

	// �ɷ�ġ
	public float strength; // �� (�������� ���� Ȯ�� �ø�)
	public int enemyhp; // ���� ü��
	public float enemyMove_Speed; // ���� �ٴ¼ӵ�
	public float enemyWalk_Speed; // ���� �ȴ¼ӵ�
	public float enemyStop_Speed; // ���� ����
	private float enemy_STF; // �����ð� üũ
	public float enemy_Stiffness; // ���� �ð�
	private float enemy_IE_CH; // ���ð� üũ
	public float enemy_Idle; // ���ð�


	[SerializeField] private float enemeyDead; // ���� �״� �ð� üũ��
	[SerializeField] private float enemeyDeadsTime = 100f; // ���� �״� �ƽ� �ð�


	[SerializeField] private float enemy_attTime; //���ݽð� äũ��
	public float enemy_attCoolTime = 2.4f; //���� �ӵ�
	public float attack_Range = 3;

	public NavMeshAgent navAgent;
	public GameObject zombieItemBox; // ���� ������ �ڽ�
	public WG_PlayerEquipment playerItem; // �÷��̾� ������ Ŭ����

	public GameObject[] players; // �÷��̾�� Ȯ��
	private Animator enemyAnim; // ���� �ִϸ��̼�
	public int get_Player_ATT = 100; //�÷��̾� ���ݷ� �޾� �����


	public float accuracy; // �Ѿ� ���߷� �޾� ���� ����
	public bool is_damageed; //�÷��̾����� ����
	public bool is_Bullet_Hit = false; // �Ѿ��̶� �ε���
	public bool is_Bleeding = false; //�÷��̾� ����
	public bool is_Infection = false; //�÷��̾� ����
	public bool player_Get = false; // �÷��̾ �þ߷� ã��
	public bool player_Sound_Get = false; // �÷��̾ �Ҹ��� ã��
	private bool daed;

	public bool enemd_Dead = false; // ���� ����
	public float enemey_MaxWALK; // ���� �ȴ� �ִ�
	private float enemey_Walk_x; //���� x �ȱ�
	private float enemey_Walk_z; // ���� y �ȱ�


	EnemyCSV enemyCSV; //CSV ����
	[SerializeField] private float curTime; // Time add ��
	[SerializeField] private float coolTime; //  Time  äũ��
	[SerializeField] private Vector3 view_Y;  // �þ� ����
	[SerializeField] private Transform target; // �÷��̾� ��ġ
	[SerializeField] private Transform enemyTrans; // ���� ��ġ
	[SerializeField] private float viewAngle;  // �þ� ����
	[SerializeField] private float viewDistance; // �þ� �Ÿ� 
	[SerializeField] private LayerMask targetMask;  // Ÿ�� ����ũ

	public AudioClip zombieIdleSound; // ���� ���� ����
	public AudioClip zombieAttSound; // ���ݽ� ����
	public AudioClip zombieDamgeSound; // �¾����� ����
	public AudioClip zombieDeadSound; // �׾����� ����

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
	// ���� ���� üũ( ��� �ȱ� �̵� ���� ���������� ����)
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
					// Debug.Log("����");
					enemyAnim.SetBool("ATTACK", true);
					enemyAnim.SetBool("WALK", false);
					enemyAnim.SetBool("IDLE", false);
					// AudioManager.Instance().PlaySfx(zombieAttSound);
					float dist = Vector3.Distance(target.position, transform.position);
					if (dist > attack_Range && player_Get)
					{
						// Debug.Log("���� ����");
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
					// Debug.Log("������");
					if (is_Bullet_Hit)
					{
						AN_On_Bullet_Damaged();
						is_Bullet_Hit = false;
						enemy_STF = 0;
						is_damageed = false;
					}
					else
					{
						// Debug.Log("�������⿡ ����");
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
				Debug.Log("eNEMYSTATE ���� �����ϴ�.");
				break;
		}
	}
	// �� ���� ����(��ŸƮ��)
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
	// �� ���� ���� ��������
	void AN_Enemy_Info(int i)
	{
		enemyhp = enemyCSV.enemyHP[i];
		enemyMove_Speed = enemyCSV.moveSpeed[i];
		strength = enemyCSV.enemy_Strength[i];
		enemyWalk_Speed = enemyCSV.walkSpeed[i];
	}
	// �����ð� ���� �������� �ȱ� �Ǵ� �����·� ����
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
	// ����
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
			// Debug.Log("���� :: ���� ����");
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().playerHP -= 0.7f * strength;
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_bleeding = true;
		}
		else if (rand >= 61 && rand <= 97)
		{
			// Debug.Log("������ :: ū ����");
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().playerHP -= 0.9f * strength;
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_bleeding = true;
			AN_is_Infection();
		}
		else
		{
			// Debug.Log("���� :: ����");
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().playerHP -= 1.5f * strength;
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_infection = true;
		}
	}
	// ����Ȯ���� ����
	void AN_is_Infection()
	{
		int rand = UnityEngine.Random.Range(0, 100);
		if (rand >= 0 && rand <= 5 + strength)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<WG_Player>().md_infection = true;
		}
	}
	// ���� �÷��̾�� �̵�
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
		// �÷��̾�� �̵�
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
		// DrawRay�� �����ִ� ���� �� �þ� �Ÿ�ǥ��
		angle += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
	}
	// �÷��̾� �þ߿� ������ Ȯ��
	private void AN_Enmey_View()
	{
		Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);  // �þ߰��� ���� ���
		Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);  // �þ߰��� ������ ��輱

		// �þ� ������ DrawRay�� ������
		Debug.DrawRay(transform.position + view_Y, leftBoundary, Color.green);
		Debug.DrawRay(transform.position + view_Y, rightBoundary, Color.green);

		Collider[] target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

		// ���� ��ġ�� target�� ���� ã�� �ش�.
		for (int i = 0; i < target.Length; i++)
		{
			Transform targetTf = target[i].transform;
			// ã�� target�� �� Plaeyr �±׸� ���ְ� �ִ� ������Ʈ�� ���󰣴�.
			if (targetTf.CompareTag("Player"))
			{
				// ���� ã�� ������Ʈ�� ��ġ�� ��ֶ������Ͽ� ��ġ���� ã�´�.
				Vector3 direction = (targetTf.position - transform.position).normalized;
				float angle = Vector3.Angle(direction, transform.forward);
				// ���� �÷��̾� ������Ʈ�� ����  Enemey�� viewAngle���� �ȿ� ��� �Դٸ�
				if (angle < viewAngle * 0.5f)
				{
					RaycastHit hit;
					//viewDistance�� ũ�� �ȿ� ���� �ִٸ� (direction = �÷��̾�� ������ �Ÿ�)
					if (Physics.Raycast(transform.position + view_Y, direction, out hit, viewDistance))
					{
						//Debug.Log(hit.transform.name);
						// Debug.Log("���� 1");
						// ��Ʈ�� Ʈ�������� �±װ� �÷��̾�� ����
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
										// Debug.Log("���� ���� _��Ʈ��");
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
			Debug.Log("�� ����");
			Double dmg = rnd + (get_Player_ATT * 0.5);
			enemyhp -= (int)dmg;
		}
		else
		{
			Debug.Log("������");
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
					// Debug.Log("����");
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
