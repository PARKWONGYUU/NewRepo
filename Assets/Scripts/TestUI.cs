using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestUI : MonoBehaviour
{

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = player.GetComponent<WG_Player>().playerClass.className;
        text2.text = player.GetComponent<WG_Player>().playerState.ToString();
        text3.text = player.GetComponent<WG_Player>().playerEndurance.ToString();
        if (player.GetComponent<WG_Player>().md_Exhaust)
            text4.text = "Å»Áø ¿Â";
    }
}
