using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMove : MonoBehaviour
{

    public Rigidbody grenRigid; //수류탄 리지드바디
    public float grenUpSpeed = 8f; // 수류탄 속도
    public float grenForSpeed = 8f; // 수류탄 속도
    public float curTime;
    public float coolTime = 5.5f;
    public bool getReady;
    public float attackCoolTime = 0.417f;
    public float attackCurTime = 0;
    public bool isCoolTime;

    public GameObject grenade;
    public GameObject cut;
    public WG_Player player;
    public GameObject boomMaker;
    //public Vector3 dir;
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        cut = GameObject.Find("Cube");
        grenRigid = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<WG_Player>();
        boomMaker = GameObject.Find("BoomMaker");
    }

    void Update()
    {
        if (player.isGrenade && player.isAttack && !getReady)
        {
            curTime += Time.deltaTime;
            if (curTime > 0.25f)
            {

                curTime = 0;
                GameObject shoot = Instantiate(grenade, boomMaker.transform.position, boomMaker.transform.rotation);
                shoot.GetComponent<GrenadeMove>().getReady = true; 
                grenade.GetComponent<Grenades>().itemCount--;
                isCoolTime = true;
                if (grenade.GetComponent<Grenades>().itemCount == 0)
                {
                    player.GetComponent<WG_PlayerEquipment>().usableWeapons.Remove(gameObject);
                    grenade.GetComponent<Grenades>().Btn.text = "None";
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
            grenRigid.constraints = RigidbodyConstraints.None;
            curTime += Time.deltaTime;
            if (curTime < 1)
            {
                grenRigid.AddForce(cut.transform.forward * grenForSpeed * Time.deltaTime * 2, ForceMode.Impulse);
                grenRigid.AddForce(cut.transform.up * grenUpSpeed * Time.deltaTime * 2, ForceMode.Impulse);
            }
            if (curTime < coolTime / 2.2)
            {
                grenRigid.AddForce(cut.transform.forward * grenForSpeed * Time.deltaTime);
                grenRigid.AddForce(cut.transform.up * grenUpSpeed * Time.deltaTime);
            }
            //grenForSpeed -= Time.deltaTime;
            if (curTime > coolTime)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            if (curTime > coolTime + 5)
            {
                //연기 이펙트 사라짐
                Destroy(gameObject);
            }
        }
    }



}
