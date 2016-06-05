using UnityEngine;
using System.Collections;

public struct SongEvent
{
    public bool isStart;
    public int keyID;
    public Note.Note_Types noteType;
    public float measureNumber;

    public SongEvent(bool isStart, int keyID, Note.Note_Types note_Types, float v3) : this()
    {
        this.isStart = isStart;
        this.keyID = keyID;
        this.noteType = note_Types;
        this.measureNumber = v3;
    }
}

public static class SongAnalyzer
{
    private class sortYearAscendingHelper : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            SongEvent c1 = (SongEvent)a;
            SongEvent c2 = (SongEvent)b;

            if (c1.measureNumber > c2.measureNumber)
                return 1;

            if (c1.measureNumber < c2.measureNumber)
                return -1;

            else
                return 0;
        }
    }
    public static ArrayList generateEventList(Song s)
    {
        return generateEventList(s, 0f);
    }
    /// <summary>
    /// Generates an event list with a fixed amount of tolerance for each event.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="tolerance">Amount in beats to tolerate issues. Recommended 0.5</param>
    /// <returns></returns>
    public static ArrayList generateEventList(Song s, float tolerance)
    {
        ArrayList l = new ArrayList();
        int barNum = 1;
        foreach(Bar b in s.GetBars())
        {
            foreach(Note n in b.GetNotes())
            {
                //TODO: add actual measure calculation here.
                l.Add(new SongEvent(true, n.GetPianoKey(), n.GetNoteType(), barNum * Timing.BeatsPerMeasure + n.GetStartingBeat() - tolerance));
                l.Add(new SongEvent(false, n.GetPianoKey(), n.GetNoteType(), barNum * Timing.BeatsPerMeasure + n.GetStartingBeat()+n.GetDuration() + tolerance));
            }
            barNum++;
        }

        l.Sort(new sortYearAscendingHelper());
        return l;
    }
}

/*public class SongPlayer : MonoBehaviour {
    Song currentSong;
    ArrayList songEvents;
    int currentSongEventsIndex = 0;
    int currentTick = 0;
	// Use this for initialization
	void Start () {
        //current song is something? it'scurrently NULL.
        //TODO: deBUG.
        songEvents = SongAnalyzer.generateEventList(currentSong);
	}
	
	// Update is called once per frame
	void Update () {

        //be curious about player's playing stats. 
        
        //if player is playing properly: (how do we define this?)

        //increment current time by X. where X is dependent on frametime using Time.deltaTime
        double lastFrameTime = Time.deltaTime;
        //design decision: do we use TIME? or do we use MEASURES?
            //TIME can pass when player is not playing anything but MEASURES cannot.
            //TIME cannot map directly to key that'll be down at that time range.
            //MEASURE however can get more close to the key due to quantized nature.
            //MEASURE is not necessarily accurate
        //assuming we get some song event somewhere:
            //FOR ALL SONG EVENT ending on this frame:
                //draw a cube depicting the note using PianoDescriptor to access the center
                //position the cube correctly such that it is ready to be animated, depending on how far the START position of this cube is.
                //by reverse computing the start TIME using the NoteEvent's noteType and measurementNumber:
                //append the animation script to the gameobject that we create.
        //fire off necessary events about what's happening where for the emitter.
        //look ahead X bars, and also do the same thing.

	}
}
*/