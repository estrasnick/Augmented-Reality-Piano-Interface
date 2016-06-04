using System.Collections;
using System.Collections.Generic;

public enum Keys
{
    amaj,
    amin,
    bmaj,
    bmin,
    cmaj,
    cmin,
    dmaj,
    dmin,
    emaj,
    emin,
    fmaj,
    fmin,
    gmaj,
    gmin,
    aflatmaj,
    bflatmaj,
    dflatmaj,
    eflatmaj,
    gflatmaj,
    bflatmin,
    csharpmin,
    dsharpmin,
    fsharpmin,
    gsharpmin
}

public class Song
{
    int Id;
    string Title;
    string Composer;
    int Key;
    int BeatsPerMeasure;
    int BeatUnit;
    List<Bar> Bars;
    int DefaultBPM;
    int CurrentBPM;

    static Song CurrentSong;

    public Song(int id, string title, string composer, int key, int beatsPerMeasure, int beatUnit, List<Bar> bars, int defaultBPM, int currentBPM = -1)
    {

        if (currentBPM == -1)
        {
            CurrentBPM = defaultBPM;
        }
        Id = id;
        Title = title;
        Composer = composer;
        Key = key;
        BeatsPerMeasure = beatsPerMeasure;
        BeatUnit = beatUnit;
        Bars = bars;
        DefaultBPM = defaultBPM;
    }

    public int GetId()
    {
        return Id;
    }

    public string GetTitle()
    {
        return Title;
    }

    public string GetComposer()
    {
        return Composer;
    }

    public int GetKey()
    {
        return Key;
    }

    public int GetBeatsPerMeasure()
    {
        return BeatsPerMeasure;
    }

    public int GetBeatUnit()
    {
        return BeatUnit;
    }

    public int GetDefaultBPM()
    {
        return DefaultBPM;
    }

    public int GetCurrentBPM()
    {
        return CurrentBPM;
    }

    public List<Bar> GetBars()
    {
        return Bars;
    }

    public static void SetCurrentSong(Song newSong)
    {
        CurrentSong = newSong;
    }

    public static Song GetCurrentSong()
    {
        return CurrentSong;
    }
}

public class Bar
{
    List<Note> Notes;

    public Bar(List<Note> notes)
    {
        Notes = notes;
    }

    public List<Note> GetNotes()
    {
        return Notes;
    }
}

