using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovFire : MonoBehaviour
{
    //csv ��� ���� AttackPower, AttackRange AttackSpeed(coolTime)
    public bool tri;
    public float curTime;
    public float coolTime = 1f;
    public int damage = 3;
    public float destroyCurlTime;
    public float destroyCoolTime = 15f;
    void Start()
    {
        
    }
    void Update()
    {
        destroyCurlTime += Time.deltaTime;
        if(destroyCurlTime > destroyCoolTime)
        {
            Destroy(transform.parent.gameObject);
        }
        if(tri)
        {
            curTime += Time.deltaTime;
            if(curTime > coolTime)
            {
                //Debug.Log(damage + "�� ������!");
                curTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Enemy")
        //{
        //}
        tri = true;
        Debug.Log("��Ҵ�");
    }
    private void OnTriggerExit(Collider other)
    {
        tri = false;
        Debug.Log("���� �ȴ�´�");
        curTime = 0;
    }
}
