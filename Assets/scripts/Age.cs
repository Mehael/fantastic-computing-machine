using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Age : MonoBehaviour {
	public static Age instance;
	public static string age = "beforeLanguage";
	public Dictionary<Text, string> abrvgl = new Dictionary<Text, string>();

	void Awake()
	{
		instance = this;
	}

	public void AddText(Text textField, string maskedText)
	{
		abrvgl.Add(textField, textField.text);
		textField.text = maskedText;
	}

	public void changeAge(string newAge)
	{
		age = newAge;
		foreach(var t in abrvgl)
			if (t.Key != null)
			t.Key.text = t.Value;
	}


	public static string WrapInt(int number)
	{
		var res = "";
		var module = Mathf.Abs(number);
		if (age != "math")
		{
			
			if (module < 10) res = "few";
			else if (module < 100) res = "a lot";
			else res = "so much";

			if (number < 0) res = "-" + res;
		}
		else
		{
			while (module > 1000)
			{
				res = res + "k";
				module = Mathf.FloorToInt(module / 1000);
				number = Mathf.FloorToInt(number / 1000);
			}
			res = number + res;
		}

		return res; 
	}

	public void GoodEnd()
	{

		Clear();
		SceneManager.LoadScene("goodEnd");

	}

	public void Clear()
	{
		SceneManager.UnloadScene("game");
		age = "beforeLanguage";
	}

	public void BadEnd()
	{
		Clear();
		SceneManager.LoadScene("badEnd");

	}
}
