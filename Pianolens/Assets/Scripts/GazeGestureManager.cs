

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    static float PLAYBACK_MENU_X_THRESHOLD = .1f;
    static float PLAYBACK_MENU_Y_THRESHOLD = .1f;
    static Color DEFAULT_TEXT_COLOR = new Color(50, 50, 50, 255);
    static Color HIDDEN_TEXT_COLOR = new Color(0, 0, 0, 0);

    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GameObject PlaybackMenu;
    Text BPMText;

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        PlaybackMenu = GameObject.Find("PlaybackMenu");
        //BPMText = GameObject.Find("BPMText").GetComponent<Text>();

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        // Update the visibility of the playback menu based on head position
        bool visibility = CheckPlaybackMenuVisibility(gazeDirection);
        //SetVisibility(PlaybackMenu, visibility);
            
        // Canvas rendering is different from mesh rendering, so we need to alter the text separately
        //BPMText.color = visibility ? DEFAULT_TEXT_COLOR : HIDDEN_TEXT_COLOR;
        
        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
    
    // Recursive function to set the visibility of which and all children to visibility
    private void SetVisibility(GameObject which, bool visibility)
    {
        // set the object's renderer
        which.GetComponent<Renderer>().enabled = visibility;

        // and the renderers of all child objects
        foreach (Transform child in which.transform)
        {
            SetVisibility(child.gameObject, visibility);
        }
    }

    private bool CheckPlaybackMenuVisibility(Vector3 gazeDirection)
    {
        return (gazeDirection.y > PLAYBACK_MENU_Y_THRESHOLD && gazeDirection.x > PLAYBACK_MENU_X_THRESHOLD);
    }
}