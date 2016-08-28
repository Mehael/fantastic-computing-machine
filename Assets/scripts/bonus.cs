using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class bonus : AlphaController {
	bool isActive = true;

	public void Cathced(string param) {
		if (isActive == false || PauseSystem.inPause) return;
		isActive = false;

		var stock = ResourcesSystem.instance.Stock;
		var luck = stock.Where( t => t.Key != "Humans")
			.ElementAt(UnityEngine.Random.Range(0, stock.Count - 1));
		ResourcesSystem.instance.ChangeResource(luck.Key, luck.Value.withMultCost * 100, transform);
		StartCoroutine(Destroing());

	}

	private IEnumerator Destroing()
	{
		yield return StartCoroutine(Hide());
		Destroy(gameObject);		
	}
}
