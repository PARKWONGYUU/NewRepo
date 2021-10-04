using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovMove : MonoBehaviour
{
    public GameObject molotovFire;
    public Rigidbody molotovRigid; //수류탄 리지드바디
    public float grenUpSpeed = 14f; // 수류탄 속도
    public float grenForSpeed = 8f; // 수류탄 속도
    public float curTime;
    public float coolTime = 5.5f;
    public bool getReady;
    public float attackCoolTime = 0.417f;
    public float attackCurTime = 0;
    public bool isCoolTime;

    public GameObject molotov;
    public GameObject cut;
    public WG_Player player;
    public GameObject boomMaker;
    void Start()
    {
        molotovRigid = GetComponent<Rigidbody>();
        cut = GameObject.Find("Cube");
        player = GameObject.Find("Player").GetComponent<WG_Player>();
        boomMaker = GameObject.Find("BoomMaker");
    }

    void Update()
    {
        if (player.isGrenade && player.isAttack && !getReady && !isCoolTime)
        {
            curTime += Time.deltaTime;
            if (curTime > 0.25f)
            {

                curTime = 0;
                GameObject shoot = Instantiate(molotov, boomMaker.transform.position, boomMaker.transform.rotation);
                shoot.GetComponent<MolotovMove>().getReady = true;
                molotov.GetComponent<Grenades>().itemCount--;
                isCoolTime = true;
                if (molotov.GetComponent<Grenades>().itemCount == 0)
                {
                    player.GetComponent<WG_PlayerEquipment>().usableWeapons.Remove(gameObject);
                    molotov.GetComponent<Grenades>().Btn.text = "None";
                    gameObject.SetActive(false);
                }
            }
        }
        if(isCoolTime)
        {
            attackCurTime += Time.deltaTime;
            if(attackCurTime >= attackCoolTime)
            {
                attackCurTime = 0;
                isCoolTime = false;
            }
        }

        if (getReady)
        {
            molotovRigid.constraints = RigidbodyConstraints.None;
            curTime += Time.deltaTime;
            if (curTime < 1)
            {
                molotovRigid.AddForce(cut.transform.forward * grenForSpeed * Time.deltaTime * 2, ForceMode.Impulse);
                molotovRigid.AddForce(cut.transform.up * -grenUpSpeed * Time.deltaTime * 2, ForceMode.Impulse);
            }
            if (curTime < coolTime / 2.2)
            {
                molotovRigid.AddForce(cut.transform.forward * grenForSpeed * Time.deltaTime);
                molotovRigid.AddForce(cut.transform.up * grenUpSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            grenForSpeed = 0f;
            grenUpSpeed = 0f;
            molotovRigid.useGravity = false;
            molotovRigid.isKinematic = true;
        }
    }
}
