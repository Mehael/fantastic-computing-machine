using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class techList : MonoBehaviour {
	static public techList instance;
	 public Dictionary<string, Tech> teches = new Dictionary<string, Tech>()
	{
		/* Tier 1
		 * Power = 1
		 * humans < 10
		 * prices = 5
		 * 
		 * population feature
		 */
		{ "Language" , new AgeTech("Language", 5, "lang", new List<string>()) },
		{ "Mathematics" , new AgeTech("Mathematics", 5, "math", new List<string>() {"Language"}) },
		{ "Reserching", new newTaskTech("Reserching", 1, new List<string>() { "Mathematics" }, "I'm doing science", "Science")},

		{ "Sex",  new SexTech() },
		{ "Family", new TierUnlock("Family", 10, new List<string>() { "Language" }) },

		{ "Clothing", new WorkerTech("Clothing", 7, new List<string>(), .9f, "Uhhrrr") },
		{ "Club", new JustTech("Club", 3, new List<string>(), "Food", 7, "BamBam") },

		/* Tier 2
		 * Power = 10
		 * humans < 100
		 * prices = 100
		 * 
		 * two resources
		 * worker feature
		 * 
		 */
		{ "Hunting", new newTaskTech("Hunting", 25, new List<string>() { "Club", "Family" }, "Hunting", "Food")},
		{ "Fire", new newTaskTech("Fire", 35, new List<string>() { "Sex", "Family" }, "Search Firewood", "Combustible", 500, -.5f)},

		{ "Leaders", new TierUnlock("Leaders", 100, new List<string>() { "Family" }) },

		//{ "Professions" , new ProfTech() },

		{ "Houses", new WorkerTech("Houses", 40, new List<string>() {"Family", "Clothing" }, .8f)},
		{ "Agriculture", new JustTech("Agriculture", 60, new List<string>() {"Club", "Houses" }, "Food", 40) },
		{ "Axes", new JustTech("Axes", 10, new List<string>() {"Fire" }, "Combustible", 40) },
	
		/* Tier 3
		 * Power = 100
		 * humans < 1 000
		 * prices = 1 000
		 * 
		 * cats feature
		 * 
		 */
		{ "Cats" , new FrendTech() },
		{ "Domestication", new newTaskTech("Domestication", 777, new List<string>() { "Cats", "Agriculture" }, "Cats Breeding", "Cats", 5000, -.1f)},

		{ "Government", new TierUnlock("Government", 1000, new List<string>() { "Leaders" }) },

		//FoodUp\2, CombusUp\2, CatsUp\10, WorkerUp
		{ "Bear mounts", new JustTech("Bear mounts", 500, new List<string>() {"Domestication" }, "Food", 400) },
		{ "Oil", new JustTech("Oil", 500, new List<string>() { "Leaders", "Axes"}, "Combustible", 400) },
		{ "Cat Farms", new JustTech("Cat Farms", 777, new List<string>() { "Leaders", "Domestication"}, "Cats", 100) },

		{ "Workmans", new WorkerTech("Workmans", 999, new List<string>() {"Family", "Clothing" }, .7f)},

		/* Tier 4
		 * Power = 1 000
		 * humans < 10 000
		 * prices = 100 000
		 * 
		 */
		{ "Electricity", new newTaskTech("Electricity", 2000, new List<string>() { "Government" }, "Powerplants", "Combustible", 500, -.1f)},

		{ "Industrialization", new TierUnlock("Industrialization", 100000, new List<string>() { "Government" }) },

		//FoodUp\4, CombusUp\6, CatsUp\20, WorkerUp
		{ "Medicine", new JustTech("Medicine", 700, new List<string>() {"Government"}, "Humans", 100) },

		{ "Fertilizers", new JustTech("Fertilizers", 7000, new List<string>() { "Government", "Bear mounts"}, "Food", 300) },
		{ "Natural gas", new JustTech("Natural gas", 5000, new List<string>() { "Government", "Oil"}, "Combustible", 120) },
		{ "Cats Video", new JustTech("Cats Video", 3000, new List<string>() { "Government", "Cat Farms"}, "Cats", 40) },

		{ "Books", new JustTech("Books", 1000, new List<string>() { "Government", "Reserching"}, "Science", 520) },

		{ "Guilds", new WorkerTech("Guilds", 9999, new List<string>() {"Workmans", "Government" }, .6f)},

		/* Tier 5
		 * Power = 10 000
		 * humans < 100 000
		 * prices = 1 000 000
		 * 
		 */
		{ "Ludum Dare", new newTaskTech("Ludum Dare", 200000, new List<string>() {  "Industrialization", "Internet" },
			"Game Jams", "Indie", 2000000, -1f)},

		{ "Union of nations", new TierUnlock("Union of nations", 1000000, new List<string>() { "Industrialization", "Ludum Dare" }) },

		//all stuff
		{ "Regeneration", new JustTech("Regeneration", 900000, new List<string>() {"Industrialization", "Medicine"}, "Humans", 1000) },

		{ "Greenlight", new JustTech("Greenlight", 123, new List<string>() {"Ludum Dare"}, "Indie", 45000) },
		{ "Cookie printer", new JustTech("Cookie printer", 700000, new List<string>() { "Government", "Bear mounts"}, "Food", 20000) },
		{ "Nuclear powerplant", new JustTech("Nuclear powerplant", 500000, new List<string>() { "Industrialization" }, "Combustible", 15000)},
		{ "Internet", new JustTech("Internet", 800000, new List<string>() { "Industrialization", "Electricity", "Guilds" }, "Cats", 5000)},

		{ "Googling", new WorkerTech("Googling", 999999, new List<string>() {"Workmans", "Government", "Internet" }, .4f)},

		/* Tier 6
		 * Power = 100 000 
		 * humans < 1 000 000
		 * prices = 10 000 000
		 * 
		 * stable waiting endgame
		 * 
		 */
		{ "Androids", new WorkerTech("Androids", 3000000, new List<string>() { "Union of nations" }, .1f)},
		{ "Shadow Control", new GlobalTech("Shadow Control", 3000000, new List<string>() { "Union of nations" }, 3f)},

		{ "Terraforming", new TierUnlock("Terraforming", 10000000, new List<string>() { "Union of nations" }) },
		{ "Haven Project", new HavenTech()},
	};
	public List<string> researchedTeches = new List<string>();

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
			yield return new WaitForEndOfFrame();
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

		ResourcesSystem.instance.Stock["Science"].value -= teches[techName].cost;
		teches[techName].Buy();

		Destroy(teches[techName].bn.gameObject);
		CheckParticles(techName);
		teches.Remove(techName);
	}


	public List<ParticleSystem> emmiters = new List<ParticleSystem>();
	public List<Material> particles = new List<Material>();

	void CheckParticles(string tech)
	{
		string newMat = null;
		if (tech == "Sex")
			foreach (var s in emmiters)
				s.Play();
		if (tech == "Fire")
			newMat = "Torch";
		if (tech == "Axes")
			newMat = "Axe";
		if (tech == "Bear mounts")
			newMat = "Wodka";
		if (tech == "Industrialization")
			newMat = "Gun";
		if (tech == "Nuclear powerplant")
			newMat = "Nuka";

		if (newMat!= null)
			foreach(var p in particles)
				if (p.name == newMat)
					foreach(var e in emmiters)
						e.GetComponent<ParticleSystemRenderer>().material = p;
	}

	string ColorToHex(Color color)
	{
		Color32 color32 = color;
		return "#"+color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2")+ "FF";
	}

	public void UnlockTech(string TechName)
	{
		var newTech = teches[TechName];

		newTech.bn = Instantiate(TechBn).GetComponent<Button>();
		newTech.bn.transform.SetParent(TechPanel.transform);

		UpdateLabel(TechName);
		CheckCosts();
	}

	public void UpdateLabels()
	{
		foreach(var t in teches)
			UpdateLabel(t.Key);
	}

	void UpdateLabel(string TechName)
	{
		var newTech = teches[TechName];
		if (newTech.bn == null) return;

		var lbl = newTech.bn.GetComponentInChildren<Text>();
		
		var cost = " : " + Age.WrapInt(newTech.cost);
		string CostColor;
		if (newTech.isScienceCost == false)
			CostColor = ColorToHex(ResourcesSystem.colorCodes["Humans"]);
		else
			CostColor = ColorToHex(ResourcesSystem.colorCodes["Science"]);

		var TechColor = "#000000ff";
		if (newTech.type != null)
			TechColor = ColorToHex(ResourcesSystem.colorCodes[newTech.type]);

		var text = newTech.name;
		if (Age.age == "beforeLanguage" && TechName != "Language")
		{
			if (newTech.maskName != null) text = newTech.maskName;
			else text = "BlahBlah";
			Age.instance.abrvgl.Add(newTech.bn.GetComponentInChildren<Text>(),
				"<color=" + TechColor + ">" + newTech.name + "</color>" +
				"<color=" + CostColor + ">" + cost + "</color>");
		}

		lbl.text = "<color=" + TechColor + ">" + text + "</color>" +
			"<color=" + CostColor + ">" + cost + "</color>";
		newTech.bn.name = newTech.name;
	}

}

