// Finds out whether target is on the left or right side of the screen
using UnityEngine;
using System.Collections;

public class FOVTarget : MonoBehaviour
{
    public Transform target;
    Camera FOVCamera;
    private MeshRenderer[] leftMeshRenderer;
    private MeshRenderer rightMeshRenderer;

    void Start()
    {
        FOVCamera = Camera.main;

        //Grab the mesh renderer that's on the same object as this script.
        leftMeshRenderer = FOVCamera.GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        Vector3 viewPos = FOVCamera.WorldToViewportPoint(target.position);
        if (viewPos.x > 0.5F)
        {
            leftMeshRenderer[0].enabled = false;
            leftMeshRenderer[1].enabled = true;
        }
        else
        {
            leftMeshRenderer[0].enabled = true;
            leftMeshRenderer[1].enabled = false;
        }
    }
}