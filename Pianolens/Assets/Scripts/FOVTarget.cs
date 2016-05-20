// Finds out whether target is on the left or right side of the screen
using UnityEngine;
using System.Collections;

public class FOVTarget : MonoBehaviour
{
    public Transform target;
    Camera FOVCamera;

    void Start()
    {
        FOVCamera = Camera.main;
    }

    void Update()
    {
        Vector3 viewPos = FOVCamera.WorldToViewportPoint(target.position);
        if (viewPos.x > 0.5F)
            print("target is on the right side!");
        else
            print("target is on the left side!");
    }
}