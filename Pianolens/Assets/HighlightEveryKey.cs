using UnityEngine;
using System.Collections;

public class HighlightEveryKey : MonoBehaviour {
    bool[] keyIsHighlighted = new bool[109];
    private PianoDescriptor p;
    private Vector3 start;
    private Vector3 end;

	// Use this for initialization
	void Start () {
        p = PianoDescriptor.getPianoDescriptor();
        GameObject leftAnchor = GameObject.Find("leftAnchor");
        GameObject rightAnchor = GameObject.Find("rightAnchor");
        print(rightAnchor);

        start = leftAnchor.transform.position;
        end = rightAnchor.transform.position;
        print(start);
        print(end);

        Vector3 length = end - start;
        Vector3 lengthCentered = length * ((float)p.NumKeys / (p.NumKeys - 1));
        print(lengthCentered);
        length = start + lengthCentered;
        //try to extend from start to end, but not reach end.
        Vector3 halflength = (end - length) / (float)2.0;


        end = halflength + length;
        start = start + halflength;
        print(end);
        print(start);

        for(int i = 0; i < keyIsHighlighted.Length; i++)
        {
            keyIsHighlighted[i] = false;
        }
    }

    // Update is called once per frame
    void Update () {
        int i = Random.Range(21, 109);
        if (keyIsHighlighted[i]) {
            GameObject g = GameObject.Find("highlighting" + i);
            GameObject.DestroyObject(g);
            keyIsHighlighted[i] = false;
        }
        else
        {
            Vector3[] pos = p.getKeyWidth(i, false, ref start, ref end);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //print(pos[0]);
            //print(pos[1]);
            cube.name = "highlighting" + i;
            cube.transform.position = (pos[0]);
            //print(pos[0]);
            cube.transform.localScale = new Vector3(0.003f, 0.001f, 0.05f);
            //GameObject.Add
            keyIsHighlighted[i] = true;

        }
    }
}
