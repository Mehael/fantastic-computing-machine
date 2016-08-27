using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Resource{
	public float value = 0;
	public float income = 0;
	public float multiplicator = 1;
	public float clickCost = 1;

	public Text labelText;
	public GameObject label;

	public Resource(GameObject newlabel, float startIncome, float startValue)
	{
		label = newlabel;
		value = startValue;
		income = startIncome;
		labelText = label.GetComponent<Text>();
		labelText.text = "New: 0";
	}
}

public class ResourcesSystem : MonoBehaviour {
	public static ResourcesSystem instance;
	public float GlobalTimer = 1f;
	public float WorkerTimer = 1f;
	public GameObject resourceLabelPrefab;
	public GameObject stockPanel;

	public Dictionary<string, Resource> Stock = new Dictionary<string, Resource>();

	void Awake()
	{
		instance = this;
	}

	public void ChangeResource(string resName, float delta, Transform spawn = null)
	{
		var newValue = Stock[resName].value + delta;
		var coords = Stock[resName].label.transform.position;
		if (spawn)
			coords = spawn.position;


		var visibleValue = Mathf.FloorToInt(newValue);
		if (Mathf.FloorToInt(Stock[resName].value) != visibleValue) {
			var visibleDelta = Mathf.FloorToInt(delta);
			if (visibleDelta == 0)
				visibleDelta = (int)Mathf.Sign(delta);

			FloatingNumbers.instance.CreateTooltip(visibleDelta, coords);
		}

		Stock[resName].value = newValue;
		Stock[resName].labelText.text = resName + ":  " + visibleValue;
		
	}

	public void AddNewResource(string name, float startIncome = 0, float startValue = 0)
	{
		var go = Instantiate<GameObject>(resourceLabelPrefab);
		go.transform.SetParent(stockPanel.transform);
		Stock.Add(name, new Resource(go, startIncome, startValue));
	}


	IEnumerator Start()
	{
		AddNewResource("Food", -2, 100);
		AddNewResource("Science", .1f);

		while (true)
		{
			yield return new WaitForSeconds(GlobalTimer);
			foreach(var res in Stock)
				ChangeResource(res.Key, Stock[res.Key].income, Stock[res.Key].label.transform);

		}
	}
}
