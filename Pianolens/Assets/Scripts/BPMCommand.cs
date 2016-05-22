using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class BPMCommand : MonoBehaviour
{
    MeshRenderer upKey;
    MeshRenderer downKey;
    Text text;
    public static int speed = 100;

    // Use this for initialization
    void Start()
    {
        // Grab the original local position of the sphere when the app starts.
        MeshRenderer[] renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in renderers)
        {
            if (mr.tag == "Up")
                upKey = mr;
            else if (mr.tag == "Down")
                downKey = mr;
        }
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