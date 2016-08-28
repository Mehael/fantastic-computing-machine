using UnityEngine;
using System.Collections;

public class ConstantAnimation : MonoBehaviour {
    public Vector3 moveVector;
    public float Torgue;

	// Update is called once per frame
	void LateUpdate () {
        var delta = Time.deltaTime;
        if (PauseSystem.inPause == false) transform.Translate(moveVector * delta, Space.World);
        transform.Rotate(Vector3.forward, Torgue * delta, Space.World);
	}
}
