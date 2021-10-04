using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertDeleteCeiling : MonoBehaviour
{
    public bool ceiling;
    public List<GameObject> ceilingObject = new List<GameObject>();
    public bool exist = false;
    public bool groundwalking = false;
    public float existCurTime;
    public float existCoolTime = 0.3f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (exist)
        {
            existCurTime += Time.deltaTime;
            if (existCurTime > existCoolTime)
            {
                ceilingObject[0].SetActive(true);
                ceilingObject.RemoveAt(0);
                exist = false;
                groundwalking = false;
                existCurTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            if(ceilingObject.Count >= 1)
                exist = true;
        }
        if (other.gameObject.tag == "Ceiling")
        {
            exist = false;
            existCurTime = 0;
            if (ceilingObject.Count >= 1)
            {
                for (int i = 0; i < ceilingObject.Count; i++)
                {
                    if (ceilingObject.Count - 1 == i)
                    {
                        ceilingObject[i].SetActive(true);
                        ceilingObject.RemoveAt(i);
                        Debug.Log("ÁöºØ »ý±è");
                    }
                }
            }   
            ceilingObject.Insert(0, other.gameObject.transform.GetChild(0).gameObject);
            Debug.Log("ÁöºØ ¾ø¾îÁü");
            ceilingObject[0].SetActive(false);
        }
        

    }
   
}
