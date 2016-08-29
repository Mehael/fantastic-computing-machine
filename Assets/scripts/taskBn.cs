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
		if (PauseSystem.inPause == false)
		ResourcesSystem.instance.ChangeResource(resName, 
			ResourcesSystem.instance.Stock[resName].withMultCost*10, transform);
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (workers.Count >= slotAmount || DragAndDropMans.dragObject==null) return;
		if(workers.Contains(DragAndDropMans.dragObject.gameObject)) return;

		workers.Add(DragAndDropMans.dragObject.gameObject);
		DragAndDropMans.dragObject.SetWork(this);
	}

	IEnumerator Start()
	{
		while (true)
		{
			if (PauseSystem.inPause)
			{
				yield return new WaitForEndOfFrame();
				continue;
			}
			yield return new WaitForSeconds(ResourcesSystem.instance.WorkerTimer);

			if (resName == "Humans" && ResourcesSystem.instance.isInNegativeMode)
				continue;

			foreach (var worker in workers) {
				ResourcesSystem.instance.ChangeResource(resName, 
					ResourcesSystem.instance.Stock[resName].withMultCost, worker.transform);
			}
		}
	}
}
