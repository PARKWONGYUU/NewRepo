using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    private float cutTime;
    public float spawnTime = 2f;

    public List<GameObject> enemy_ZOM;
    public GameObject[] enemypool;
    public GameObject[] uniqueEnemyPool;
    public Transform playerTransform;
    public Inventory_Manager invenMgr;

    public float instanceDistanceX;
    public float instanceDistanceZ;
    public float PlayerDistance;

    public int zombieCount;

    public int enemyMaster = 16;
    public int addEnemyMaster = 4;
    float curTime;
    public float enemyaddTime;

    int spawn_Cut = 0;
    int uniqueSpawn_Cut = 0;

    void Start()
    {
        invenMgr = GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>();
        playerTransform = GameObject.Find("Enemy_Maker").transform;
        enemypool = new GameObject[enemyMaster];
        uniqueEnemyPool = new GameObject[addEnemyMaster];
        for (int i = 0; i < enemyMaster; i++)
        {
            enemypool[i] = Instantiate(enemy_ZOM[0]) as GameObject;  
            enemypool[i].SetActive(false);
        }
        for (int j = 0; j < addEnemyMaster; j++)
        {
            int ran = Random.Range(1, enemy_ZOM.Count);
            uniqueEnemyPool[j] = Instantiate(enemy_ZOM[ran]) as GameObject;         
            uniqueEnemyPool[j].SetActive(false);
        }
        invenMgr.inven_View.SetActive(false);
    }

    void Update()
    {
        cutTime += Time.deltaTime;
        if (cutTime > spawnTime)
        {
            // 오브젝트 풀링
            cutTime = 0;
            for (int i = 0; i < enemyMaster; i++)
            {
                //Debug.Log("TEST");
                if (enemypool[i].activeSelf == false)
                {
                    // Debug.Log(playerTransform.position.x);
                    // Debug.Log(playerTransform.position.z);


                    //continue;
                    zombieCount++;
                    float x = Random.Range(playerTransform.position.x - instanceDistanceX, playerTransform.position.x + instanceDistanceX);
                    float z = Random.Range(playerTransform.position.z - instanceDistanceZ, playerTransform.position.z + instanceDistanceZ);
                    float roty = Random.Range(-180f, 180f);
                    enemypool[i].transform.position = new Vector3(x, 0.5f, z);
                    enemypool[i].transform.rotation = Quaternion.Euler(0, roty, 0);

                    enemypool[i].SetActive(true);
                    // enemypool[i].name = "Enemy" + spawn_Cut;
                    ++spawn_Cut;
                    break;
                }
            }
            for (int j  = 0; j < addEnemyMaster; j++)
            {
                //Debug.Log("TEST");
                if (uniqueEnemyPool[j].activeSelf == false)
                {
                    // Debug.Log("생성");

                    zombieCount++;
                    //continue;
                    float x = Random.Range(playerTransform.position.x - instanceDistanceX, playerTransform.position.x + instanceDistanceX);
                    float z = Random.Range(playerTransform.position.z - instanceDistanceZ, playerTransform.position.z + instanceDistanceZ);
                    float roty = Random.Range(-180f, 180f);
					uniqueEnemyPool[j].transform.position = new Vector3(x, 0.5f, z);
					uniqueEnemyPool[j].transform.rotation = Quaternion.Euler(0, roty, 0);

					uniqueEnemyPool[j].SetActive(true);
					//uniqueEnemyPool[j].name = "Unique_Enemy" + uniqueSpawn_Cut;
					++uniqueSpawn_Cut;
					break;
                }
            }
        }
    }
}