public class Tech {
	public int cost;
	public List<string> prereqTeches = new List<string>();
	public string name;
	public string maskName;
	public Button bn;
	public string type;
	public bool isScienceCost = true;

	public virtual bool avaliableBuyCheck()
	{
		if (ResourcesSystem.instance.Stock.ContainsKey("Science") == false) return false;
		return ResourcesSystem.instance.Stock["Science"].value >= cost;
	}

	public bool unlockCheck()
	{
		var allOk = true;
		foreach(var t in prereqTeches)
		{
			if (techList.instance.researchedTeches.Contains(t)==false)
				allOk = false;
		}

		return allOk;
	}

	public virtual void Buy() {}
}

public class AgeTech: Tech
{
	string age;

	public AgeTech(string name, int cost, string newAge, List<string> prereqTeches)
	{
		this.name = name;
		base.cost = cost;
		age = newAge;
		this.prereqTeches = prereqTeches;
	}

	public override void Buy()
	{
		Age.instance.changeAge(age);
		techList.instance.UpdateLabels();
	}
}

public class TierUnlock : Tech
{
	public TierUnlock(string name, int cost, List<string> prereqTeches)
	{
		this.name = name;
		isScienceCost = false;
		this.cost = cost;
		this.prereqTeches = prereqTeches;
		type = "Humans";
	}
	public override bool avaliableBuyCheck()
	{
		return 	ResourcesSystem.instance.Stock["Humans"].value >= cost;
	}

