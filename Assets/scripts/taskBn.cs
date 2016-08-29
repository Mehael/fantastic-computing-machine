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

		transform.localScale = new Vector3(.9f, .9f);

	}

	public void OnDrop(PointerEventData eventData)
	{
		if (workers.Count >= slotAmount || DragAndDropMans.dragObject==null) return;
		if(workers.Contains(DragAndDropMans.dragObject.gameObject)) return;

		workers.Add(DragAndDropMans.dragObject.gameObject);
		DragAndDropMans.dragObject.SetWork(this);
	}


	float scaleVelocity;
	public float targetScale = 1f;
	void Update()
	{
		if (transform.localScale.x > targetScale)
			targetScale = transform.localScale.x;
		if (transform.localScale.x == targetScale) return;

		if (Mathf.Abs(transform.localScale.x - targetScale) < .01f)
			transform.localScale = new Vector3(targetScale, targetScale, targetScale);
		else
		{
			var t = Mathf.SmoothDamp(transform.localScale.x, targetScale, ref scaleVelocity, .3f);
			transform.localScale = new Vector3(t, t, t);
		}

			
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
