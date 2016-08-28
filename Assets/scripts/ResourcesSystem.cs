using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Resource{
	public float value = 0;
	public float poplMult = 0;
	public float multiplicator = 1;
	public float clickCost = 1;

	public float withMultCost { get { return clickCost * multiplicator * ResourcesSystem.instance.GlobalMult; } }

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
	public float GlobalMult = 1f;
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
			if (resName == "Cookies") visibleResName = "Omnomnom";
			if (resName == "Humans") visibleResName = "Chaka";
			if (resName == "Science") visibleResName = "Mmmmmm";

		}
		Stock[resName].labelText.text = visibleResName + ":\n" + Age.WrapInt(visibleValue);
		
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
		AddNewResource("Science", .1f, 1000000000);

		while (true)
		{
			if (PauseSystem.inPause)
			{
				yield return new WaitForEndOfFrame();
				continue;
			}

			yield return new WaitForSeconds(GlobalTimer);
			isInNegativeMode = false;
			foreach (var res in Stock)
			{
				if (res.Key != "Science" && res.Key != "Humans")
				ChangeResource(res.Key, Stock[res.Key].poplMult * Stock["Humans"].value,
					Stock[res.Key].label.transform);

				if (Stock[res.Key].value + (Stock[res.Key].poplMult * Stock["Humans"].value) * 5 < 0)
					PauseSystem.instance.Pause(res.Key);
			}

			if (isInNegativeMode)
			{
				foreach (var e in techList.instance.emmiters)
					e.Emit(10);

				ChangeResource("Humans", -1 * (techList.researchedTeches.Count + Stock["Humans"].value * .3f),
					Stock["Humans"].label.transform);

				if (Stock["Humans"].value < 30)
					PauseSystem.instance.Pause("Humans");

				if (Stock["Humans"].value == 0)
					Age.instance.BadEnd();
			}
			else
			{
				ChangeResource("Science", Stock["Science"].poplMult * Stock["Humans"].value,
					Stock["Science"].label.transform);

				ChangeResource("Humans", Stock["Humans"].poplMult * Stock["Humans"].value,
					Stock["Humans"].label.transform);
			}
		}
	}

	public GameObject bonusPrefab;
	public GameObject bonusPanel;
	public float GiftCooldown = 10f;
	float giftSpeed = 160;

	public IEnumerator Bonuses()
	{
		while (true)
		{
			if (PauseSystem.inPause)
			{
				yield return new WaitForEndOfFrame();
				continue;
			}

			yield return new WaitForSeconds(GiftCooldown * Random.Range(0f,2f));

			var spawn = new Vector3(-10, Random.Range(10, Camera.main.pixelHeight - 10));
			var vector = new Vector3(giftSpeed, 0,0);
			if (Random.Range(0, 2) ==0)
			{
				spawn = new Vector3(Random.Range(10, Camera.main.pixelWidth - 10), Camera.main.pixelHeight+ 10, 0);
				vector = new Vector3(0, -giftSpeed, 0);
			}

			var gift = (GameObject)Instantiate(bonusPrefab, spawn, Quaternion.identity);
			gift.transform.SetParent(bonusPanel.transform);

			var anim = gift.GetComponent<ConstantAnimation>();
			anim.Torgue *= Random.Range(-2, 2);
			anim.moveVector = vector * Random.Range(.7f, 1.5f);

			gift.GetComponentInChildren<Image>().color = new Color(Random.Range(.6f,1), 
				Random.Range(.6f, 1), 
				Random.Range(.6f, 1));
		}
	}
}
