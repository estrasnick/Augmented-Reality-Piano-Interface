using UnityEngine;

public class PlaybackCommands : MonoBehaviour
{
    Vector3 originalPosition;
    bool IsPaused = false;

    // Use this for initialization
    void Start()
    {
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        switch (this.name)
        {
            case "PlayPause":
                if (IsPaused)
                {
                    renderer.material.color = new Color(255, 255, 255);
                }
                else
                {
                    renderer.material.color = new Color(0, 0, 0);
                }
                IsPaused = !IsPaused;
                break;
            case "FastForward":
                renderer.material.color = new Color(255, 0, 0);
                break;
            case "Rewind":
                renderer.material.color = new Color(0, 255, 0);
                break;
            case "Record":
                renderer.material.color = new Color(0, 0, 255);
                break;
            default:
                renderer.material.color = new Color(0, 0, 0);
                break;
        }
        
        
    }
}