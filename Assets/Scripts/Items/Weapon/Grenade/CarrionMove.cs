using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrionMove : MonoBehaviour
{
    public Rigidbody CarrRigid; //수류탄 리지드바디
    public float CarrUpSpeed = 14f; // 수류탄 속도
    public float CarrForSpeed = 8f; // 수류탄 속도
    public float curTime;
    public float coolTime = 5.5f;
    public int test;
    public bool getReady;
    public float attackCoolTime = 0.417f;
    public float attackCurTime = 0;
    public bool isCoolTime;

    public GameObject carrion;
    public GameObject cut;
    public WG_Player player;
    public GameObject boomMaker;
    void Start()
    {
        CarrRigid = GetComponent<Rigidbody>();
        cut = GameObject.Find("Cube");
        player = GameObject.Find("Player").GetComponent<WG_Player>();
        boomMaker = GameObject.Find("BoomMaker");
    }

    void Update()
    {
        if(player.isGrenade && player.isAttack && !getReady)
        {
            curTime += Time.deltaTime;
            if (curTime > 0.25f)
            {

                curTime = 0;
                GameObject shoot = Instantiate(carrion, boomMaker.transform.position, boomMaker.transform.rotation);
                shoot.GetComponent<CarrionMove>().getReady = true; 
                carrion.GetComponent<Grenades>().itemCount--;
                isCoolTime = true;
                if (carrion.GetComponent<Grenades>().itemCount == 0)
                {
                    player.GetComponent<WG_PlayerEquipment>().usableWeapons.Remove(gameObject);
                    carrion.GetComponent<Grenades>().Btn.text = "None";
                    gameObject.SetActive(false);
                }
            }
        }
        if (isCoolTime)
        {
            attackCurTime += Time.deltaTime;
            if (attackCurTime >= attackCoolTime)
            {
                attackCurTime = 0;
                isCoolTime = false;
            }
        }
        if (getReady)
        {
            CarrRigid.constraints = RigidbodyConstraints.None;

            curTime += Time.deltaTime;
            if (curTime < 1)
            {
                CarrRigid.AddForce(cut.transform.forward * CarrForSpeed * Time.deltaTime * 1.5f, ForceMode.Impulse);
                CarrRigid.AddForce(cut.transform.up * CarrUpSpeed * Time.deltaTime * 1.2f, ForceMode.Impulse);
            }
            if (curTime < coolTime / 2.8f)
            {
                CarrRigid.AddForce(cut.transform.forward * CarrForSpeed * Time.deltaTime);
                //CarrRigid.AddForce(Vector3.up * CarrUpSpeed*Time.deltaTime);

            }
            if (curTime > coolTime)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                test++;
            }
            if (curTime > coolTime + 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
