using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SheetMusicCycle : MonoBehaviour {

    static float BETWEEN_STAFF_LINE_DISTANCE = .0125f;
    static float LINE_OFFSET = .0125f;

    static Bar firstBar = new Bar(new List<Note>() { new Note(1, 63, 1), new Note(2, 62, 1), new Note(3, 61, 1), new Note(4, 62, 1) });
    static Bar secondBar = new Bar(new List<Note>() { new Note(1, 63, 1), new Note(2, 63, 1), new Note(3, 63, 2) });

    static Song song = new Song("Mary Had a Little Lamb", "I Don't Know", (int)Keys.cmaj, 4, 4, new List<Bar>() { firstBar, secondBar }, 100);

    public Transform quarter_note;
    public Transform whole_note;
    public Transform half_note;

    GameObject MusicSheet;
    Vector3 MusicSheetLoc;

    int currentBar;

    // Use this for initialization
    void Start() {
        MusicSheet = GameObject.Find("MusicSheet");
        MusicSheetLoc = MusicSheet.transform.position;

        currentBar = 0;
        StartCoroutine(NextBar());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator NextBar()
    {
        while (true)
        {
            ClearBar();
            RenderBar(song.GetBars()[currentBar++]);
            if (currentBar >= song.GetBars().Count)
            {
                currentBar = 0;
            }

            yield return new WaitForSeconds(3);
        }
    }

    void ClearBar()
    {
        GameObject[] currentNotes = GameObject.FindGameObjectsWithTag("MicroNote");
        for (int i = 0; i < currentNotes.Length; i++)
        {
            Destroy(currentNotes[i]);
        }
    }

    void RenderBar(Bar bar)
    {
        foreach (Note note in bar.GetNotes())
        {
            Instantiate(getTransformForNoteRatio(note), getNotePosition(note), Quaternion.identity);
        }
    }

    Transform getTransformForNoteRatio(Note note)
    {
        float ratio = (float) note.GetDuration() / song.GetBeatsPerMeasure();
        if (ratio == .25f)
        {
            return quarter_note;
        }
        else if (ratio == .5f)
        {
            return half_note;
        }
        else if (ratio == 1)
        {
            return whole_note;
        }
        else
        {
            return quarter_note;
        }
    }

    Vector3 getNotePosition(Note note)
    {
        float x = 0;
        try
        {
            x = (MusicSheet.GetComponent<MeshFilter>().sharedMesh.bounds.size.x * .6f) * ((float) note.GetStartingBeat() / (song.GetBeatsPerMeasure() + 1)) + MusicSheetLoc.x - (.3f * MusicSheet.GetComponent<MeshFilter>().sharedMesh.bounds.size.x);
        } catch (Exception e)
        {
            Debug.Log("X");
        }

        float y = 0;
        try
        {
            switch (note.GetPianoKey())
            {
                case 61:
                    y = GameObject.Find("StaffLine5").transform.position.y - BETWEEN_STAFF_LINE_DISTANCE - LINE_OFFSET;
                    break;
                case 62:
                    y = GameObject.Find("StaffLine5").transform.position.y - LINE_OFFSET;
                    break;
                case 63:
                    y = GameObject.Find("StaffLine5").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE - LINE_OFFSET;
                    break;
                default:
                    y = GameObject.Find("StaffLine5").transform.position.y - LINE_OFFSET;
                    break;
            }
        } catch (Exception e)
        {
            Debug.Log("Y");
        }

float z = GameObject.Find("StaffLine5").transform.position.z;

        Debug.Log("X: " + x + ", Y: " + y + ", Z: " + z);

        return new Vector3(x, y, z);
    }
}
