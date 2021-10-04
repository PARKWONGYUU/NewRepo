using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrionSmall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Enemy")
        //{
        //}
        Debug.Log("¾î±×·Î²û" + other.name);
       
    }
}
