using UnityEngine;
using System.Collections;

public class PianoRoll : MonoBehaviour {

    public SongEvent e;
    public float midPoint;
    public float center;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        center = midPoint - Timing.CurrentMeasure;
        print(center);
        if(center < 0)
        {
            Object.Destroy(gameObject);
            return;
        }
        float z_depth = center * HighlightEveryKey.PIXELPERBEAT;
        Vector3 lp = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(lp.x, lp.y, z_depth);
    }
}
