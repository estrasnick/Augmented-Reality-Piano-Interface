using UnityEngine;
using System.Collections;

public class PianoDescriptor : MonoBehaviour
{
    int numOfScales = 7;
    int numKeys = 7*12+4;
    int numWhiteKeys = 52;

    private float[] keyWidthRatio = new float[13] {0, 14, 14, 14, 14, 14, 13, 14, 13, 14, 13, 14, 13};
    private float[] whiteKeyWidthRatio = new float[13] {0, 23, 0, 24,0, 23, 24,0, 23,0, 23,0, 24 };
    //these arrays are recomputed so that it is of the format [0, 0.1, 0.2 ... ~ 1]

    private static PianoDescriptor descriptor;

    private Vector3 startPos, endPos, singleOctave;

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
    public PianoDescriptor():this(7)
    {
        float keyWidthRatioSum = 0;
        float whiteKeyWidthRatioSum = 0;
        for (int i = 0; i < keyWidthRatio.Length; i++)
        {
            keyWidthRatioSum += keyWidthRatio[i];
        }
        for (int i = 0; i < whiteKeyWidthRatio.Length; i++)
        {
            whiteKeyWidthRatioSum += whiteKeyWidthRatio[i];
        }

        float start = 0;
        for (int i = 0; i < keyWidthRatio.Length; i++)
        {
            keyWidthRatio[i] /= keyWidthRatioSum;
            float temp = keyWidthRatio[i];
            keyWidthRatio[i] += start;
            start += temp;
        }
        start = 0;
        for (int i = 0; i < whiteKeyWidthRatio.Length; i++)
        {
            whiteKeyWidthRatio[i] /= whiteKeyWidthRatioSum;
            float temp = whiteKeyWidthRatio[i];
            whiteKeyWidthRatio[i] += start;
            start += temp;
            print(whiteKeyWidthRatio[i]);
        }
    }

    public PianoDescriptor(int numOfScales)
    {
        this.NumKeys = numOfScales*12 + 4;
        this.NumOfScales = numOfScales;
    }

    /// <summary>
    /// Sets up the piano's start and begin 
    /// </summary>
    /// <param name="start">start-position of the first octave</param>
    /// <param name="end">end-position of the last octave</param>
    public void setUpPiano(Vector3 start, Vector3 end)
    {
        //using the start and end
        this.startPos = start; this.endPos = end;
        singleOctave = (end - start) / (float)NumOfScales;
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
    /// <param name="overrideWhite">if true, then it will use the top pianokey width regardless of if input key was white or black.</param>
    /// <returns></returns>
    public Vector3[] getKeyWidth(int keyId, bool overrideWhite)
    {
        Vector3[] keySpace = new Vector3[2];
        //Vector3 absolutePosition = (end - start)*((float)(keyId-21) / (float)(108-21));
        //Fixme: the -1 might be right or might be wrong. Testing is needed.

        Vector3 initialOffset = startPos + singleOctave * Mathf.Floor((keyId - 24f) / 12f);


        if (overrideWhite)
        {
            keySpace[0] = initialOffset + singleOctave * keyWidthRatio[(keyId) % 12] ;
            keySpace[1] = initialOffset + singleOctave * keyWidthRatio[(keyId) % 12 +1 ];
        }
        else
        {
            switch (keyId % 12) // 23, 0, 24,0, 23, 24,0, 23,0, 23,0, 24 
            {
                case 0: case 2: case 4: case 5: case 7: case 9: case 11: //WHITE KEY
                    keySpace[0] = initialOffset + singleOctave * whiteKeyWidthRatio[(keyId) % 12] ;
                    keySpace[1] = initialOffset + singleOctave * whiteKeyWidthRatio[(keyId) % 12+1] ;
                    break;
                case 1: case 3: case 6: case 8: case 10: //BLACK KEY\
                    keySpace[0] = initialOffset + singleOctave * keyWidthRatio[(keyId) % 12] ;
                    keySpace[1] = initialOffset + singleOctave * keyWidthRatio[(keyId) % 12+1] ;
                    break;
            }
        }
        
        return keySpace;
    }


}
