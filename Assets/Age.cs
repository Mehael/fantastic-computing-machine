using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

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

	public void GoodEnd()
	{
		throw new NotImplementedException();
	}

	public void BadEnd()
	{
		throw new NotImplementedException();
	}
}
