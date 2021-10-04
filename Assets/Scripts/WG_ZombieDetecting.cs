using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WG_ZombieDetecting : MonoBehaviour
{
    public List<GameObject> zombies;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
			if (zombies[i].GetComponent<NavMesh_EenmyController>().enemyhp > 0)
			{
                zombies[i].transform.LookAt(new Vector3(transform.position.x, zombies[i].transform.position.y, transform.position.z));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            zombies.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            zombies.Remove(other.gameObject);
        }
    }
}
