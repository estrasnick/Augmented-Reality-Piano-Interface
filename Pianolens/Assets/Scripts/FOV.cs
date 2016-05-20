using UnityEngine;
using System.Collections;

public class FOV : MonoBehaviour
{

    private MeshRenderer[] meshRenderers;

    // Use this for initialization
    void Start()
    {
        //GameObject theObject = GameObject.Find("CursorTopLeft");
        //Grab the mesh renderer within the object 
        //theObject.transform.position = new Vector3(0.2f, 0.2f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        ////Do a raycast into the world based on the user's 
        ////head position and orientation.
        //var headPosition = Camera.main.transform.position;
        //var gazeDirection = Camera.main.transform.forward;

        //RaycastHit hitInfo;

        //if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        //{
        //    // If the raycast hit a hologram...
        //    // Display the cursor mesh.
        //    meshRenderer.enabled = true;

        //    // Move theCursor to the point where the raycase hit. 
        //    this.transform.position = hitInfo.point;

        //    // Rotate the cursor to hug the surface of the hologram
        //    this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        //}
        //else
        //{
        //    // If the raycast did not hit a hologram, hide the cursor mesh. 
        //    meshRenderer.enabled = false;
        //}
    }
}