	public override void Buy()
	{
		BotBrains.instance.SpawnNewMan();
	}
}

public class FrendTech : Tech
{
	public FrendTech()
	{
		prereqTeches.Add("Leaders");
		name = "Cats";
		cost = 333;
		type = "Cats";
	}

	public override void Buy()
	{
		ResourcesSystem.instance.StartCoroutine(ResourcesSystem.instance.Bonuses());
	}
}

public class HavenTech : Tech
{
	public HavenTech()
	{
		prereqTeches.Add("Terraforming");
		name = "Haven Project";
		cost = 10000000;
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
		this.cost = scienceCost;
		this.maskName = maskName;
		this.techName = techName;
		this.addClickPower = addClickPower;
		type = techName;
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
		this.cost = scienceCost;
		this.maskName = maskName;
		this.newTimerValue = newTimerValue;
	}

	public override void Buy()
	{
		ResourcesSystem.instance.WorkerTimer = newTimerValue;
	}

}

public class GlobalTech : Tech
{
	float newMult;

	public GlobalTech(string name, int scienceCost, List<string> prereqTeches, float newMult, string maskName = null)
	{
		this.prereqTeches = prereqTeches;
		this.name = name;
		this.cost = scienceCost;
		this.maskName = maskName;
		this.newMult = newMult;
	}

	public override void Buy()
	{
		ResourcesSystem.instance.GlobalMult = newMult;
	}

}

public class SexTech : Tech
{
	public SexTech()
	{
		prereqTeches.Add("Language");
		name = "Sex";
		cost = 10;
		type = "Humans";
	}


	public override void Buy()
	{
		TaskSystem.instance.AddNewTask("Humans", "Babies Delivery", 0, 0);
		ResourcesSystem.instance.Stock["Humans"].poplMult = .2f;
	
		
	}
}

public class ProfTech : Tech
{
	public ProfTech()
	{
		prereqTeches.Add("Family");
		name = "Professions";
		cost = 1;
	}


	public override void Buy()
	{
		DragAndDropMans.isDraggable = true;
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
		this.cost = scienceCost;
		this.tName = newTaskName;
		this.tType = newTaskType;
		this.startValue = startValue;
		this.poplMult = poplMult;
		type = newTaskType;
	}

	public override void Buy()
	{
		TaskSystem.instance.AddNewTask(tType, tName, startValue, poplMult);
	}
}