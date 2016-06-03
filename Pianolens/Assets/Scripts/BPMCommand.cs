using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class BPMCommand : MonoBehaviour
{
    Text text;
    public static int speed = -1;

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
            text.text = speed.ToString();
        }
    }

    void Update()
    {
        if (Timing.GetCurrentBPM() != speed)
        {
            speed = Timing.GetCurrentBPM();
            text.text = speed.ToString();
        }
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelectUp()
    {
        if (speed > 0)
        {
            speed += 1;
            Timing.SetBPM(speed);
            text.text = speed.ToString();
        }
    }

    void OnSelectDown()
    {
        if (speed > 0)
        {
            speed -= 1;
            Timing.SetBPM(speed);
            text.text = speed.ToString();
        }
    }


}