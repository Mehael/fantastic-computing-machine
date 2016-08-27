using UnityEngine;
using System.Collections;

public class bnTechClicked : MonoBehaviour {

	public void Click(string param)
	{
		techList.instance.ResearchClick(gameObject.name);
	}
}
