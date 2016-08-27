using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragAndDropMans : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public Vector3 startPosition;
	public static DragAndDropMans dragObject;
	public taskBn work;
	public Material lineMaterial;
	public GameObject lineParent;


	public bool getAnyWork = false;
	Transform SlotParent;
	CanvasGroup cGroup;

	void Start()
	{
		cGroup = GetComponent<CanvasGroup>();
		SlotParent = transform.parent;
	}

	public void Click(string param)
	{
		if (work != null) work.Click("");
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("dragStart");
		startPosition = transform.position;
		dragObject = this;
		cGroup.blocksRaycasts = false;
		getAnyWork = false;
	}

	public void SetWork(taskBn newWork)
	{
		if (work != null) work.workers.Remove(gameObject);
		getAnyWork = true;
		transform.SetParent(newWork.transform);
		work = newWork;
		if (SlotParent != null) SlotParent.gameObject.SetActive(false);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		cGroup.blocksRaycasts = true;
		if (work != null)
		{
			dragObject.transform.position = work.transform.position;
		}
		else {
			dragObject.transform.position = startPosition;
		}
		dragObject = null;
	}

}
