using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBubles : AlphaController {
	static public TextBubles instance;
	public GameObject bubblesParent;
	public GameObject bubblePrefab;

	void Awake()
	{
		instance = this;
	}

	public IEnumerator Say(Vector2 coords, string message)
	{
		var bbl = (GameObject)Instantiate(bubblePrefab, coords, Quaternion.identity);
		bbl.GetComponentInChildren<Text>().text = message;
		bbl.transform.SetParent(bubblesParent.transform);

		var bblGraphfics = bbl.GetComponentsInChildren<Graphic>();
		foreach (var gr in bblGraphfics) Show(gr);
		yield return StartCoroutine(WaitBusy());
		foreach (var gr in bblGraphfics) Hide(gr);
		yield return StartCoroutine(WaitBusy());

		Destroy(bbl);
	}
}
