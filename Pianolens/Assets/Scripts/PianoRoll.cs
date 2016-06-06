using UnityEngine;
using System.Collections;

public class PianoRoll : MonoBehaviour {

    const float TOLERANCE = .5f;

    Material errorMat;

    public SongEvent e;
    public float endPoint;
    public float center;
    public float duration;
    public float startPoint;

    bool markedWrong;

	// Use this for initialization
	void Start () {
        errorMat = Resources.Load("Materials/Error") as Material;
    }
	
	// Update is called once per frame
	void Update () {
        float remaining = endPoint - Timing.CurrentMeasure;
        
        if(remaining < 0)
        {
            Object.Destroy(gameObject);
            return;
        }

        if (Timing.CurrentMeasure - startPoint > TOLERANCE)
        {
            if (!e.IsHit() && !markedWrong)
            {
                markedWrong = true;
                gameObject.GetComponent<Renderer>().material = errorMat;
                Debug.Log("Marking wrong: " + e.EndPoint + ", key: " + e.KeyID);
            }
        }

        float z_depth = (remaining - (.5f * duration)) * HighlightEveryKey.PIXELPERBEAT;
        Vector3 lp = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(lp.x, lp.y, z_depth);
    }
}
