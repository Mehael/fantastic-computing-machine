using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class techList : MonoBehaviour {
	static public techList instance;
	static public Dictionary<string, Tech> teches = new Dictionary<string, Tech>()
	{
		{ "Language" , new LangTech() },
		{ "Sex",  new SexTech() },
		{ "Club", new JustTech("Club", 5, new List<string>(), "Food", 2, "BamBam") },
		{ "Hunting", new newTaskTech("Hunting", 15, new List<string>() { "Club" }, "Hunting", "Food")},
		{ "Fire", new newTaskTech("Fire", 15, new List<string>() { "Sex" }, "Search Firewood", "Wood", 25, -.5f)},
		{ "Clothing", new WorkerTech("Clothing", 5, new List<string>(), .9f, "Uhhrrr") },
		{ "Money", new newTaskTech("Money", 15, new List<string>() { "Fire" }, "Cookie Diggings", "Cookie", 99, -.01f)},
		{ "Domestication", new newTaskTech("Domestication", 20, new List<string>() { "Fire" }, "Cats Breeding", "Cats", 500, -.1f)},
		{ "Industrialization", new JustTech("Industrialization", 20, new List<string>() { "Domestication", "Money" },
			"Cats", 100)},
		{ "Electricity", new newTaskTech("Electricity", 20, new List<string>() {"Domestication", "Money"}, "Powerplants", "Energy", 500, -.1f)},
		{ "Internet", new JustTech("Internet", 20, new List<string>() { "Industrialization", "Electricity" },
			"Cats", 1000)},

		{ "Nuclear Tech", new JustTech("Nuclear Tech", 20, new List<string>() { "Industrialization" },
			"Energy", 1000)},
		{ "Terraforming", new WorkerTech("Terraforming", 20, new List<string>() { "Nuclear Tech" }, .1f)},
		{ "Haven Project", new HavenTech()},
	};
	static public List<string> researchedTeches = new List<string>();

	public GameObject TechBn;
	public GameObject TechPanel;

	void Awake()
	{
		instance = this;
	}

	IEnumerator Start () {
		CheckUnlock();

		while (true)
		{
			yield return new WaitForSeconds(1f);
			CheckCosts();
		}
	}

	public void CheckCosts()
	{
		foreach (var t in teches)
			if (t.Value.bn != null)
				t.Value.bn.interactable = t.Value.avaliableBuyCheck();
	}

	public void CheckUnlock()
	{
		foreach (var t in teches)
			if (t.Value.bn == null && t.Value.unlockCheck())
				UnlockTech(t.Key);
	}

	public void ResearchClick(string techName)
	{
		researchedTeches.Add(techName);
		CheckUnlock();

		Destroy(teches[techName].bn.gameObject);
		ResourcesSystem.instance.Stock["Science"].value -= teches[techName].scienceCost;
		teches[techName].Buy();
		teches.Remove(techName);
	}

	public void UnlockTech(string TechName)
	{
		var newTech = teches[TechName];

		newTech.bn = Instantiate(TechBn).GetComponent<Button>();
		newTech.bn.transform.SetParent(TechPanel.transform);

		var text = newTech.name;
		if (Age.age == "beforeLanguage" && newTech.maskName != null)
		{
			text = newTech.maskName;
			Age.instance.abrvgl.Add(newTech.bn.GetComponentInChildren<Text>(), newTech.name 
				+ " : " + newTech.scienceCost);
		}
		newTech.bn.GetComponentInChildren<Text>().text = text
			+ " : " + newTech.scienceCost;
		newTech.bn.name = newTech.name;
		CheckCosts();
	}
}


public class Tech {
	public int scienceCost;
	public List<string> prereqTeches = new List<string>();
	public string name;
	public string maskName;
	public Button bn;

	public bool avaliableBuyCheck()
	{
		if (ResourcesSystem.instance.Stock.ContainsKey("Science") == false) return false;
		return ResourcesSystem.instance.Stock["Science"].value >= scienceCost;
	}

	public bool unlockCheck()
	{
		var allOk = true;
		foreach(var t in prereqTeches)
		{
			if (techList.researchedTeches.Contains(t)==false)
				allOk = false;
		}

		return allOk;
	}

	public virtual void Buy() {}
}

public class LangTech: Tech
{
	public LangTech()
	{
		name = "Language";
		scienceCost = 5;
	}

	public override void Buy()
	{
		Age.instance.changeAge("lang");
	}
}

public class HavenTech : Tech
{
	public HavenTech()
	{
		prereqTeches.Add("Terraforming");
		name = "Haven Project";
		scienceCost = 5;
	}

	public override void Buy()
	{
		Age.instance.GoodEnd();
	}
}

public class JustTech : Tech
{
	int addClickPower;
	string techName;

	public JustTech(string name, int scienceCost, List<string> prereqTeches, string techName, 
		int addClickPower, string maskName = null)
	{
		this.prereqTeches = prereqTeches;
		this.name = name;
		this.scienceCost = scienceCost;
		this.maskName = maskName;
		this.techName = techName;
		this.addClickPower = addClickPower;
	}

	public override void Buy()
	{
		ResourcesSystem.instance.Stock[techName].clickCost += addClickPower;
	}

}

public class WorkerTech : Tech
{
	float newTimerValue;

	public WorkerTech(string name, int scienceCost, List<string> prereqTeches, 	float newTimerValue, string maskName = null)
	{
		this.prereqTeches = prereqTeches;
		this.name = name;
		this.scienceCost = scienceCost;
		this.maskName = maskName;
		this.newTimerValue = newTimerValue;
	}

	public override void Buy()
	{
		ResourcesSystem.instance.WorkerTimer = newTimerValue;
	}

}

public class SexTech : Tech
{
	public SexTech()
	{
		prereqTeches.Add("Language");
		name = "Sex";
		scienceCost = 10;
	}


	public override void Buy()
	{
		TaskSystem.instance.AddNewTask("Humans", "Babies Delivery", 0, 0);
		ResourcesSystem.instance.Stock["Humans"].poplMult = .2f; 
	}
}

public class newTaskTech : Tech
{
	string tName;
	string tType;
	int startValue;
	float poplMult;

	public newTaskTech(string name, int scienceCost, List<string> prereqTeches, 
		string newTaskName, string newTaskType, int startValue=0, float poplMult=1)
	{
		this.prereqTeches = prereqTeches;
		this.name = name;
		this.scienceCost = scienceCost;
		this.tName = newTaskName;
		this.tType = newTaskType;
		this.startValue = startValue;
		this.poplMult = poplMult;
	}

	public override void Buy()
	{
		TaskSystem.instance.AddNewTask(tType, tName, startValue, poplMult);
	}
}