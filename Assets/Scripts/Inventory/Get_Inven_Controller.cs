using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Get_Inven_Controller : MonoBehaviour
{

    private int inven_double;
    private GameObject inven_View;

    private GameObject inven_Add;
    public GameObject item_Name;
    public GameObject item_Move;
    public GameObject playerItemEquip;
    public GameObject playerItemEquip2;
    public GameObject playerItemUse;

    private void Awake()
    {
        playerItemEquip = GameObject.Find("PlayerItemEquip");
        playerItemEquip2 = GameObject.Find("PlayerItemEquip2");
        playerItemUse = GameObject.Find("PlayerItemUse");
    }
    void Start()
    {
        inven_View = GameObject.Find("Get_Inventory_Ime");
        inven_Add = GameObject.Find("Content_Inven");
        item_Move = GameObject.Find("Item_Move");
        inven_View.SetActive(false);
        item_Move.SetActive(false);
        playerItemEquip.SetActive(false);
        playerItemEquip2.SetActive(false);
        playerItemUse.SetActive(false);
    }

   
    void Update()
    {
        
    }
    public void AN_Inven_But()
    {
        if (inven_double <= 0)
        {
            inven_View.SetActive(true);
            inven_double++;
        }
        else if (inven_double >= 1)
        {
            inven_View.SetActive(false);
            inven_double = 0;
        }
    }
}
