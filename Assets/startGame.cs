using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {

	public void StartGameClick(string param)
	{
		SceneManager.LoadScene("game");
	}
}
