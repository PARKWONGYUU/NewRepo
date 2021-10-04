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

    void Awake() //Null ���� �߻������� Start ��� Awake �Ἥ TriggerEnter���� ���� �߻��ǰ� �ϱ� 
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
            Debug.Log("ť�� ����ֱ�" + other.name);
            bulletPooling.Taeho_InsertQueue(gameObject);
            curTime = 0;
        }
    }
}
