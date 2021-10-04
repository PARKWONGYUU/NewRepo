using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIPlayerClassBUT : MonoBehaviour
{

    public List<Sprite> playerJOB;
    public GameObject playerInfoBUT;
    public GameObject playerChoiceBUT;

    public GameObject playerSearch;
    // public GameObject classSelectUI;
    private WG_Player playerClass;

	private void Awake()
	{
        Application.targetFrameRate = 40;
        playerClass = GameObject.Find("Player").GetComponent<WG_Player>();

    }
    void Start()
    {
        playerSearch.SetActive(false);
        playerChoiceBUT.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void PlayerJobSearch(int i)
	{
        playerInfoBUT.GetComponent<Image>().sprite = playerJOB[i];
        playerSearch.SetActive(true);
        playerSearch.GetComponent<Image>().sprite = playerJOB[i];
        playerChoiceBUT.SetActive(true);
    }
    public void JobChoice()
	{
        Time.timeScale = 1;
        playerClass.classSelectUI.SetActive(false);
    }
}
