using UnityEngine;
using System.Collections;

public class soundSystem : MonoBehaviour {
	AudioSource audioPlayer;

	int[] clicksBySeconds;
	public int deep = 8;
	public float average;

	float baseGiftCooldown;
	// Use this for initialization
	void Awake () {
		audioPlayer = GetComponent<AudioSource>();
	}
	
	IEnumerator Start()
	{
		baseGiftCooldown = ResourcesSystem.instance.GiftCooldown;
		clicksBySeconds = new int[deep];
		while (true)
		{
			yield return new WaitForSeconds(1f);
			average = 0;
			for (int i= deep-1; i>0; i--)
			{
				average += clicksBySeconds[i];
				clicksBySeconds[i] = clicksBySeconds[i - 1];
			}

			clicksBySeconds[0] = 0;
			average = average / deep;

			audioPlayer.pitch = 1f + average * .05f;

			float giftMult = ((average / 4) - 1) * (-1);
			if (giftMult < 0) giftMult = 0;
			ResourcesSystem.instance.GiftCooldown = giftMult * baseGiftCooldown + 0.1f;
		}
	}

	void Update () {
		if (Input.GetMouseButtonDown(0))
			clicksBySeconds[0]++;
	
	}
}
