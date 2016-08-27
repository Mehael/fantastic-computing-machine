using UnityEngine;
using System.Collections;

public class NumberTooltips : AlphaController {

	IEnumerator Start () {
		yield return StartCoroutine(Hide());
		Destroy(gameObject);
	}
	
}
