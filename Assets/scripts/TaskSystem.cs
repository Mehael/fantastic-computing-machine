using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TaskSystem : MonoBehaviour {
	static public TaskSystem instance;
	public GameObject TaskPrefab;
	public GameObject TaskPanel;
	Dictionary<string, GameObject> tasks = new Dictionary<string, GameObject>();
	public List<Sprite> sprites = new List<Sprite>(); 

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		AddNewTask("Cookies", "Omnomnom", 100, -1);
		Age.instance.abrvgl.Add(tasks["Omnomnom"].GetComponentInChildren<Text>(), "Gathering");
	}

	public void AddNewTask(string resType, string bnText, int startValue, float populationMult)
	{
		var go = (GameObject)Instantiate(TaskPrefab);
		go.transform.SetParent(TaskPanel.transform);
		go.name = resType;
		go.GetComponent<taskBn>().resName = resType;
		go.GetComponentInChildren<Text>().text = bnText;
		tasks.Add(bnText, go);

		foreach (var s in sprites)
			if (s.name == bnText)
				go.GetComponentInChildren<Image>().sprite = s;

		ResourcesSystem.instance.AddNewResource(resType, populationMult, startValue);
	}
}
