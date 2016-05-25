using UnityEngine;
using System.Collections;

public class SheetMusicCycle : MonoBehaviour {

    static float Z_OFFSET = .05f;

    bool flipper;
    public Transform quarter_note;
    public Transform whole_note;

    GameObject MusicSheet;
    Vector3 MusicSheetLoc;

    // Use this for initialization
    void Start () {
        StartCoroutine(NextBar());

        MusicSheet = GameObject.Find("MusicSheet");
        MusicSheetLoc = MusicSheet.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator NextBar()
    {
        while(true)
        {
            ClearBar();

            Instantiate(flipper ? quarter_note : whole_note, new Vector3(MusicSheetLoc.x, MusicSheetLoc.y, MusicSheetLoc.z - Z_OFFSET), Quaternion.identity);
            flipper = !flipper;
            yield return new WaitForSeconds(2);
        }
    }

    void ClearBar()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("MicroNote");
        for (int i = 0; i < notes.Length; i++)
        {
            Destroy(notes[i]);
        }
    }
}
