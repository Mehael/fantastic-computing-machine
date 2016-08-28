using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {
	public static FloatingNumbers instance;
	public GameObject tooltipPrefab;
	public GameObject tooltipCanvas;
	public Color positiveColor;
	public Color negativeColor;

	void Awake()
	{
		instance = this;
	}

	public void CreateTooltip(int value, Vector2 position)
	{
		var go = (GameObject)Instantiate(tooltipPrefab, position, Quaternion.identity);
		go.transform.SetParent(tooltipCanvas.transform);
		var text = go.GetComponentInChildren<Text>();

		text.color = (value < 0) ?
			negativeColor :
			positiveColor;

		text.text = (value < 0) ? Age.WrapInt(value) : "+" + Age.WrapInt(value);
	}
}
