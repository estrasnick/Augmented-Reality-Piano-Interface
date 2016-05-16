using UnityEngine;
using System.Collections;

public class PianoDescriptor {
    int numOfScales = 7;
    int numKeys = 7*12+4;
    int numWhiteKeys = 52;

    private static PianoDescriptor descriptor;

    public int NumOfScales
    {
        get
        {
            return numOfScales;
        }

        set
        {
            numOfScales = value;
        }
    }

    public int NumKeys
    {
        get
        {
            return numKeys;
        }

        set
        {
            numKeys = value;
        }
    }

    public int NumWhiteKeys
    {
        get
        {
            return numWhiteKeys;
        }

        set
        {
            numWhiteKeys = value;
        }
    }

    /// <summary>
    /// Static function return a Piano Descriptor singleton.
    /// </summary>
    /// <returns></returns>
    public static PianoDescriptor getPianoDescriptor()
    {
        if (descriptor == null) descriptor = new PianoDescriptor();
        return descriptor;
    }

    /// <summary>
    /// Currently assumes a piano starting on A0 and Ending on C8(id 108).
    /// </summary>
    public PianoDescriptor()
    {
        NumKeys = 7*12+4;
        NumOfScales = 7;
    }

    public PianoDescriptor(int numOfScales)
    {
        this.NumKeys = numOfScales*12 + 4;
        this.NumOfScales = numOfScales;
    }

    //http://computermusicresource.com/midikeys.html
    //Note 60 is the central C3, and others are around it.
    //the lowest note on an 88 is A. i.e. 21. 
    //Lowest C is the 24.
    //highest note on an 88 is a 108 (C8)
    /// <summary>
    /// Gets the key width given two vector references denoting the absolute position of the center of the first and last key on the piano.
    /// </summary>
    /// <param name="keyId">Absolute Key ID as used by MIDI standard on http://computermusicresource.com/midikeys.html </param>
    /// <param name="isWhite">If is white, then we return the full width fo the white key, otherwise we just use a standard key width.</param>
    /// <param name="start">Absolute Start 3D vector position for the first key (center of key)</param>
    /// <param name="end">Absolute start 3D vector position for the last key (center)</param>
    /// <returns></returns>
    public Vector3[] getKeyWidth(int keyId, bool isWhite, ref Vector3 start, ref Vector3 end)
    {
        Vector3[] keySpace = new Vector3[2];
        Vector3 absolutePosition = (end - start)*((float)(keyId-21-1) / (float)(108-21-1)) ;
        //Fixme: the -1 might be right or might be wrong. Testing is needed.
        if (isWhite)
        {
            Vector3 perWhiteKeyWidth = (end - start) / (float)NumWhiteKeys;
            keySpace[0] = absolutePosition - perWhiteKeyWidth + start;
            keySpace[1] = absolutePosition + perWhiteKeyWidth + start;
        }
        else
        {
            Vector3 perKeyWidth = (end - start) / (float)NumKeys;
            keySpace[0] = absolutePosition - perKeyWidth + start;
            keySpace[1] = absolutePosition + perKeyWidth + start;
        }
        
        return keySpace;
    }


}
