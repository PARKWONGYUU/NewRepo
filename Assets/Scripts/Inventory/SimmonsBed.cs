using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimmonsBed : MonoBehaviour
{
	public GameObject sleepButton; // 슬립 버튼
	public GameObject sleepBlind; // 슬립버튼 누르면 블라인드

	public float FadeTime = 2f; // Fade효과 재생시간

	Image fadeImg; // 슬립버튼 누르면 블라인드

	private bool playerSleepState = false; // 플레이어 자는지 확인
	public WG_Player player;

	float start;

	float end;

	float time = 0f;
	float time2 = 0f;


	bool isPlaying = false;

	private void Awake()
	{
		sleepButton = GameObject.Find("SleepButton");
		sleepBlind = GameObject.Find("SleepBlind");
		player = GameObject.Find("Player").GetComponent<WG_Player>();
		fadeImg = sleepBlind.GetComponent<Image>();
		sleepButton.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { SleepButtonClick(); });
	}
	void Start()
	{
		sleepButton.SetActive(false);
		// InStartFadeAnim();
		OutStartFadeAnim();

	}

	void Update()
	{
		if (player.playerTired >= 0f && !playerSleepState)
		{
			Debug.Log("플레이어 피로도 0");
			OutStartFadeAnim();
			sleepBlind.SetActive(false);
			playerSleepState = false;
		}
		else if (playerSleepState)
		{
			player.playerTired -= 0.1f;
			Debug.Log("들어옴");
			if (player.playerTired <= 0)
			{
				playerSleepState = false;
			}
		}
	}

	public void OutStartFadeAnim()

	{
		// sleepBlind.SetActive(true);
		if (isPlaying == true)

		{

			return;

		}

		start = 1f;

		end = 0f;

		StartCoroutine(fadeoutplayOut());
		time2 += Time.deltaTime;
	}

	public void InStartFadeAnim()
	{
		// sleepBlind.SetActive(true);
		if (isPlaying == true)

		{
			Debug.Log("중복됨");
			return;

		}
		start = 0f;

		end = 1f;
		StartCoroutine(fadeoutplayIn());
	}

	IEnumerator fadeoutplayIn()

	{

		isPlaying = true;

		Color fadecolor = fadeImg.color;
		time = 0f;
		fadecolor.a = Mathf.Lerp(start, end, time);
		while (fadecolor.a >= 0f)
		{

			time += Time.deltaTime / FadeTime;

			// Debug.Log("동작함" + fadecolor.a);
			fadecolor.a = Mathf.Lerp(start, end, time);

			fadeImg.color = fadecolor;
			//if (fadecolor.a <= 1f)
			//{
			//	sleepBlind.SetActive(false);
			//}
			yield return null;

		}
		isPlaying = false;
	}
	IEnumerator fadeoutplayOut()

	{

		isPlaying = true;

		Color fadecolor = fadeImg.color;
		time = 0f;
		fadecolor.a = Mathf.Lerp(start, end, time);
		while (fadecolor.a >= 0f)
		{

			time += Time.deltaTime / FadeTime;

			fadecolor.a = Mathf.Lerp(start, end, time);

			fadeImg.color = fadecolor;
			fadeImg.color = fadecolor;
			if (fadecolor.a == 0f)
			{
				sleepBlind.SetActive(false);
			}
			yield return null;

		}
		isPlaying = false;
	}

	public void SleepButtonClick()
	{
		if (player.playerTired >= 60)
		{
			playerSleepState = true;
			isPlaying = false;
			sleepBlind.SetActive(true);
			sleepButton.SetActive(false);
			InStartFadeAnim();
		}
		else
		{
			Debug.Log("잘수 없음");
			sleepButton.SetActive(false);
		}
	}
}

