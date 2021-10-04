using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMaker : MonoBehaviour
{
    public GameObject grenade;
    public GameObject molotov;
    public GameObject carrion;
    public int test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaeHo_Grenade()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(test == 0)
            {
                GameObject instantGrenade = Instantiate(grenade, transform.position, transform.rotation);
            }
            if (test == 1)
            {
                GameObject instantGrenade = Instantiate(molotov, transform.position, transform.rotation);

            }
            if (test == 2)
            {
                GameObject instantGrenade = Instantiate(carrion, transform.position, transform.rotation);
            }

        }
    }
}
