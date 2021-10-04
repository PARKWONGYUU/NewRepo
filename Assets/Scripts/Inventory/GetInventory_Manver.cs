using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GetInventory_Manver : MonoBehaviour
{
	public GameObject furniture_But; // 생성할 버튼(플레이어가 근처올시 생성)
	public GameObject get_furniture; // 생성한 버튼을 넣을 위치
	public GameObject moveAllButton;

	public GameObject get_ItemText; // 아이템 정보 보여줄 오브젝트
	public GameObject Item_Box; // 아이템 정보를 넣을 위치 
	public GameObject boxInvenButton; // 인벤토리 활성화 버튼 
	private bool enemyDaed;  //좀비 죽음

	public bool inven_button_On; // 인벤토리 온오프 확인

	[SerializeField] private List<string> item_name; //랜덤으로 뽑은 아이템 이름
	[SerializeField] private List<float> item_weight; // 아이템 무게
	private FurnitureCSV furnitureCSV;
	private Inven_ItemCSV inven_ItemCSV;
	public List<Material> material;

	private int num = 0; // 이름 체크용 1번
						 // private int nums1 = 0; // 이름 체크용 2번

	private int getItmeCount = 0; //개수 채크용

	public GameObject item_Move;
	public GameObject playerItemEquip;
	private GameObject inven_Add;
	public Inventory_Item_Search inven_Mgr;
	[SerializeField] Inventory_Item_Search inventory_Item_Search; // 클릭한 버튼
	[SerializeField] private List<int> get_Item_Num1;
	[SerializeField] private List<int> get_Item_Num2;
	[SerializeField] private int set_Num;
	[SerializeField] int i_Num;
	public int inven_item_B_num; // 생성된 가구의 앞에 붙는 번호
	public GameObject obj; // 생성되는 오른쪽 버튼
	public Inventory_Manager invenMgr;
	List<GameObject> objs; //생성 오른쪽 버튼 확인용
	public List<GameObject> item_Texts;


	private void Awake()
	{
		invenMgr = GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>();
		get_furniture = GameObject.Find("Content_Get_Item");
		Item_Box = GameObject.Find("Content_Get_Inven");
		furnitureCSV = GameObject.Find("Inven_Mgr").GetComponent<FurnitureCSV>();
		inven_ItemCSV = GameObject.Find("Inven_Mgr").GetComponent<Inven_ItemCSV>();
		inven_Add = GameObject.Find("Content_Inven");
		item_Move = GameObject.Find("Item_Move");
		inven_Mgr = GameObject.Find("Inven_Mgr").GetComponent<Inventory_Item_Search>();
		playerItemEquip = GameObject.Find("PlayerItemEquip");
		boxInvenButton = GameObject.FindGameObjectWithTag("GetButton");
		inventory_Item_Search = GameObject.Find("Inven_Mgr").GetComponent<Inventory_Item_Search>();
		moveAllButton = GameObject.Find("MoveAllButton");
	}
	void Start()
	{
		AN_GetFurniture();
		bool s = true;
		if (s)
		{
			AN_Set_Item();
			s = false;
		}
		AN_itmeOverlap();
		if (i_Num == 4)
		{
			gameObject.SetActive(false);
		}
		enemyDaed = true;
	}

	void Update()
	{

	}

	void AN_GetFurniture()
	{
		if (gameObject.CompareTag("Closet"))
		{
			i_Num = 0;
			gameObject.name = i_Num+ "F"+ inven_Mgr.invent_Nums + "Closet";
		}
		else if (gameObject.CompareTag("Drawer"))
		{
			i_Num = 2;
			gameObject.name = i_Num + "F"+inven_Mgr.invent_Nums + "Drawer";
		}
		else if (gameObject.CompareTag("Refrigerator"))
		{
			i_Num = 3;
			gameObject.name = i_Num + "F"+inven_Mgr.invent_Nums + "Refrigerator";
		}
		else if (gameObject.CompareTag("ZombieBOX"))
		{
			i_Num = 4;
			gameObject.name = i_Num + "F"+inven_Mgr.invent_Nums + "ZombieBOX";
		}
	}
	void AN_Random_Search(int num1, int num2, int num3, int count)
	{
		for (int i = 0; i < inven_ItemCSV.item_unique_Num1.Count; i++)
		{
			if (inven_ItemCSV.item_unique_Num1[i] == num1 || inven_ItemCSV.item_unique_Num1[i] == num2 || inven_ItemCSV.item_unique_Num1[i] == num3)
			{
				get_Item_Num1.Add((inven_ItemCSV.item_unique_Num1[i]));
				get_Item_Num2.Add(inven_ItemCSV.item_unique_Num2[i]);
				item_name.Add(inven_ItemCSV.item_Name[i]);
				item_weight.Add(inven_ItemCSV.item_Weight[i]);
			}
		}

		int idx0 = get_Item_Num1.IndexOf(num1);
		int idx1 = get_Item_Num1.IndexOf(num2);
		int idx2 = get_Item_Num1.IndexOf(num3);
		
		int rands = Random.Range(0, count);

		for (int i = 0; i < rands; i++)
		{
			int[] array = new int[] { num1, num2, num3 };		
			var rNum = Random.Range(0, array.Length);			
			int ran2;
			if (array[rNum] == num1)
			{
				ran2 = Random.Range(idx0, idx1 - 1);			
				AN_GetItem_Instantiate(idx0, ran2);
			}
			else if (array[rNum] == num2)
			{
				ran2 = Random.Range(idx1, idx2 - 1);			
				AN_GetItem_Instantiate(idx1, ran2);
			}
			else if (array[rNum] == num3)
			{
				ran2 = Random.Range(idx2, get_Item_Num2.Count);			
				AN_GetItem_Instantiate(idx2, ran2);
			}
		}
	}

	// 버튼 생성 및 정보 추가
	public void AN_GetItem_Instantiate(int num1, int num2)
	{
		item_Texts.Add(Instantiate(get_ItemText));
		item_Texts[set_Num].transform.SetParent(Item_Box.transform);
		item_Texts[set_Num].name = i_Num+"F" + inven_Mgr.invent_Nums + "B" + i_Num + "Item" + set_Num;
		item_Texts[set_Num].GetComponent<Item_Button_Info>().Item_Number1 = get_Item_Num1[num1];
		item_Texts[set_Num].GetComponent<Item_Button_Info>().Item_Number2 = get_Item_Num2[num2];
		item_Texts[set_Num].GetComponent<Item_Button_Info>().item_weight = item_weight[num2];
		item_Texts[set_Num].GetComponent<Item_Button_Info>().item_name = item_name[num2];
		item_Texts[set_Num].GetComponent<Item_Button_Info>().item_produce = true;
		//AN_Get_Item(num1, num2);

		getItmeCount = 1;
		item_Texts[set_Num].transform.GetChild(0).GetComponent<Text>().text = item_name[num2];
		item_Texts[set_Num].transform.GetChild(1).GetComponent<Text>().text = item_weight[num2] + "g";
		item_Texts[set_Num].transform.GetChild(2).GetComponent<Text>().text = getItmeCount + "개";

		item_Texts[set_Num].GetComponent<Button>().onClick.AddListener(() => { AN_Item_Get(); });
		item_Texts[set_Num].SetActive(false);
		set_Num++;

	}

	// 중복 아이템 채크
	public void AN_itmeOverlap()
	{
		for (int i = 0; i < item_Texts.Count; i++)
		{
			for (int j = 0; j < item_Texts.Count; j++)
			{
				if (item_Texts[i].GetComponent<Item_Button_Info>().Item_Number1 == item_Texts[j].GetComponent<Item_Button_Info>().Item_Number1
					&& item_Texts[i].GetComponent<Item_Button_Info>().Item_Number2 == item_Texts[j].GetComponent<Item_Button_Info>().Item_Number2)
				{
					getItmeCount++;
					item_Texts[i].transform.GetChild(2).GetComponent<Text>().text = getItmeCount + "개";
					item_Texts[i].GetComponent<Item_Button_Info>().item_Count = getItmeCount;

					if (item_Texts[i].GetComponent<Item_Button_Info>().item_Count > item_Texts[j].GetComponent<Item_Button_Info>().item_Count)
					{
						GameObject obj2 = item_Texts[j];
						item_Texts.Remove(item_Texts[j]);
						Destroy(obj2);
						//Debug.Log(obj2.GetComponent<Item_Button_Info>().item_name);
					}
				}
			}
			getItmeCount = 0;
		}
	}
	public void AN_Set_Item()
	{
		switch (i_Num)
		{
			case 0:
				AN_Random_Search(0, 1, 3, 3);
				//AN_itmeOverlap();
				inven_item_B_num = inven_Mgr.invent_Nums;
				inven_Mgr.invent_Nums++;

				break;
			case 1:
				AN_Random_Search(0, 1, 2, 3);
				// AN_itmeOverlap();
				inven_item_B_num = inven_Mgr.invent_Nums;
				inven_Mgr.invent_Nums++;

				break;
			case 2:
				AN_Random_Search(2, 4, 5, 3);
				// AN_itmeOverlap();
				inven_item_B_num = inven_Mgr.invent_Nums;
				inven_Mgr.invent_Nums++;

				break;
			case 3:
				AN_Random_Search(2, 6, 7, 3);
				AN_itmeOverlap();
				inven_item_B_num = inven_Mgr.invent_Nums;
				inven_Mgr.invent_Nums++;

				break;
			case 4:
				AN_Random_Search(0, 1, 2, 3);
				AN_itmeOverlap();
				inven_item_B_num = inven_Mgr.invent_Nums;
				inven_Mgr.invent_Nums++;

				break;
			default:
				break;
		}
	}

	void AN_Button_SetActive_OnOff()
	{
		for (int i = 0; i < inven_Mgr.get_Furniture.Count; i++)
		{

			for (int j = 0; j < inven_Mgr.get_Furniture[i].GetComponent<GetInventory_Manver>().item_Texts.Count; j++) // Refrigerator Drawer Closet

			{
				inven_Mgr.get_Furniture[i].GetComponent<GetInventory_Manver>().item_Texts[j].SetActive(false);
				inven_Mgr.get_Furniture[i].GetComponent<MeshRenderer>().material = material[1];
			}
			for (int s = 0; s < item_Texts.Count; s++)
			{
				gameObject.GetComponent<MeshRenderer>().material = material[2];
				item_Texts[s].SetActive(true);
			}
		}
	}
	public void AN_Furniture_But()
	{
		if (i_Num == 0)
		{
			AN_Button_SetActive_OnOff();
		}
		else if (i_Num == 2)
		{
			AN_Button_SetActive_OnOff();
		}
		else if (i_Num == 3)
		{
			AN_Button_SetActive_OnOff();
		}
		else if (i_Num == 4)
		{
			AN_Button_SetActive_OnOff();
		}
		moveAllButton.GetComponent<Button>().onClick.RemoveAllListeners();
		moveAllButton.GetComponent<Button>().onClick.AddListener(() => { ItemMove_All(); });
	}
	public void WG_ItemCombine(GameObject obj)
	{
		for (int i = 0; i < invenMgr.player_Inven.Count; i++)
		{
			if (invenMgr.player_Inven[i].GetComponent<Item_Button_Info>().Item_Number1 == obj.GetComponent<Item_Button_Info>().Item_Number1 && invenMgr.player_Inven[i].GetComponent<Item_Button_Info>().Item_Number2 == obj.GetComponent<Item_Button_Info>().Item_Number2)
			{
				invenMgr.player_Inven[i].GetComponent<Item_Button_Info>().item_Count += obj.GetComponent<Item_Button_Info>().item_Count;
				invenMgr.isCombine = true;
			}
		}
	}
	public void ItemMove_All()
	{
		while(item_Texts.Count > 0)
		{
			item_Texts[item_Texts.Count - 1].GetComponent<Button>().onClick.RemoveAllListeners();
			item_Texts[item_Texts.Count - 1].GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().WG_Inven_Item_Click(); });
			item_Texts[item_Texts.Count - 1].transform.SetParent(inven_Add.transform);
			WG_ItemCombine(item_Texts[item_Texts.Count - 1]);
			GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().AN_Player_Inven(item_Texts, item_Texts[item_Texts.Count - 1]);
		}
	}
	public void AN_Item_Move()
	{
		item_Move.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
		item_Move.SetActive(false);
		if (item_Move.transform.GetChild(2).GetComponent<Slider>().maxValue == item_Move.transform.GetChild(2).GetComponent<Slider>().value)
		{
			inventory_Item_Search.clickObj.GetComponent<Button>().onClick.RemoveAllListeners();
			inventory_Item_Search.clickObj.GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().WG_Inven_Item_Click(); });
			inventory_Item_Search.clickObj.GetComponent<Item_Button_Info>().inven_Num = GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().item_Child_Num;
			inventory_Item_Search.clickObj.transform.SetParent(inven_Add.transform);  //ok
			WG_ItemCombine(inventory_Item_Search.clickObj); //ok
			GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().AN_Player_Inven(item_Texts, inventory_Item_Search.clickObj);
			item_Texts.Remove(inventory_Item_Search.clickObj);
			item_Move.transform.GetChild(2).GetComponent<Slider>().value = 0;
			inventory_Item_Search.clickObj = null;
		}
		else if (item_Move.transform.GetChild(2).GetComponent<Slider>().value != 0 && item_Move.transform.GetChild(2).GetComponent<Slider>().value != item_Move.transform.GetChild(2).GetComponent<Slider>().maxValue)
		{
			GameObject obj = Instantiate(inventory_Item_Search.clickObj);
			obj.transform.SetParent(inven_Add.transform);
			obj.GetComponent<Item_Button_Info>().item_Count = (int)item_Move.transform.GetChild(2).GetComponent<Slider>().value;
			obj.transform.GetChild(2).GetComponent<Text>().text = obj.GetComponent<Item_Button_Info>().item_Count + "개";

			obj.GetComponent<Button>().onClick.RemoveAllListeners();
			obj.GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().WG_Inven_Item_Click(); });
			obj.GetComponent<Item_Button_Info>().inven_Num = GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().item_Child_Num;
			WG_ItemCombine(inventory_Item_Search.clickObj);
			GameObject.Find("Inven_Mgr").GetComponent<Inventory_Manager>().AN_Player_Inven(item_Texts, obj);
			inventory_Item_Search.clickObj.GetComponent<Item_Button_Info>().item_Count -= (int)item_Move.transform.GetChild(2).GetComponent<Slider>().value;
			inventory_Item_Search.clickObj.transform.GetChild(2).GetComponent<Text>().text = inventory_Item_Search.clickObj.GetComponent<Item_Button_Info>().item_Count + "개";
			item_Move.transform.GetChild(2).GetComponent<Slider>().value = 0;
			inventory_Item_Search.clickObj = null;
		}
		else
		{
			// Debug.Log("개수를 입력하시오");
			// Debug.Log(clickObj.name);
			// clickObj = null;
		}
	}
	public void AN_Item_Get()
	{
		item_Move.SetActive(true);
		inventory_Item_Search.clickObj = EventSystem.current.currentSelectedGameObject;
		item_Move.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { AN_Item_Move(); });
		item_Move.transform.GetChild(2).GetComponent<Slider>().maxValue = inventory_Item_Search.clickObj.GetComponent<Item_Button_Info>().item_Count;
		item_Move.transform.GetChild(2).GetComponent<Slider>().value = 1;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("PlayerSound"))
		{
			// boxInvenButton.SetActive(true);
			if (i_Num != 4)
			{
				// Debug.Log("생성1");
				gameObject.GetComponent<MeshRenderer>().material = material[1];
				obj = Instantiate(furniture_But);
				obj.transform.SetParent(get_furniture.transform);
				obj.transform.GetChild(0).GetComponent<Text>().text = furnitureCSV.furniture_Name[i_Num];
				obj.GetComponent<Button>().onClick.AddListener(() => { AN_Furniture_But(); });
				inventory_Item_Search.get_Furniture.Add(gameObject);
			}
			else if (i_Num == 4)
			{
				if (enemyDaed)
				{
					// Debug.Log(gameObject.name);
					gameObject.GetComponent<MeshRenderer>().material = material[1];
					obj = Instantiate(furniture_But);
					obj.transform.SetParent(get_furniture.transform);
					obj.transform.GetChild(0).GetComponent<Text>().text = furnitureCSV.furniture_Name[i_Num];
					obj.GetComponent<Button>().onClick.AddListener(() => { AN_Furniture_But(); });
					inventory_Item_Search.get_Furniture.Add(gameObject);
					enemyDaed = false;
				}
			}
			// GameObject.Find("Inven_Mgr").GetComponent<Inventory_Item_Search>().getFurnituresButton.Add(obj);
		}
	}

	public void AN_ItemDestroy()
	{
		for (int i = 0; i < item_Texts.Count; i++)
		{
			item_Texts[i].SetActive(false);
			inventory_Item_Search.get_Furniture.Remove(gameObject);
			if (inventory_Item_Search.get_Furniture.Count == 0)
			{
				// boxInvenButton.SetActive(false);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("PlayerSound"))
		{
			gameObject.GetComponent<MeshRenderer>().material = material[3];
			if (i_Num != 4)
			{
				Destroy(obj);
			}
			if (i_Num == 4)
			{
				Destroy(obj);
				enemyDaed = true;
			}
			AN_ItemDestroy();
			// GameObject.Find("Inven_Mgr").GetComponent<Inventory_Item_Search>().getFurnituresButton.Remove(obj);
		}
	}
}
