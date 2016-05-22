

using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    // The layer we care about is the "Keys" layer
    private const string LAYER_NAME = "Interactive";
    private const string NORMAL_SHADER_NAME = "Mobile/Diffuse";
    private const string HIGHLIGHTED_SHADER_NAME = "Mobile/Bumped Specular";

    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }
    
    // Shaders 
    Shader normal;
    Shader highlighted;

    int layerMask;

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        // Set the mask for the designated interactive layer. 
        layerMask = 1 << LayerMask.NameToLayer(LAYER_NAME);

        // Get shaders
        normal = Shader.Find(NORMAL_SHADER_NAME);
        highlighted = Shader.Find(HIGHLIGHTED_SHADER_NAME);

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                string message = "OnSelect";
                if (FocusedObject.tag != "Untagged")
                {
                     message += FocusedObject.tag;
                }
                FocusedObject.SendMessageUpwards(message);
            }
        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Helps with debugging. Hit "space" to emulate a detected gesture.
        if (Input.GetKeyDown("space") && (FocusedObject != null))
        {
            string message = "OnSelect";
            if (FocusedObject.tag != "Untagged")
            {
                message += FocusedObject.tag;
            }
            FocusedObject.SendMessageUpwards(message);
        }

        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, Mathf.Infinity, layerMask))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;

            // If there was no old focused object, then this one is also the old one. 
            if (oldFocusObject == null)
            {
                oldFocusObject = FocusedObject;
            }

            MeshRenderer hitMesh = FocusedObject.GetComponent<MeshRenderer>();
            hitMesh.material.shader = highlighted;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;

            if (oldFocusObject != null)
            {
                MeshRenderer hitMesh = oldFocusObject.GetComponent<MeshRenderer>();
                hitMesh.material.shader = normal;
            }
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}