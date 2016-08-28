using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BotBrains : MonoBehaviour {
	static public BotBrains instance;
	static int population = 0;

	public GameObject botPanel;
	public GameObject manPrefab;
	public List<GameObject> mans = new List<GameObject>();
	public int newManThrethold = 10;
	public Transform popupCanvas;

	public List<string> beforeWords = new List<string>()
	{
		"Tumba",
		"Agu",
		"Duba",
		"Duda",
		"Kappa",
		"Bebe"
	};

	void Awake()
	{
		instance = this;
	}

	IEnumerator Start () {
		SpawnNewMan();

		while (true)
		{
			if (PauseSystem.inPause) yield return new WaitForEndOfFrame();
			yield return new WaitForSeconds(Random.Range(3, 12));
			var message = "";

			if (Age.age == "beforeLanguage")
				for (int i = 0; i < Random.Range(1, 3); i++)
					message += beforeWords[Random.Range(0, beforeWords.Count - 1)] + "-";
			StartCoroutine(
				TextBubles.instance.Say(mans[Random.Range(0, mans.Count - 1)].transform.position, message));
		}
		
	}
	
	public void SpawnNewMan()
	{
		population++;
		var go = (GameObject)Instantiate(manPrefab);
		go.transform.SetParent(botPanel.transform);
		mans.Add(go.transform.GetChild(0).gameObject);
	}
}
