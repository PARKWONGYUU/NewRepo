using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameObject gameOptionBut;
    public GameObject gameOption;
    public AudioSource audioManger;
    public Slider soundSlider;

    // Start is called before the first frame update
    void Start()
    {
        audioManger = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        gameOption.SetActive(false);
        soundSlider.value = 0.1f;
    }
    // Update is called once per frame
    void Update()
    {
		if (gameOption)
		{
            VolumeControl();
        }
    }

    public void GameOptionBut()
	{
        Time.timeScale = 0; 
        gameOption.SetActive(true);
    }
    public void GameOptionOff()
	{
        Time.timeScale = 1;
        gameOption.SetActive(false);
    }
    void VolumeControl()
	{
       audioManger.volume = soundSlider.value;
    }
    public void GameExit()
	{
        Application.Quit();
    }
}
