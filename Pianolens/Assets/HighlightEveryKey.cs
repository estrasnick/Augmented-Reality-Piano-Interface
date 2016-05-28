using UnityEngine;
using System.Collections;


public class HighlightEveryKey : MonoBehaviour {
    bool[] keyIsHighlighted = new bool[109];
    private PianoDescriptor p;
    private Vector3 start;
    private Vector3 end;

    public Material highlightMatRef;
    public GameObject stage;

	// Use this for initialization
	void Start () {
        p = PianoDescriptor.getPianoDescriptor();
        GameObject leftAnchor = GameObject.Find("leftAnchor");
        GameObject rightAnchor = GameObject.Find("rightAnchor");
        print(rightAnchor);
        stage = GameObject.Find("MainApp");

        start = stage.transform.InverseTransformPoint(leftAnchor.transform.position); //start and end are relative to stage after all.
        end = stage.transform.InverseTransformPoint(rightAnchor.transform.position);
        print(start);
        print(end);

        p.setUpPiano(start, end);

        //Vector3 length = end - start;
        //Vector3 lengthCentered = length * ((float)p.NumKeys / (p.NumKeys - 1));
        //print(lengthCentered);
        //length = start + lengthCentered;
        ////try to extend from start to end, but not reach end.
        //Vector3 halflength = (end - length) / (float)2.0;


        //end = halflength + length;
        //start = start + halflength;
        //print(end);
        //print(start);

        for(int i = 0; i < keyIsHighlighted.Length; i++)
        {
            keyIsHighlighted[i] = false;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Random.Range(0, 100) < 2)
        {
            int i = Random.Range(40, 60);
            //int i = Random.Range(60, 72);
            if (keyIsHighlighted[i])
            {
                GameObject g = GameObject.Find("highlighting" + i);
                GameObject.DestroyObject(g);
                keyIsHighlighted[i] = false;
            }
            else
            {
                Vector3[] pos = p.getKeyWidth(i, false);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.GetComponent<MeshRenderer>().sharedMaterial = highlightMatRef;
                //print(pos[0]);
                //print(pos[1]);
                cube.name = "highlighting" + i;
                FOVTarget f = cube.AddComponent<FOVTarget>();
                f.target = cube.transform;
                cube.transform.parent = stage.transform;
                cube.transform.position = stage.transform.position + ((pos[0] + pos[1]) / 2.0f);
                //print(pos[0]);
                cube.transform.localScale = new Vector3((pos[0] - pos[1]).magnitude, 0.03f, 0.05f);
                //GameObject.Add
                keyIsHighlighted[i] = true;

            }
        }
    }
}

// Custom Editor the "old" way by modifying the script variables directly.
// No handling of multi-object editing, undo, and prefab overrides!
//[CustomEditor(typeof(HighlightEveryKey))]
//public class MyPlayerEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        HighlightEveryKey mp = (HighlightEveryKey)target;
//        mp.highlightMatRef = (Material) EditorGUILayout.ObjectField("Material", mp.highlightMatRef, typeof(Material), !EditorUtility.IsPersistent(target));
//        EditorGUILayout.Space();
//    }

//    // Custom GUILayout progress bar.
//    void ProgressBar(float value, string label)
//    {
//        // Get a rect for the progress bar using the same margins as a textfield:
//        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
//        EditorGUI.ProgressBar(rect, value, label);
//        EditorGUILayout.Space();
//    }
//}
