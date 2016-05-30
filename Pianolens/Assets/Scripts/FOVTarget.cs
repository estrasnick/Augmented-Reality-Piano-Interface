// Finds out whether target is on the left or right side of the screen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FOVTarget : MonoBehaviour
{
    Camera FOVCamera;
    public MeshRenderer leftMeshRenderer;
    public MeshRenderer rightMeshRenderer;
    private const string LAYER_NAME = "Keys";
    private const float FOV_THRESHOLD = 0.1f;
    int layerMask;

    void Start()
    {
        FOVCamera = Camera.main;

        // Set the mask for the designated interactive layer. 
        layerMask = 1 << LayerMask.NameToLayer(LAYER_NAME);

        //Grab the mesh renderer that's on the same object as this script.
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
            SortedList litKeys = new SortedList();
            foreach (GameObject litKey in GameObject.FindGameObjectsWithTag("HighlightedKey"))
            {
                litKeys.Add(litKey.name, litKey);
            }
            // Don't have to do anything if there are no lit keys.
            int numLitKeys = litKeys.Count;
            if (numLitKeys < 1)
            {
                rightMeshRenderer.enabled = false;
                leftMeshRenderer.enabled = false;
                return;
            }

            GameObject firstKey = litKeys.GetByIndex(0) as GameObject;
            GameObject lastKey = litKeys.GetByIndex(numLitKeys - 1) as GameObject;

            // Show the right direction arrow if the last keyt is too far to the right.
            Vector3 viewPosRight = FOVCamera.WorldToViewportPoint(lastKey.transform.position);
            if (viewPosRight.x > (1.0f - FOV_THRESHOLD))
            {
                print(viewPosRight.x.ToString() + " target is on the right side!");
                // Move theCursor to the point where the raycase hit. 
                rightMeshRenderer.transform.position = hitInfo.point;
                // Rotate the cursor to hug the surface of the hologram
                rightMeshRenderer.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                rightMeshRenderer.transform.Rotate(hitInfo.normal, 270);

                rightMeshRenderer.enabled = true;
            }
            else
            {
                rightMeshRenderer.enabled = false;
            }

            // Show the left direction arrow if the last keyt is too far to the right.
            Vector3 viewPosLeft = FOVCamera.WorldToViewportPoint(firstKey.transform.position);
            if (viewPosLeft.x < FOV_THRESHOLD)
            {
                print(viewPosLeft.x.ToString() + " target is on the left side!");
                // Move theCursor to the point where the raycase hit. 
                leftMeshRenderer.transform.position = hitInfo.point;
                // Rotate the cursor to hug the surface of the hologram
                leftMeshRenderer.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                leftMeshRenderer.transform.Rotate(hitInfo.normal, 90);

                leftMeshRenderer.enabled = true;
            }
            else
            {
                leftMeshRenderer.enabled = false;
            }
        }
        else
        {
            leftMeshRenderer.enabled = false;
            rightMeshRenderer.enabled = false;
        }
    }
}