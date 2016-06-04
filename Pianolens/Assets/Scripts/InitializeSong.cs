using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitializeSong : MonoBehaviour {

    static Bar firstBar = new Bar(new List<Note>() { new Note(1, Note.F_3, Note.Note_Types.quarter_note), new Note(2, Note.E_3, Note.Note_Types.quarter_note), new Note(3, Note.D_3, Note.Note_Types.quarter_note), new Note(4, Note.E_3, Note.Note_Types.quarter_note) });
    static Bar secondBar = new Bar(new List<Note>() { new Note(1, Note.F_3, Note.Note_Types.quarter_note), new Note(2, Note.F_3, Note.Note_Types.quarter_note), new Note(3, Note.F_3, Note.Note_Types.half_note) });
    static Bar thirdBar = new Bar(new List<Note>() { new Note(1, Note.C_3, Note.Note_Types.whole_note), /*new Note(1, Note.D_3, Note.Note_Types.eighth_note), */new Note(1.125f, Note.E_3, Note.Note_Types.eighth_note), new Note(1.25f, Note.F_3, Note.Note_Types.eighth_note), new Note(1.375f, Note.G_3, Note.Note_Types.eighth_note), new Note(1.5f, Note.A_3, Note.Note_Types.eighth_note), new Note(1.625f, Note.B_3, Note.Note_Types.eighth_note), new Note(1.75f, Note.C_4, Note.Note_Types.eighth_note), new Note(1.875f, Note.D_4, Note.Note_Types.eighth_note), new Note(2, Note.F_1, Note.Note_Types.eighth_note), new Note(2.125f, Note.G_1, Note.Note_Types.eighth_note), new Note(2.25f, Note.A_1, Note.Note_Types.eighth_note), new Note(2.375f, Note.B_1, Note.Note_Types.eighth_note), new Note(2.5f, Note.C_2, Note.Note_Types.eighth_note), new Note(2.625f, Note.D_2, Note.Note_Types.eighth_note), new Note(2.75f, Note.E_2, Note.Note_Types.eighth_note), new Note(2.875f, Note.F_2, Note.Note_Types.eighth_note), new Note(3.0f, Note.G_2, Note.Note_Types.eighth_note), new Note(3.125f, Note.A_2, Note.Note_Types.eighth_note) });
    static Bar fourthBar = new Bar(new List<Note>() { new Note(1, Note.Rest_Treble, Note.Note_Types.quarter_rest), new Note(2, Note.Rest_Bass, Note.Note_Types.whole_rest), new Note(2, Note.Rest_Treble, Note.Note_Types.half_rest), new Note(3, Note.Rest_Treble, Note.Note_Types.eighth_rest), new Note(3.5f, Note.Rest_Treble, Note.Note_Types.eighth_rest) });

    static Song song = new Song(1, "Mary Had a Little Lamb", "I Don't Know", (int)Keys.cmaj, 4, 4, new List<Bar>() { firstBar, secondBar, thirdBar, fourthBar }, 60);

    // Use this for initialization
    void Start () {
        Song.SetCurrentSong(song);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
