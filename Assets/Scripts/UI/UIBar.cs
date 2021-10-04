using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBar : MonoBehaviour
{
    public WG_Player player;
    public GameObject hpBar;
    public GameObject EnduranceBar;
   
    public int playerMaxHP;
    
    public int playerMaxER;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<WG_Player>();
        
        playerMaxHP = (int)player.playerMaxHP;
        playerMaxER = (int)player.playerMaxEndurance;


    }

    void Update()
    {
        hpBar.GetComponent<Slider>().value = player.playerHP / 100;
		if (player.playerHP >= 0)
		{
            hpBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (int)player.playerHP + "/" + playerMaxHP;
        }
        else
		{
            hpBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 0 + "/" + playerMaxHP;
        }
        // hpBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (int)player.playerHP + "/" +playerMaxHP;
        EnduranceBar.GetComponent<Slider>().value = player.playerEndurance/100;
        EnduranceBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (int)player.playerEndurance + "/" + playerMaxER;

    }
}
