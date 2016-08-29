using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxData
{
	public Transform sprite;
	public float speed;

	public ParallaxData(Transform sprite, float speed)
	{
		this.speed = speed;
		this.sprite = sprite;
	}
}

public class parallax : MonoBehaviour {
	public Transform Grass;
	public Transform SemiGrass;
	public Transform Mountans;
	public Transform Clouds;
	public Transform Sun;

	List<ParallaxData> paraLayers ; 
	// Use this for initialization
	void Start () {

		paraLayers = new List<ParallaxData>()
		{
			new ParallaxData(Grass, -1),
			new ParallaxData(SemiGrass, -.2f),
			new ParallaxData(Mountans, -.05f),
			new ParallaxData(Clouds, -.03f),
			new ParallaxData(Sun, -.02f),
		};

		oldXMousePosition = Input.mousePosition.x;
	}

	public float oldXMousePosition;

	// Update is called once per frame
	void Update () {
		var xMouseDelta = oldXMousePosition - Input.mousePosition.x;
		if (Mathf.Abs(xMouseDelta) > 200) return;

		oldXMousePosition = Input.mousePosition.x;
		if (xMouseDelta == 0) return;

		foreach(var para in paraLayers)
			para.sprite.Translate(new Vector3(xMouseDelta * para.speed * Time.deltaTime,0));

	}
}
