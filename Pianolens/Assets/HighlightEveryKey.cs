using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighlightEveryKey : MonoBehaviour {
    private const int MAX_KEY_INDEX = 108;
    public const float PIXELPERBEAT = 0.06f; //80 pixels per beat.

    bool[] keyIsHighlighted = new bool[MAX_KEY_INDEX + 1];
    private PianoDescriptor p;
    private Vector3 start;
    private Vector3 end;

    public Material PianoRollMatRef;
    public Material KeyHighlightMatRef;
    public GameObject stage;
    public GameObject pianoRollContainer;

	// Use this for initialization
	void Start () {
        p = PianoDescriptor.getPianoDescriptor();
        GameObject leftAnchor = GameObject.Find("leftAnchor");
        GameObject rightAnchor = GameObject.Find("rightAnchor");
        print(rightAnchor);
        stage = GameObject.Find("MainApp");
        pianoRollContainer = GameObject.Find("PianoRollContainer");

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
    //void Update () {
       /* if (Random.Range(0, MAX_KEY_INDEX) < 2)
        {
            // TODO: This isn't going to be random in the end. This is going to have
            // an understanding of the music that needs to be played. For example, 
            // if we know we are going to play the first bar of "Ode-to-Joy", then we have: 62, 62, 63, 64
            // This means we have a note range of 62 to 64. It's important for the app to understand
            // what music is upcoming so the FOV compensation shows you where your head will need to be in the future.
            int i = Random.Range(40, 60);

            if (keyIsHighlighted[i])
            {
                SetKeyHighlight(false, i);
            }
            else
            {
                SetKeyHighlight(true, i);
            }
        }*/
    //}

    // Highlight the key
    public void SetKeyHighlight(bool isHighlight, int keyIndex)
    {
        if (keyIndex < 20) return;

        if (!isHighlight)
        {
            GameObject g = GameObject.Find("highlighting" + keyIndex.ToString().PadLeft('0'));
            GameObject.DestroyObject(g);
            keyIsHighlighted[keyIndex] = false;
        }
        else
        {
            Vector3[] pos = p.getKeyWidth(keyIndex, false);
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.GetComponent<MeshRenderer>().sharedMaterial = KeyHighlightMatRef;
            //print(pos[0]);
            //print(pos[1]);
            cube.name = "highlighting" + keyIndex.ToString().PadLeft('0');
            cube.tag = "HighlightedKey";
            cube.transform.parent = stage.transform;
            cube.transform.position = stage.transform.position + ((pos[0] + pos[1]) / 2.0f);
            //print(pos[0]);
            cube.transform.localScale = new Vector3((pos[0] - pos[1]).magnitude, 0.03f, 0.05f);
            //GameObject.Add
            keyIsHighlighted[keyIndex] = true;
        }
    }

    public void AddPianoRollItem(SongEvent e, float futureTick)
    {
        Vector3[] keyWidth = PianoDescriptor.getPianoDescriptor().getKeyWidth(e.KeyID, false);
        float girth = .5f * (keyWidth[0] - keyWidth[1]).magnitude;
        float duration = Note.GetDuration(e.NoteType);
        float length = PIXELPERBEAT * duration;

        float center = e.EndPoint - Timing.CurrentMeasure - (duration / 2f);
        float z_depth = center * PIXELPERBEAT;

        Vector3 midpoint = ((keyWidth[0] + keyWidth[1]) / 2f);
        float x_center = midpoint.x;
        float y_center = midpoint.y;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<MeshRenderer>().sharedMaterial = PianoRollMatRef;
        cube.name = "rolling" + e.KeyID.ToString().PadLeft('0');
        cube.tag = "PianoRoll";

        PianoRoll r = cube.AddComponent<PianoRoll>();
        r.e = e;
        r.endPoint = e.EndPoint;
        r.duration = duration;
        r.startPoint = e.StartPoint;

        cube.transform.parent = pianoRollContainer.transform;
        cube.transform.localPosition = new Vector3(x_center, y_center, z_depth);
        cube.transform.localScale = new Vector3(girth, 0.01f, length);
        cube.transform.rotation = GameObject.Find("MainApp").gameObject.transform.rotation;
    }

    public void DestroyAllPianoRollItems()
    {
        GameObject[] pianoRollObjects = GameObject.FindGameObjectsWithTag("PianoRoll");

        for (var i = 0; i < pianoRollObjects.Length; i++)
        {
            Destroy(pianoRollObjects[i]);
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
