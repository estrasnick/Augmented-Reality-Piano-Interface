using UnityEngine;
using System.Collections;

public class ActivatingDemo02 : MonoBehaviour {
	public GameObject nextObj;
	public float _time = 8.0f;
	private bool Many = false;
	// Use this for initialization
	void Start () {
		Transform[] objs = GetComponentsInChildren<Transform> ();
		if (objs.Length == 2) {
				Many = false;
				} else {
					Many = true;
				}

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
		if(Many){
			Transform[] objs = GetComponentsInChildren<Transform> ();
			foreach (Transform obj in objs) {
				if(obj.transform.parent != null && obj.GetComponent<Renderer>()){
					Debug.Log(obj.transform.gameObject);
					obj.transform.Rotate (Vector3.up*1);
				}
			}
		}
		else{
			transform.Rotate (Vector3.up*1);
		}
	}
}
