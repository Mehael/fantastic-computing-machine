using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour {
	static public PauseSystem instance;
	static public bool inPause = false;
	public GameObject pausePanel;
	public Text textbox;
	public float Cooldown = 5f;
	public float timer = 0;

	void Awake () {
		instance = this;
	}
	
	public void Pause(string resType)
	{
		if (timer > 0 || resType=="Science") return;
		inPause = true;

		textbox.text = "We will lost all our "+resType+" soon";
		pausePanel.gameObject.SetActive(true);
	}

	void Update()
	{
		if (timer > 0) timer -= Time.deltaTime;
		if (inPause == false) return;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			inPause = false;
			timer = Cooldown;
			pausePanel.gameObject.SetActive(false);
		}
		
	}
}