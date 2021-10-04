using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance = null;

    public float _volume;
    public static AudioManager Instance()
    {
        return _instance;
    }
    public AudioClip bgm;
    public void PlaySfx(AudioClip sfx)
    {
        GetComponent<AudioSource>().PlayOneShot(sfx);
    }
    // Start is called before the first frame update
    void Start()
    {
		if (_instance == null)
		{
			_instance = this;
			GetComponent<AudioSource>().clip = bgm;
			GetComponent<AudioSource>().loop = true;
			GetComponent<AudioSource>().Play();
			GetComponent<AudioSource>().volume = _volume;

		}
	}
}
