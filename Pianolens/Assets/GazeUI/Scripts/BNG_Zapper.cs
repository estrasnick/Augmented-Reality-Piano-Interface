using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * No Touch GUI for VR
 * By Sean Smith @ Bacon Neck Games
 * 
 * BNG_Zapper.cs
 * 
 * This script should be placed on the object that will project the raycast (ie Crosshairs, camera, laser gun, etc).
 * It will react to any object with a BNG_ZapperAction script attached and/or a NoTouchGUI tag.
 * If you are having issues, see the Support Doc's Troubleshooting section
 * 
 */

public class BNG_Zapper : MonoBehaviour
{

    #region public vars

    #if UNITY_EDITOR
    [Tooltip("Only use a color here if you have a sprite renderer component attached, it will change the sprite's color. Otherwise, ignore it.")]
    #endif
    public Color colorOnHover = Color.white;
    
    #if UNITY_EDITOR
    [Tooltip("Only use a color here if you have a sprite renderer component attached, it will change the sprite's color. Otherwise, ignore it.")]
    #endif
    public float rayLength = 2.0f; //public so you can set it, but also access and manipulate for certain situations

    #endregion

    #region private vars
    private Color defaultColor;
    private Transform pos;
    private RaycastHit hit;
    [HideInInspector]
    public float timeOnButton; // current time on hover button
    [HideInInspector]
    public bool onButton;
    private GameObject currentGameObject;
    private List<GameObject> zappedObjects; // a list of objects currently being zapped, only allow 1 to zap at a time
    private static BNG_ZapperAction za;
    private Vector3 fwd;
    private SpriteRenderer sr;
    private bool usingSprites = true;
    #endregion

    // Use this for initialization
	void Start () {

        timeOnButton = 0;
        onButton = false;

        //check if we have a Sprite Renderer and if not, set usingSprites to false
        if (GetComponent<SpriteRenderer>() != null)
        {
            sr = GetComponent<SpriteRenderer>();
            defaultColor = sr.color;
            usingSprites = true;
        }
        else
        {
            usingSprites = false;
        }

        zappedObjects = new List<GameObject>();

	}
	
	// Update is called once per frame
    void Update()
    {
        fwd = transform.TransformDirection(Vector3.forward);
        CastRay(fwd);
        //Debug.Log(timeOnButton); 

    }

    void CastRay(Vector3 fwd) {

        //Debug.DrawRay(transform.position, fwd, Color.blue, 1f); //draws a line in the editor's scene window so you can see where the raycast is pointing

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength)
            && hit.transform.gameObject.GetComponent<BNG_ZapperAction>() != null) //if you have the tag, great, if not, check for BNG_Zapper Script
        {
            onButton = true;
            currentGameObject = hit.transform.gameObject;
            if (zappedObjects.Count < 1) zappedObjects.Add(currentGameObject);
            za = zappedObjects[0].GetComponent<BNG_ZapperAction>();
            if (za.canActivate && usingSprites) sr.color = colorOnHover; //change color of crosshairs
            DoHit(hit, zappedObjects[0], za);            

            //Debug.Log("Ray is Hitting: " + currentGameObject.name);
        }
        else
        {
            //Debug.Log("No objects with BNG_ZapperAction script detected.");

            if (zappedObjects.Count > 0)
            {
                onButton = false;
                if (usingSprites) sr.color = defaultColor;
                za.resetBar(this, timeOnButton);
                timeOnButton = 0;
                currentGameObject = null;
                if (zappedObjects.Count > 0) zappedObjects.RemoveAt(0);
            }
        }
    }

    void DoHit(RaycastHit hitInfo, GameObject hitObject, BNG_ZapperAction za)
    {

        if (za.canActivate)
        {
            print("Activated");
            timeOnButton += Time.deltaTime;
            za.ActivateTimer(this, timeOnButton);
        }
        else
        {

            print("Cannot Activate");

            if(usingSprites) sr.color = defaultColor;
            onButton = false;
            if (zappedObjects.Count > 0) zappedObjects.RemoveAt(0);
        }
        
    }    
}
