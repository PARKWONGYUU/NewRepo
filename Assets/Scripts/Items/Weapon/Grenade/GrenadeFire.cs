using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFire : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Enemy")
        //{
        //}
        Debug.Log("5�� ������!"+other.name);
        Destroy(gameObject);
    }
}
