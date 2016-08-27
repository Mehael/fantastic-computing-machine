using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class taskBn : MonoBehaviour, IDropHandler {
	public string resName;
	public int slotAmount = 1;
	public List<GameObject> workers = new List<GameObject>();

	public void Click(string param)
	{
		ResourcesSystem.instance.ChangeResource(resName, 
			ResourcesSystem.instance.Stock[resName].clickCost, transform);
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (workers.Count >= slotAmount) return;
		if(workers.Contains(DragAndDropMans.dragObject.gameObject)) return;

		workers.Add(DragAndDropMans.dragObject.gameObject);
		DragAndDropMans.dragObject.SetWork(this);
	}

	IEnumerator Start()
	{
		while (true)
		{
			yield return new WaitForSeconds(ResourcesSystem.instance.WorkerTimer);
			foreach (var worker in workers) {
				ResourcesSystem.instance.ChangeResource(resName, 
					ResourcesSystem.instance.Stock[resName].clickCost, worker.transform);
				yield return new WaitForSeconds(.1f);
			}
		}
	}
}