public class Note
{
    #region consts
    public const int Rest_Treble = 0;
    public const int Rest_Bass = 1;
    public const int A_minus1 = 21;
    public const int Asharp_minus1 = 22;
    public const int Bflat_minus1 = 22;
    public const int B_minus1 = 23;
    public const int C_0 = 24;
    public const int Csharp_0 = 25;
    public const int Dflat_0 = 25;
    public const int D_0 = 26;
    public const int Dsharp_0 = 27;
    public const int Eflat_0 = 27;
    public const int E_0 = 28;
    public const int F_0 = 29;
    public const int Fsharp_0 = 30;
    public const int Gflat_0 = 30;
    public const int G_0 = 31;
    public const int Gsharp_0 = 32;
    public const int Aflat_0 = 32;
    public const int A_0 = 33;
    public const int Asharp_0 = 34;
    public const int Bflat_0 = 34;
    public const int B_0 = 35;
    public const int C_1 = 36;
    public const int Csharp_1 = 37;
    public const int Dflat_1 = 37;
    public const int D_1 = 38;
    public const int Dsharp_1 = 39;
    public const int Eflat_1 = 39;
    public const int E_1 = 40;
    public const int F_1 = 41;
    public const int Fsharp_1 = 42;
    public const int Gflat_1 = 42;
    public const int G_1 = 43;
    public const int Gsharp_1 = 44;
    public const int Aflat_1 = 44;
    public const int A_1 = 45;
    public const int Asharp_1 = 46;
    public const int Bflat_1 = 46;
    public const int B_1 = 47;
    public const int C_2 = 48;
    public const int Csharp_2 = 49;
    public const int Dflat_2 = 49;
    public const int D_2 = 50;
    public const int Dsharp_2 = 51;
    public const int Eflat_2 = 51;
    public const int E_2 = 52;
    public const int F_2 = 53;
    public const int Fsharp_2 = 54;
    public const int Gflat_2 = 54;
    public const int G_2 = 55;
    public const int Gsharp_2 = 56;
    public const int Aflat_2 = 56;
    public const int A_2 = 57;
    public const int Asharp_2 = 58;
    public const int Bflat_2 = 58;
    public const int B_2 = 59;
    public const int C_3 = 60;
    public const int Csharp_3 = 61;
    public const int Dflat_3 = 61;
    public const int D_3 = 62;
    public const int Dsharp_3 = 63;
    public const int Eflat_3 = 63;
    public const int E_3 = 64;
    public const int F_3 = 65;
    public const int Fsharp_3 = 66;
    public const int Gflat_3 = 66;
    public const int G_3 = 67;
    public const int Gsharp_3 = 68;
    public const int Aflat_3 = 68;
    public const int A_3 = 69;
    public const int Asharp_3 = 70;
    public const int Bflat_3 = 70;
    public const int B_3 = 71;
    public const int C_4 = 72;
    public const int Csharp_4 = 73;
    public const int Dflat_4 = 73;
    public const int D_4 = 74;
    public const int Dsharp_4 = 75;
    public const int Eflat_4 = 75;
    public const int E_4 =76;
    public const int F_4 = 77;
    public const int Fsharp_4 = 78;
    public const int Gflat_4 = 78;
    public const int G_4 = 79;
    public const int Gsharp_4 = 80;
    public const int Aflat_4 = 80;
    public const int A_4 = 81;
    public const int Asharp_4 = 82;
    public const int Bflat_4 = 82;
    public const int B_4 = 83;
    public const int C_5 = 84;
    public const int Csharp_5 = 85;
    public const int Dflat_5 = 85;
    public const int D_5 = 86;
    public const int Dsharp_5 = 87;
    public const int Eflat_5 = 87;
    public const int E_5 = 88;
    public const int F_5 = 89;
    public const int Fsharp_5 = 90;
    public const int Gflat_5 = 90;
    public const int G_5 = 91;
    public const int Gsharp_5 = 92;
    public const int Aflat_5 = 92;
    public const int A_5 = 93;
    public const int Asharp_5 = 94;
    public const int Bflat_5 = 94;
    public const int B_5 = 95;
    public const int C_6 = 96;
    public const int Csharp_6 = 97;
    public const int Dflat_6 = 97;
    public const int D_6 = 98;
    public const int Dsharp_6 = 99;
    public const int Eflat_6 = 99;
    public const int E_6 = 100;
    public const int F_6 = 101;
    public const int Fsharp_6 = 102;
    public const int Gflat_6 = 102;
    public const int G_6 = 103;
    public const int Gsharp_6 = 104;
    public const int Aflat_6 = 104;
    public const int A_6 = 105;
    public const int Asharp_6 = 106;
    public const int Bflat_6 = 106;
    public const int B_6 = 107;
    public const int C_7 = 108;
    public const int Csharp_7 = 109;
    public const int Dflat_7 = 109;
    public const int D_7 = 110;
    public const int Dsharp_7 = 111;
    public const int Eflat_7 = 111;
    public const int E_7 = 112;
    public const int F_7 = 113;
    public const int Fsharp_7 = 114;
    public const int Gflat_7 = 114;
    public const int G_7 = 115;
    public const int Gsharp_7 = 116;
    public const int Aflat_7 = 116;
    public const int A_7 = 117;
    public const int Asharp_7 = 118;
    public const int Bflat_7 = 118;
    public const int B_7 = 119;
    #endregion
    public enum Note_Types 
    {
        quarter_note,
        half_note,
        whole_note,
        eighth_note,
        sixteenth_note,
        dotted_quarter_note,
        dotted_half_note,
        dotted_whole_note,
        dotted_eighth_note,
        dotted_sixteenth_note,
        quarter_rest,
        half_rest,
        whole_rest,
        eighth_rest
    }


    float StartingBeat;
    int PianoKey; // # of the key on the piano. 0 indicates a rest.
    Note_Types Note_Type; // duration, in beats

    public Note(float startingBeat, int pianoKey, Note_Types note_type)
    {
        StartingBeat = startingBeat;
        PianoKey = pianoKey;
        Note_Type = note_type;
    }

    public float GetDuration()
    {
        switch (Note_Type)
        {
            case Note_Types.quarter_note:
            case Note_Types.quarter_rest:
                return 1f;
            case Note_Types.half_note:
            case Note_Types.half_rest:
                return 2f;
            case Note_Types.whole_note:
            case Note_Types.whole_rest:
                return 4f;
            case Note_Types.eighth_note:
            case Note_Types.eighth_rest:
                return 0.5f;
        }
        return 0.00003f;
    }

    public float GetStartingBeat()
    {
        return StartingBeat;
    }

    public int GetPianoKey()
    {
        return PianoKey;
    }

    public Note_Types GetNoteType()
    {
        return Note_Type;
    }
}