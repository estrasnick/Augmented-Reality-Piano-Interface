using UnityEngine;
using System.Collections;

public class PianoRoll : MonoBehaviour {

    public SongEvent e;
    public float endPoint;
    public float center;
    public float duration;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float remaining = endPoint - Timing.CurrentMeasure;
        
        if(remaining < 0)
        {
            Object.Destroy(gameObject);
            return;
        }
        float z_depth = (remaining - (.5f * duration)) * HighlightEveryKey.PIXELPERBEAT;
        Vector3 lp = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(lp.x, lp.y, z_depth);
    }
}
