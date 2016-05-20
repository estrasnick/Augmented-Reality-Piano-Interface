// Finds out whether target is on the left or right side of the screen
using UnityEngine;
using System.Collections;

public class FOVTarget : MonoBehaviour
{
    public Transform target;
    Camera FOVCamera;
    private MeshRenderer leftMeshRenderer;
    private MeshRenderer rightMeshRenderer;
    private const int LAYER_KEYS = 8;
    private const float FOV_THRESHOLD = 0.2f;

    void Start()
    {
        FOVCamera = Camera.main;

        //Grab the mesh renderer that's on the same object as this script.
        MeshRenderer[] renderers = FOVCamera.GetComponentsInChildren<MeshRenderer>();
        leftMeshRenderer = renderers[0];
        rightMeshRenderer = renderers[1];
        leftMeshRenderer.enabled = false;
        rightMeshRenderer.enabled = false;
    }

    void Update()
    {
        //Do a raycast into the world based on the user's 
        //head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, LAYER_KEYS)) 
        {
            Vector3 viewPos = FOVCamera.WorldToViewportPoint(target.position);
            if (viewPos.x > (1.0f - FOV_THRESHOLD))
            {
                // Move theCursor to the point where the raycase hit. 
                rightMeshRenderer.transform.position = hitInfo.point;
                // Rotate the cursor to hug the surface of the hologram
                rightMeshRenderer.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                rightMeshRenderer.transform.Rotate(hitInfo.normal, 270);

                leftMeshRenderer.enabled = false;
                rightMeshRenderer.enabled = true;
            }
            else if (viewPos.x < FOV_THRESHOLD)
            {
                // Move theCursor to the point where the raycase hit. 
                leftMeshRenderer.transform.position = hitInfo.point;
                // Rotate the cursor to hug the surface of the hologram
                leftMeshRenderer.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                leftMeshRenderer.transform.Rotate(hitInfo.normal, 90);

                leftMeshRenderer.enabled = true;
                rightMeshRenderer.enabled = false;
            }
            else
            {
                leftMeshRenderer.enabled = false;
                rightMeshRenderer.enabled = false;
            }
        }
    }
}