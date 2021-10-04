using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATT_Range : MonoBehaviour
{
    // public EnemyController parent;
    public bool searchPlayer = false;


    void Start()
    {
        

    }

    
    void Update()
    {
     
    }



	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ÀÎ½ÄÇÔ");
            searchPlayer = true;

        }
    }
}
