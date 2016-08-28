using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragAndDropMans : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public Vector3 startPosition;
	public static DragAndDropMans dragObject;
	public static bool isDraggable = true;
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
		if (isDraggable == false) return;
		
		startPosition = transform.position;
		dragObject = this;
		cGroup.blocksRaycasts = false;
		getAnyWork = false;
		transform.SetParent(BotBrains.instance.popupCanvas);
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
		if (isDraggable == false) return;
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (dragObject == null) return;
		cGroup.blocksRaycasts = true;

		if (getAnyWork == false && work != null)
		{
			work.workers.Remove(gameObject);
			SlotParent.gameObject.SetActive(true);
			dragObject.transform.SetParent(SlotParent.transform);
			dragObject.transform.position = SlotParent.transform.position;
		}
		else
		{
			if (work != null)
			{
				dragObject.transform.position = work.transform.position + new Vector3(-50f,-45f);
			}
			else
			{
				dragObject.transform.position = startPosition;
			}
		}
		dragObject = null;
	}

}
