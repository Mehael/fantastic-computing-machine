using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TaskSystem : MonoBehaviour {
	static public TaskSystem instance;
	public GameObject TaskPrefab;
	public GameObject TaskPanel;
	Dictionary<string, GameObject> tasks = new Dictionary<string, GameObject>();

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		AddNewTask("Food", "Omnomnom", 100, -1);
		Age.instance.abrvgl.Add(tasks["Omnomnom"].GetComponentInChildren<Text>(), "Gathering");
	}

	public void AddNewTask(string resType, string bnText, int startValue, float populationMult)
	{
		var go = (GameObject)Instantiate(TaskPrefab);
		go.transform.SetParent(TaskPanel.transform);
		go.name = resType;
		go.GetComponentInChildren<Text>().text = bnText;
		tasks.Add(bnText, go);

		ResourcesSystem.instance.AddNewResource(resType, populationMult, startValue);
	}
}
