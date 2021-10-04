using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling instance;
    public GameObject bulletsPrefab = null; // ������ ������
    public Queue<GameObject> myQueue = new Queue<GameObject>(); // ��ü�� ������ ť
    public int numOfBullet = 30;//
    public GameObject bulletMaker;
    public FireController fireCont;

    void Start()
    {
        instance = this;
        
        for (int i = 0; i < numOfBullet; i++) // �̸� 100���� ��ü �̸� ����
        {
            GameObject bullet = Instantiate(bulletsPrefab, bulletMaker.transform.position, Quaternion.Euler(90, 0, 0));
            myQueue.Enqueue(bullet);
            bullet.transform.position = bulletMaker.transform.position;
            bullet.transform.rotation = bulletMaker.transform.rotation;
            bullet.SetActive(false);
        }

    }

    public void Taeho_InsertQueue(GameObject In_bullet) // ����� ��ü�� Pool(ť)�� �ݳ���Ű�� �Լ�
    {
        myQueue.Enqueue(In_bullet);
        In_bullet.SetActive(false);
        In_bullet.transform.position = bulletMaker.transform.position;
        In_bullet.transform.rotation = bulletMaker.transform.rotation;
    }

    public GameObject Taeho_GetQueue() // ����� ��ü�� Pool(ť)���� ������ �Լ�
    {
        GameObject Out_bullet = myQueue.Dequeue();
        Out_bullet.transform.position = bulletMaker.transform.position;
        Out_bullet.transform.rotation = bulletMaker.transform.rotation;
        
        Out_bullet.SetActive(true);
        return Out_bullet;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            myQueue.Enqueue(gameObject);
        }
    }*/
}
