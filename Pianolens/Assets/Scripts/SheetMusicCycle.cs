using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SheetMusicCycle : MonoBehaviour {

    static float BETWEEN_STAFF_LINE_DISTANCE = .0125f;
    static float LINE_OFFSET = .0125f;

    Song song;
    
    public Transform quarter_note;
    public Transform whole_note;
    public Transform half_note;
    public Transform eighth_note;
    public Transform sixteenth_note;
    public Transform dotted_quarter_note;
    public Transform dotted_whole_note;
    public Transform dotted_half_note;
    public Transform dotted_eighth_note;
    public Transform dotted_sixteenth_note;

    public Transform quarter_rest;
    public Transform whole_rest;
    public Transform half_rest;
    public Transform eighth_rest;

    public Transform ledger_line;

    GameObject MusicSheet;
    Vector3 MusicSheetLoc;

    int currentBar;

    // Use this for initialization
    void Start() {
        song = Song.GetCurrentSong();

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
            // Check if song is null, and continue checking until it is not
            if (song == null)
            {
                song = Song.GetCurrentSong();
            }
            else
            {
                ClearBar();
                RenderBar(song.GetBars()[currentBar++]);
                if (currentBar >= song.GetBars().Count)
                {
                    currentBar = 0;
                }
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
        switch (note.GetNoteType())
        {
            case Note.Note_Types.quarter_note:
                return quarter_note;
            case Note.Note_Types.half_note:
                return half_note;
            case Note.Note_Types.eighth_note:
                return eighth_note;
            case Note.Note_Types.whole_note:
                return whole_note;
            case Note.Note_Types.sixteenth_note:
                return sixteenth_note;
            case Note.Note_Types.dotted_quarter_note:
                return dotted_quarter_note;
            case Note.Note_Types.dotted_half_note:
                return dotted_half_note;
            case Note.Note_Types.dotted_eighth_note:
                return dotted_eighth_note;
            case Note.Note_Types.dotted_whole_note:
                return dotted_whole_note;
            case Note.Note_Types.dotted_sixteenth_note:
                return dotted_sixteenth_note;
            case Note.Note_Types.quarter_rest:
                return quarter_rest;
            case Note.Note_Types.half_rest:
                return half_rest;
            case Note.Note_Types.eighth_rest:
                return eighth_rest;
            case Note.Note_Types.whole_rest:
                return whole_rest;
            default:
                Debug.Log("Unknown note type");
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
            Debug.Log("note position fail");
        }

        float z = GameObject.Find("StaffLine5").transform.position.z;

        float y = 0;
        try
        {
            switch (note.GetPianoKey())
            {
                case Note.Rest_Treble:
                    y = GameObject.Find("StaffLine4").transform.position.y;
                    break;
                case Note.Rest_Bass:
                    y = GameObject.Find("StaffLine9").transform.position.y;
                    break;
                case Note.F_1:
                    y = GameObject.Find("StaffLine10").transform.position.y - BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.G_1:
                    y = GameObject.Find("StaffLine10").transform.position.y;
                    break;
                case Note.A_1:
                    y = GameObject.Find("StaffLine10").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.B_1:
                    y = GameObject.Find("StaffLine9").transform.position.y;
                    break;
                case Note.C_2:
                    y = GameObject.Find("StaffLine9").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.D_2:
                    y = GameObject.Find("StaffLine8").transform.position.y;
                    break;
                case Note.E_2:
                    y = GameObject.Find("StaffLine8").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.F_2:
                    y = GameObject.Find("StaffLine7").transform.position.y;
                    break;
                case Note.G_2:
                    y = GameObject.Find("StaffLine7").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.A_2:
                    y = GameObject.Find("StaffLine6").transform.position.y;
                    break;
                case Note.B_2:
                    y = GameObject.Find("StaffLine6").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.C_3:
                    y = GameObject.Find("StaffLine6").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE + BETWEEN_STAFF_LINE_DISTANCE;
                    
                    // we need to add a ledger line, halfway between the two staves
                    Instantiate(ledger_line, new Vector3(x, .5f * (GameObject.Find("StaffLine6").transform.position.y + GameObject.Find("StaffLine5").transform.position.y), z), Quaternion.identity);
                    break;
                case Note.D_3:
                    y = GameObject.Find("StaffLine5").transform.position.y - BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.E_3:
                    y = GameObject.Find("StaffLine5").transform.position.y;
                    break;
                case Note.F_3:
                    y = GameObject.Find("StaffLine5").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.G_3:
                    y = GameObject.Find("StaffLine4").transform.position.y;
                    break;
                case Note.A_3:
                    y = GameObject.Find("StaffLine4").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.B_3:
                    y = GameObject.Find("StaffLine3").transform.position.y;
                    break;
                case Note.C_4:
                    y = GameObject.Find("StaffLine3").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.D_4:
                    y = GameObject.Find("StaffLine2").transform.position.y;
                    break;
                case Note.E_4:
                    y = GameObject.Find("StaffLine2").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                case Note.F_4:
                    y = GameObject.Find("StaffLine1").transform.position.y;
                    break;
                case Note.G_4:
                    y = GameObject.Find("StaffLine1").transform.position.y + BETWEEN_STAFF_LINE_DISTANCE;
                    break;
                default:
                    Debug.Log("Note " + note.GetPianoKey() + " is currently unsupported.");
                    y = GameObject.Find("StaffLine5").transform.position.y;
                    break;
            }
            y -= LINE_OFFSET;
        } catch (Exception e)
        {
           // Debug.Log("Y");
        }

        

        //Debug.Log("X: " + x + ", Y: " + y + ", Z: " + z);

        return new Vector3(x, y, z);
    }
}
