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

public class Song {
    string Title;
    string Composer;
    int Key;
    int BeatsPerMeasure;
    int BeatUnit;
    List<Bar> Bars;
    int DefaultBPM;
    int CurrentBPM;

    public Song(string title, string composer, int key, int beatsPerMeasure, int beatUnit, List<Bar> bars, int defaultBPM, int currentBPM = -1)
    {
        if (currentBPM == -1)
        {
            CurrentBPM = defaultBPM;
        }
        Title = title;
        Composer = composer;
        Key = key;
        BeatsPerMeasure = beatsPerMeasure;
        BeatUnit = beatUnit;
        Bars = bars;
        DefaultBPM = defaultBPM;
    }
}

public class Bar
{
    List<Note> Notes;

    public Bar(List<Note> notes)
    {
        Notes = notes;
    }
}

public class Note
{
    static int REST = 0;

    int StartingBeat;
    int PianoKey; // # of the key on the piano. 0 indicates a rest.
    int Duration; // duration, in beats

    public Note(int startingBeat, int pianoKey, int duration)
    {
        StartingBeat = startingBeat;
        PianoKey = pianoKey;
        Duration = duration;
    }
}