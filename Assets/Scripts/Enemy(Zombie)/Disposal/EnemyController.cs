using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyInfo
{
    // �÷��̾� ã���
    public GameObject player;
    ATT_Range ear_range;
    ATT_Range eyesight;
    // �÷��̾� ��ġ Ȯ��
    public Transform target;
    // Enemy�� �ɸ��� ��Ʈ�ѷ�
    private CharacterController enemyCharacterController;
    // �÷��̾� ����ٴϴ� �ð�
    public float cutTime;
    public float stalkingTime;
    // ���� �ൿ
    ENEMYSTATE eNEMYSTATE = ENEMYSTATE.NONE;
    ENEMY_KINDS eNEMY_KINDS = ENEMY_KINDS.NONE;
    // CSV ������ ��ũ��Ʈ
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
        // ���� �ȿ� �þ߳� û���� �ϳ��� ��Ʈ�ϸ� AN_Enemy_Move ����
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

    // ���� �̵�
    public void AN_Enemy_Move()
	{
        Vector3 dir = target.position - transform.position;
        dir.y = 0.5f;
        dir.Normalize();
        enemyCharacterController.SimpleMove(dir * moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
    }
    // ���� ����
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
            Debug.Log("�÷��̾�� ����");
		}
	}
}
