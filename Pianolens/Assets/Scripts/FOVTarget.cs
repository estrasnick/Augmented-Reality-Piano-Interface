// Finds out whether target is on the left or right side of the screen
using UnityEngine;
using System.Collections;

public class FOVTarget : MonoBehaviour
{
    public Transform target;
    Camera FOVCamera;
    private MeshRenderer leftMeshRenderer;
    private MeshRenderer rightMeshRenderer;
    private const string LAYER_NAME = "Keys";
    private const float FOV_THRESHOLD = 0.25f;
    int layerMask;

    void Start()
    {
        FOVCamera = Camera.main;

        // Set the mask for the designated interactive layer. 
        layerMask = 1 << LayerMask.NameToLayer(LAYER_NAME);

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
        var headPosition = FOVCamera.transform.position;
        var gazeDirection = FOVCamera.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, Mathf.Infinity, layerMask)) 
        {
            Vector3 viewPos = FOVCamera.WorldToViewportPoint(target.position);

            if (viewPos.x > (1.0f - FOV_THRESHOLD))
            {
                print(viewPos.x.ToString() + " target is on the right side!");
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
                print(viewPos.x.ToString() + " target is on the left side!");
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
        else
        {
            leftMeshRenderer.enabled = false;
            rightMeshRenderer.enabled = false;
        }
    }
}