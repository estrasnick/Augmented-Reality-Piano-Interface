using UnityEngine;
using System.Collections;

public class PianoRoll : MonoBehaviour {

    public SongEvent e;
    public float midPoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float center = midPoint - Timing.CurrentMeasure;
        print(center);
        if(center < 0)
        {
            Object.Destroy(gameObject);
            return;
        }
        float z_depth = center * HighlightEveryKey.PIXELPERBEAT;
        Vector3 lp = this.transform.localPosition;
        this.transform.position.Set(lp.x, lp.y, z_depth);
    }
}
