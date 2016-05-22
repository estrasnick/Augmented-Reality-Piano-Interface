using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class BPMCommand : MonoBehaviour
{
    Text text;
    public static int speed = 100;

    // Use this for initialization
    void Start()
    {
        text = this.gameObject.GetComponentInChildren<Text>();
        text.text = speed.ToString();
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelectUp()
    {
        speed += 1;
        text.text = speed.ToString();
    }

    void OnSelectDown()
    {
        speed -= 1;
        text.text = speed.ToString();
    }
}