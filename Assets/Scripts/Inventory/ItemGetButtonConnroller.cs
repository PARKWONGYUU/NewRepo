using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemGetButtonConnroller : MonoBehaviour
{

    public GameObject boxInvenButton;
    public GameObject playerItemEquip;
    public GameObject playerItemEquip2;
    public GameObject playerItemUse;
    public Slider boxSlider;
    public Slider invenSlider;


    public Text boxCountText;
    public Text invenCountText;


    void Awake()
	{
        boxInvenButton = GameObject.Find("Item_Move");
        playerItemEquip = GameObject.Find("PlayerItemEquip");
        playerItemEquip2 = GameObject.Find("PlayerItemEquip2");
        playerItemUse = GameObject.Find("PlayerItemUse");
        boxCountText = GameObject.Find("BoxTextCount").GetComponent<Text>();
        boxSlider = GameObject.Find("CountBox").GetComponent<Slider>();
        invenSlider = GameObject.Find("CountPlayer").GetComponent<Slider>();
        invenCountText = GameObject.Find("PlayerTextCount").GetComponent<Text>();
    }

    void Start()
    {
     
    }

    void Update()
    {
        if (boxInvenButton)
        {
            boxCountText.text = "개수 : " + boxSlider.value;
        }
        if (playerItemUse)
        {
            invenCountText.text = "개수 : " + invenSlider.value;
        }
    }

    public void BoxItemButtonExit()
	{
        boxInvenButton.SetActive(false);
    }
    public void PlayerItemButtonExit()
    {
        playerItemEquip.SetActive(false);
        playerItemEquip2.SetActive(false);
        playerItemUse.SetActive(false);
    }
}
