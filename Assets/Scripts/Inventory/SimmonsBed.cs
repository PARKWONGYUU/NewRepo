using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimmonsBed : MonoBehaviour
{
	public GameObject sleepButton; // ���� ��ư
	public GameObject sleepBlind; // ������ư ������ ����ε�

	public float FadeTime = 2f; // Fadeȿ�� ����ð�

	Image fadeImg; // ������ư ������ ����ε�

	private bool playerSleepState = false; // �÷��̾� �ڴ��� Ȯ��
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
			Debug.Log("�÷��̾� �Ƿε� 0");
			OutStartFadeAnim();
			sleepBlind.SetActive(false);
			playerSleepState = false;
		}
		else if (playerSleepState)
		{
			player.playerTired -= 0.1f;
			Debug.Log("����");
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
			Debug.Log("�ߺ���");
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

			// Debug.Log("������" + fadecolor.a);
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
			Debug.Log("�߼� ����");
			sleepButton.SetActive(false);
		}
	}
}

