using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class BPMCommand : MonoBehaviour
{
    Text text;
    public static float speed = 1.0f;

    // Use this for initialization
    void Start()
    {
        text = this.gameObject.GetComponentInChildren<Text>();
        if (speed < 0)
        {
            text.text = "---";
        }
        else
        {
            DisplaySpeed();
        }
    }

    void Update()
    {
        if (Timing.GetCurrentBPM() != Mathf.CeilToInt(Song.GetCurrentSong().GetDefaultBPM() * speed))
        {
            int updatedBPM = Mathf.CeilToInt(Song.GetCurrentSong().GetDefaultBPM() * speed);
            Timing.SetBPM(updatedBPM);
            DisplaySpeed();
        }
    }

    void DisplaySpeed()
    {
        float defaultSpeed = Song.GetCurrentSong().GetDefaultBPM();
        float relativeSpeed = speed / defaultSpeed;
        text.text = "BPM: " + Timing.CurrentBPM + "\n" + "Speed: " + Mathf.CeilToInt(speed * 100).ToString() + "%";
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelectUp()
    {
        if (speed < 2.0f)
        {
            speed += 0.25f;
            int updatedBPM = Mathf.CeilToInt(Song.GetCurrentSong().GetDefaultBPM() * speed);
            Timing.SetBPM(updatedBPM);
            DisplaySpeed();
        }
    }

    void OnSelectDown()
    {
        if (speed > 0.25)
        {
            speed -= 0.25f;
            int updatedBPM = Mathf.CeilToInt(Song.GetCurrentSong().GetDefaultBPM() * speed);
            Timing.SetBPM(updatedBPM);
            DisplaySpeed();
        }
    }


}