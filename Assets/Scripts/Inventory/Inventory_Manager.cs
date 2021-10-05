using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Inventory_Manager : MonoBehaviour
{
    
    public GameObject inven_View;
    private GameObject inven_Get_View;

    public GameObject playerItemEquip;
    public GameObject playerItemEquip2;
    public GameObject playerItemUse;
    public GameObject player;
    public GameObject checkUI;
    public GameObject invenClickObject;

    public GameObject itemMgr;

    private int inven_double = 0;
    public int item_Child_Num = 0;
    public int inven_Child_Num;
    public float invenWeight;
    public List<GameObject> player_Inven;
    public bool isCombine;

    public Text weightText;
    public Text btnText1;
    public Text btnText2;
    public Text btnText3;
    private void Awake()
    {
        weightText = GameObject.Find("Weight").GetComponent<Text>();
        itemMgr = GameObject.Find("ItemManager");
        inven_Get_View = GameObject.Find("Content_Inven");
        player = GameObject.Find("Player");
        playerItemEquip = GameObject.Find("PlayerItemEquip");
        playerItemEquip2 = GameObject.Find("PlayerItemEquip2");
        playerItemUse = GameObject.Find("PlayerItemUse");
        checkUI = GameObject.Find("CheckUI");
        inven_View = GameObject.Find("Inventory_Ime");
        playerItemEquip2.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        checkUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        WG_InvenListManager();
    }

    public void AN_Player_Inven(List<GameObject> obj,GameObject crk)
	{

        obj.Remove(crk);
        if (!isCombine)
        {
            player_Inven.Add(inven_Get_View.transform.GetChild(item_Child_Num).gameObject);
            player_Inven[item_Child_Num].name = player_Inven[item_Child_Num].transform.GetChild(0).GetComponent<Text>().text;
            item_Child_Num++;
        }
        else
        {
            player_Inven.Remove(crk);
            Destroy(crk);
            isCombine = false;
        }
    }


    public void AN_Inven_But()
	{
        if(inven_double <= 0)
		{
            inven_View.SetActive(true);
            inven_double++;
        }
        else if(inven_double >= 1)
		{
            inven_View.SetActive(false);
            inven_double = 0;
            if (playerItemEquip.activeInHierarchy)
            {
                playerItemEquip.SetActive(false);
            }
            else if (playerItemUse.activeInHierarchy)
            {
                playerItemUse.SetActive(false);
            }
            else if (playerItemEquip2.activeInHierarchy)
            {
                playerItemEquip2.SetActive(false);
            }
        }
    }

    public void WG_InvenListManager()
    {
        float weight = 0;
        for (int i = 0; i < player_Inven.Count; i++)
        {
            if (player_Inven != null)
            {
                weight += player_Inven[i].GetComponent<Item_Button_Info>().item_weight * player_Inven[i].GetComponent<Item_Button_Info>().item_Count;
                player_Inven[i].GetComponent<Item_Button_Info>().inven_Num = player_Inven.IndexOf(player_Inven[i]);
                player_Inven[i].transform.GetChild(2).GetComponent<Text>().text = player_Inven[i].GetComponent<Item_Button_Info>().item_Count + "°³";
            }
            if (player_Inven[i].GetComponent<Item_Button_Info>().item_Count <= 0)
            {
                GameObject temp = player_Inven[i];
                player_Inven.Remove(player_Inven[i]);
                inven_Child_Num--;
                Destroy(temp);
            }
        }
        /*weight += player.GetComponent<WG_PlayerEquipment>().cap.GetComponent<Armors>().weight;
        weight += player.GetComponent<WG_PlayerEquipment>().shoes.GetComponent<Armors>().weight;
        weight += player.GetComponent<WG_PlayerEquipment>().shirts.GetComponent<Armors>().weight;
        weight += player.GetComponent<WG_PlayerEquipment>().pants.GetComponent<Armors>().weight;*/
        if (invenWeight != weight)
        {
            invenWeight = weight;
        }
        weightText.text = "Total Weight : " + weight.ToString() + "g";
        if(player_Inven != null)
            item_Child_Num = player_Inven.Count;
        if (player.GetComponent<WG_Player>().noWeapon)
        {
            return;
        }
            btnText1.text = player.GetComponent<WG_PlayerEquipment>().usableWeapons[0].name;
            if (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count <= 1)
                return;
            btnText1.text = player.GetComponent<WG_PlayerEquipment>().usableWeapons[1].name;
            if (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count <= 2)
                return;
            btnText1.text = player.GetComponent<WG_PlayerEquipment>().usableWeapons[2].name;
    }

    public void WeaponQuickEquip1()
    {
        if(player.GetComponent<WG_PlayerEquipment>().usableWeapons[0] != null)
        {
            player.GetComponent<WG_PlayerEquipment>().weapon = player.GetComponent<WG_PlayerEquipment>().usableWeapons[0];
            player.GetComponent<WG_PlayerEquipment>().WG_WeaponEquipment();
        }
    }
    public void WeaponQuickEquip2()
    {
        if (player.GetComponent<WG_PlayerEquipment>().usableWeapons[1] != null)
        {
            player.GetComponent<WG_PlayerEquipment>().weapon = player.GetComponent<WG_PlayerEquipment>().usableWeapons[1];
            player.GetComponent<WG_PlayerEquipment>().WG_WeaponEquipment();
        }
    }

    public void WeaponQuickEquip3()
    {
        if (player.GetComponent<WG_PlayerEquipment>().usableWeapons[2] != null)
        {
            player.GetComponent<WG_PlayerEquipment>().weapon = player.GetComponent<WG_PlayerEquipment>().usableWeapons[2];
            player.GetComponent<WG_PlayerEquipment>().WG_WeaponEquipment();
        }
    }

    public void WG_Inven_Item_Click()
    {
        invenClickObject = EventSystem.current.currentSelectedGameObject;
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Item_Button_Info>().Item_Number1 >= 2 && EventSystem.current.currentSelectedGameObject.GetComponent<Item_Button_Info>().Item_Number1 <= 5)
        {
            inven_Child_Num = EventSystem.current.currentSelectedGameObject.GetComponent<Item_Button_Info>().inven_Num;
            playerItemEquip.SetActive(true);
        }
        if (invenClickObject.GetComponent<Item_Button_Info>().Item_Number1 == 0)
        {
            inven_Child_Num = invenClickObject.GetComponent<Item_Button_Info>().inven_Num;
            playerItemEquip2.SetActive(true);
        }
        if (invenClickObject.GetComponent<Item_Button_Info>().Item_Number1 == 6 || invenClickObject.GetComponent<Item_Button_Info>().Item_Number1 == 7)
        {
            inven_Child_Num = invenClickObject.GetComponent<Item_Button_Info>().inven_Num;
            playerItemUse.transform.GetChild(4).GetComponent<Slider>().value = 1;
            playerItemUse.SetActive(true);
        }
    }

    public void WG_ItemUse()
    {
        int count = 0;
        if (invenClickObject.GetComponent<Item_Button_Info>().Item_Number1 == 6)
        {
            for (int i = 0; i < itemMgr.GetComponent<FoodsData>().Foods.Count; i++)
            {
                if (itemMgr.GetComponent<FoodsData>().Foods[i].GetComponent<Foods>().subItemNum == invenClickObject.GetComponent<Item_Button_Info>().Item_Number2)
                {
                    playerItemUse.transform.GetChild(4).GetComponent<Slider>().maxValue = invenClickObject.GetComponent<Item_Button_Info>().item_Count;
                    count = (int)playerItemUse.transform.GetChild(4).GetComponent<Slider>().value;
                    player.GetComponent<WG_Player>().playerHungry += (int)itemMgr.GetComponent<FoodsData>().Foods[i].GetComponent<Foods>().hungry * count;
                    player.GetComponent<WG_Player>().playerThirsty += (int)itemMgr.GetComponent<FoodsData>().Foods[i].GetComponent<Foods>().thirst * count;
                    invenClickObject.GetComponent<Item_Button_Info>().item_Count -= count;
                    playerItemUse.SetActive(false);
                }
            }
        }
        if (invenClickObject.GetComponent<Item_Button_Info>().Item_Number1 == 7)
        {
            for (int i = 0; i < itemMgr.GetComponent<DrinksData>().drinks.Count; i++)
            {
                if (itemMgr.GetComponent<DrinksData>().drinks[i].GetComponent<Drinks>().subItemNum == invenClickObject.GetComponent<Item_Button_Info>().Item_Number2)
                {
                    playerItemUse.transform.GetChild(4).GetComponent<Slider>().maxValue = invenClickObject.GetComponent<Item_Button_Info>().item_Count;
                    count = (int)playerItemUse.transform.GetChild(4).GetComponent<Slider>().value;
                    player.GetComponent<WG_Player>().playerHungry += (int)itemMgr.GetComponent<DrinksData>().drinks[i].GetComponent<Drinks>().hungry * count;
                    player.GetComponent<WG_Player>().playerThirsty += (int)itemMgr.GetComponent<DrinksData>().drinks[i].GetComponent<Drinks>().thirst * count;
                    invenClickObject.GetComponent<Item_Button_Info>().item_Count -= count;
                    playerItemUse.SetActive(false);
                }
            }
        }
    }
    public void WG_AmorEquip()
    {
        if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 >= 0 && player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 <= 4)
        {
            for (int i = 0; i < itemMgr.GetComponent<ArmorsData>().caps.Count; i++)
            {
                if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == itemMgr.GetComponent<ArmorsData>().caps[i].GetComponent<Armors>().subItemNum)
                {
                    player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count--;
                    player.GetComponent<WG_PlayerEquipment>().cap = Instantiate(itemMgr.GetComponent<ArmorsData>().caps[i]);
                }
            }
        }
        if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 >= 5 && player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 <= 9)
        {
            for (int i = 0; i < itemMgr.GetComponent<ArmorsData>().shoes.Count; i++)
            {
                if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == itemMgr.GetComponent<ArmorsData>().shoes[i].GetComponent<Armors>().subItemNum)
                {
                    player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count--;
                    player.GetComponent<WG_PlayerEquipment>().shoes = Instantiate(itemMgr.GetComponent<ArmorsData>().shoes[i]);
                }
            }
        }
        if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 >= 10 && player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 <= 14)
        {
            for (int i = 0; i < itemMgr.GetComponent<ArmorsData>().shirts.Count; i++)
            {
                if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == itemMgr.GetComponent<ArmorsData>().shirts[i].GetComponent<Armors>().subItemNum)
                {
                    player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count--;
                    player.GetComponent<WG_PlayerEquipment>().shirts = Instantiate(itemMgr.GetComponent<ArmorsData>().shirts[i]);
                }
            }
        }
        if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 >= 15 && player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 <= 19)
        {
            for (int i = 0; i < itemMgr.GetComponent<ArmorsData>().pants.Count; i++)
            {
                if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == itemMgr.GetComponent<ArmorsData>().pants[i].GetComponent<Armors>().subItemNum)
                {
                    player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count--;
                    player.GetComponent<WG_PlayerEquipment>().pants = Instantiate(itemMgr.GetComponent<ArmorsData>().pants[i]);
                }
            }
        }
        playerItemEquip2.SetActive(false);
    }
    public void WG_ItemEquip()
    {
        if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number1 >= 2 && player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number1 <= 5)
        {

            int i = 0;
            switch (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number1)
            {
                case 2:
                    while (i < player.GetComponent<WG_PlayerEquipment>().weaponEquips.Count)
                    {
                        if (player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<OneHandedWeapons>() != null)
                        {
                            if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<OneHandedWeapons>().subItemNum)
                            {
                                if (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count >= 3)
                                {
                                    player.GetComponent<WG_PlayerEquipment>().usableWeapons.Remove(player.GetComponent<WG_PlayerEquipment>().usableWeapons[0]);
                                }
                                player.GetComponent<WG_PlayerEquipment>().usableWeapons.Add(player.GetComponent<WG_PlayerEquipment>().weaponEquips[i]);
                                player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count--;
                                switch (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count)
                                {
                                    case 1:
                                        WeaponQuickEquip1();
                                        break;
                                    case 2:
                                        WeaponQuickEquip2();
                                        break;
                                    case 3:
                                        WeaponQuickEquip3();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        i++;
                    }
                    break;
                case 3:
                    while (i < player.GetComponent<WG_PlayerEquipment>().weaponEquips.Count)
                    {
                        if (player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<TwoHandedWeapons>() != null)
                        {
                            if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<TwoHandedWeapons>().subItemNum)
                            {
                                if (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count >= 3)
                                {
                                    player.GetComponent<WG_PlayerEquipment>().usableWeapons.Remove(player.GetComponent<WG_PlayerEquipment>().usableWeapons[0]);
                                }
                                player.GetComponent<WG_PlayerEquipment>().usableWeapons.Add(player.GetComponent<WG_PlayerEquipment>().weaponEquips[i]);
                                player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count--;
                                switch (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count)
                                {
                                    case 1:
                                        WeaponQuickEquip1();
                                        break;
                                    case 2:
                                        WeaponQuickEquip2();
                                        break;
                                    case 3:
                                        WeaponQuickEquip3();
                                        break;
                                }
                            }
                        }
                        i++;
                    }
                    break;
                case 4:
                    while (i < player.GetComponent<WG_PlayerEquipment>().weaponEquips.Count)
                    {
                        if (player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Firearms>() != null)
                        {
                            if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Firearms>().subItemNum)
                            {
                                if (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count >= 3)
                                {
                                    player.GetComponent<WG_PlayerEquipment>().usableWeapons.Remove(player.GetComponent<WG_PlayerEquipment>().usableWeapons[0]);
                                }
                                player.GetComponent<WG_PlayerEquipment>().usableWeapons.Add(player.GetComponent<WG_PlayerEquipment>().weaponEquips[i]);
                                player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count--;
                                switch (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count)
                                {
                                    case 1:
                                        WeaponQuickEquip1();
                                        break;
                                    case 2:
                                        WeaponQuickEquip2();
                                        break;
                                    case 3:
                                        WeaponQuickEquip3();
                                        break;
                                }
                            }
                        }
                        i++;
                    }
                    break;
                case 5:
                    while (i < player.GetComponent<WG_PlayerEquipment>().weaponEquips.Count)
                    {
                        if (player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Grenades>() != null)
                        {
                            if (player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().Item_Number2 == player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Grenades>().subItemNum)
                            {
                                if (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count >= 3)
                                {
                                    player.GetComponent<WG_PlayerEquipment>().usableWeapons.Remove(player.GetComponent<WG_PlayerEquipment>().usableWeapons[0]);
                                }
                                player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Grenades>().itemCount = player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count;
                                player.GetComponent<WG_PlayerEquipment>().usableWeapons.Add(player.GetComponent<WG_PlayerEquipment>().weaponEquips[i]);
                                player_Inven[inven_Child_Num].GetComponent<Item_Button_Info>().item_Count = 0;

                                switch (player.GetComponent<WG_PlayerEquipment>().usableWeapons.Count)
                                {
                                    case 1:
                                        WeaponQuickEquip1();
                                        player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Grenades>().Btn = btnText1;
                                        break;
                                    case 2:
                                        WeaponQuickEquip2();
                                        player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Grenades>().Btn = btnText2;
                                        break;
                                    case 3:
                                        WeaponQuickEquip3();
                                        player.GetComponent<WG_PlayerEquipment>().weaponEquips[i].GetComponent<Grenades>().Btn = btnText3;
                                        break;
                                }
                            }
                        }
                        i++;
                    }
                    break;

            }

        }
        playerItemEquip.SetActive(false);
    }
    public void ItemThrowAway()
    {
        invenClickObject.GetComponent<Item_Button_Info>().item_Count = 0;
        checkUI.SetActive(false);
        if(playerItemEquip.activeInHierarchy)
        {
            playerItemEquip.SetActive(false);
        }
        else if (playerItemUse.activeInHierarchy)
        {
            playerItemUse.SetActive(false);
        }
        else if (playerItemEquip2.activeInHierarchy)
        {
            playerItemEquip2.SetActive(false);
        }
    }

    public void ThrowAwayCheckUI()
    {
        checkUI.SetActive(true);
    }
    public void CheckUIActiveFalse()
    {
        checkUI.SetActive(false);
    }
}
