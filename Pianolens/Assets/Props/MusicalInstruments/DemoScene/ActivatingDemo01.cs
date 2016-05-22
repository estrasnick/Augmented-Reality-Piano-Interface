using UnityEngine;
using System.Collections;

public class ActivatingDemo01 : MonoBehaviour {
	public GameObject nextObj;
	public float _time = 8.0f;
	// Use this for initialization
	void Start () {
		Invoke ("ShowNext", _time);
	}
	void ShowNext(){
		if (nextObj != null) {
			nextObj.SetActive (true);
		}
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			transform.Rotate (Vector3.up*1);
	}
}
