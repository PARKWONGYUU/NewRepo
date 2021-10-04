using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepPlayer : MonoBehaviour
{
    public SimmonsBed bed;
    public GameObject player;
	// Start is called before the first frame update
	private void Awake()
	{
        bed = GameObject.Find("Inven_Mgr").GetComponent<SimmonsBed>();
        player = GameObject.Find("Player");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bed.sleepButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bed.sleepButton.SetActive(false);
        }
    }
}
