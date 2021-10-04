using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmsFireBullets : MonoBehaviour
{
    public Rigidbody bulletRigid;
    public BulletPooling bulletPooling;  
    public float Bulletspeed = 10f;
    public float curTime;
    public GameObject bulletMaker;
    public FireController fireCont;

    void Awake() //Null 오류 발생때문에 Start 대신 Awake 써서 TriggerEnter보다 먼저 발생되게 하기 
    {
        bulletPooling = GameObject.FindGameObjectWithTag("Manager").GetComponent<BulletPooling>();
        fireCont = GameObject.Find("Player").GetComponent<FireController>();

    }
    void Update()
    {
        if (fireCont.weaponNum != 1)
        {
            transform.Translate(new Vector3(0f, 0f, Bulletspeed));
            curTime += Time.deltaTime;
            if (curTime > 3f)
            {
                bulletPooling.Taeho_InsertQueue(gameObject);
                curTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("큐로 집어넣기" + other.name);
            bulletPooling.Taeho_InsertQueue(gameObject);
            curTime = 0;
        }
    }
}
