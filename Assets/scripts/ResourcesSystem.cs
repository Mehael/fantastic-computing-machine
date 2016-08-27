using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Resource{
	public float value = 0;
	public float poplMult = 0;
	public float multiplicator = 1;
	public float clickCost = 1;

	public Text labelText;
	public GameObject label;

	public Resource(GameObject newlabel, float startIncome, float startValue)
	{
		label = newlabel;
		value = startValue;
		poplMult = startIncome;
		labelText = label.GetComponent<Text>();
		labelText.text = "New: 0";
	}
}

public class ResourcesSystem : MonoBehaviour {
	public static ResourcesSystem instance;
	public float GlobalTimer = 1f;
	public float WorkerTimer = 1f;
	public bool isInNegativeMode = false;

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


		if (newValue < 0)
		{
			newValue = 0;
			isInNegativeMode = true;
		}
		var visibleValue = Mathf.FloorToInt(newValue);
		if (Mathf.FloorToInt(Stock[resName].value) != visibleValue) {
			var visibleDelta = Mathf.FloorToInt(delta);
			if (visibleDelta == 0)
				visibleDelta = (int)Mathf.Sign(delta);

			FloatingNumbers.instance.CreateTooltip(visibleDelta, coords);
		}

		Stock[resName].value = newValue;
		var visibleResName = resName;
		if (Age.age == "beforeLanguage") {
			if (resName == "Food") visibleResName = "Omnomnom";
			if (resName == "Humans") visibleResName = "Chaka";
			if (resName == "Science") visibleResName = "Mmmmmm";

		}
		Stock[resName].labelText.text = visibleResName + ":  " + visibleValue;
		
	}

	public void AddNewResource(string name, float startIncome = 0, float startValue = 0)
	{
		if (Stock.ContainsKey(name)) return;

		var go = Instantiate<GameObject>(resourceLabelPrefab);
		go.transform.SetParent(stockPanel.transform);
		Stock.Add(name, new Resource(go, startIncome, startValue));
	}


	IEnumerator Start()
	{
		AddNewResource("Humans", 0, 1);
		AddNewResource("Science", .1f, 100);

		while (true)
		{
			yield return new WaitForSeconds(GlobalTimer);
			isInNegativeMode = false;
			foreach (var res in Stock)
			{
				if (res.Key != "Science")
				ChangeResource(res.Key, Stock[res.Key].poplMult * Stock["Humans"].value,
					Stock[res.Key].label.transform);
			}

			if (isInNegativeMode)
			{
				ChangeResource("Humans", techList.researchedTeches.Count * -10,
					Stock["Humans"].label.transform);
				if (Stock["Humans"].value == 0)
					Age.instance.BadEnd(); 
			} else
				ChangeResource("Science", Stock["Science"].poplMult * Stock["Humans"].value,
					Stock["Science"].label.transform);

		}
	}
}
