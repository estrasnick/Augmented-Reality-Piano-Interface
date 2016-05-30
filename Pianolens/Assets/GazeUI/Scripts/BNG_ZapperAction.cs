using UnityEngine;
using System.Collections;

/* 
 * No Touch GUI for VR
 * By Sean Smith @ Bacon Neck Games
 * 
 * BNG_ZapperAction.cs
 * 
 * This script should be placed on any object that you want the raycast to interact with.
 * The object that has the BNG_Zapper.cs script attached will raycast to this script.
 * See Support Doc for a list of variables and their functions or hover over them in the inspector.
 * 
 */

public class BNG_ZapperAction : MonoBehaviour
{

    #region PUBLIC VARIABLES

    #if UNITY_EDITOR
        [Tooltip("For changing the color of the sprite button on hover. SPRITES ONLY. NO MESHES.")]
    #endif
    public bool changeColorOnHover = false; //only if using sprites such as a button or graphic, set to false if placing this script on a mesh object

    #if UNITY_EDITOR
        [Tooltip("For changing the color of the sprite button only when it is activated (not hover). SPRITES ONLY. NO MESHES.")]
    #endif
    public bool changeColorOnActivate = false; //only if using sprites such as a button or graphic, set to false if placing this script on a mesh object

    #if UNITY_EDITOR
        [Tooltip("The new color of the sprite if you chose one of the 2 color changing options above. SPRITES ONLY. NO MESHES.")]
    #endif
    public Color newColorOnHover; //only if using sprites such as a button or graphic, set to false if placing this script on a mesh object

    #if UNITY_EDITOR
        [Tooltip("Should the entire object rotate with the camera so it's always facing you? It will rotate the game object with the bng_ZapperAction script and any of its children.")]
    #endif
    public bool rotateObjectWithCamera = false; //should the button rotate with the camera so it's always facing you?

    #if UNITY_EDITOR
        [Tooltip("If this is seleced, it will rotate only the tooltip and the timer, but not the game object, to face the player when it displays. Best when the tooltip is in the Timer Object group.")]
    #endif
    public bool rotateTooltipAndTimerOnHover = false; //do you want to rotate the tool tip to face the player?

    #if UNITY_EDITOR
        [Tooltip("The GameObject tooltip that will appear when you are hovering over this object.")]
    #endif
    public GameObject tooltipGameObject; //the gameobject of the tooltip instead of using gui text/textures    

    #if UNITY_EDITOR
        [Tooltip("The image that the sprite will change to when deactivated, leave empty for no image change. SPRITES ONLY. NO MESHES.")]
    #endif
    public Sprite ReplacementSprite = null; //only if using sprites

    #if UNITY_EDITOR
        [Tooltip("If this is set to true, the button will only activate once, and have no deactivation state. Useful for one time use items, enemies, and other non-toggle cases.")]
    #endif
    public bool oneTimeUse = false; //if set to true, it will only activate once and have no deactivation action

    #if UNITY_EDITOR
        [Tooltip("If this is set to true, the button will only activate the item and no deactivation will ever take place. Useful for buttons like quit and continue that only have one activation state.")]
    #endif
    public bool resetOnActivate = false; //if set to true, it will only activate the item and no deactivation will ever take place, useful for continue and quit buttons that appear and disappear but have no deactivation state

    #if UNITY_EDITOR 
        [Tooltip("The time it takes to activate the button")]
    #endif
    public float timeToActivate = 1.0f; //the time it takes to activate the button

    #if UNITY_EDITOR
        [Tooltip("The time it takes to deactivate the button")] 
    #endif
    public float timeToDeactivate = 1.0f; //if different than the above, it will use this value to deactivate the button

    #if UNITY_EDITOR
        [Tooltip("The amount of time after activating/deactivating to wait before the ray will register that it is hitting this button again")]
    #endif
    public float waitTime = 3.0f; //the amount of time after activating/deactivating to register that the ray is hitting it again

    #if UNITY_EDITOR
        [Tooltip("A string that will be sent to your game object, activating a function of the same name. You should have a function in your game object's script with this string as the name")]
    #endif
    public string CallFunctionOnActivate = "";

    #if UNITY_EDITOR
        [Tooltip("This is a ref to a game object with a function named after the CallFunction above.")]
    #endif
    public GameObject GameObjectToActivate;

    #if UNITY_EDITOR
        [Tooltip("A string that will be sent to your game object when it is deactivated, activating a function of the same name. You should have a function in your game object's script with this string as the name")]
    #endif
    public string CallFunctionOnDeactivate = "";

    #if UNITY_EDITOR
        [Tooltip("You can leave blank if it's the same as GameObjectToActivate above. This is a ref to a game object with a function named after the CallFunctionOnDeactivate above. ")]
    #endif
    public GameObject GameObjectToDeactivate;

#if UNITY_EDITOR
        [Tooltip("If you call a function that isn't there, this will throw a compiler error and you can fix it.")]
#endif
        public bool debugSendMessage = false;

    #endregion


    #region PRIVATE VARIABLES
    
    private bool usingSprites = true; //only if using sprites such as a button or graphic, set to false if placing this script on a mesh object
    private bool tooltipOnHover = false; // do you want to display a tooltip when the user hovers over this object/button?
    private bool requireTimer; //does this game object use a timer indicator?
    private Transform toT; //timerObject Transform reference
    private GameObject timerObject; //timerObject
    private Transform toF; //timer object fill reference    
    private GameObject timerObjectFill;
    private SpriteRenderer sr;
    private Sprite DefaultSprite;
    [HideInInspector]
    public bool canActivate; //used for telling it to wait until the cool down period to check again for a hit
    [HideInInspector]
    public bool isActivated = false;
    private float yToBe;
    private GameObject cam;
    private bool hasTimerObject; //if the user does not require a timer but there is one available, we will use it
    [HideInInspector]
    public Color originalColor;
    private float originalAlpha;

    #endregion


    void Start () {

        //get camera so we know what the rotation is if we're using it
        if(rotateObjectWithCamera || rotateTooltipAndTimerOnHover){
            
            cam = GameObject.FindGameObjectWithTag("MainCamera");

            if (cam == null || !cam.activeInHierarchy)
            {
                Debug.LogWarning("NO CAMERA WAS FOUND ON YOUR SCENE. DID YOU FORGET TO ADD A PLAYER PREFAB?");
            }
        }

        if (GetComponent<Collider>() == null)
        {
            transform.gameObject.AddComponent<BoxCollider>();
            Debug.LogWarning(gameObject.name + ": Did not detect Physics Collider. Adding box collider for you.");
        }

        //get the sprite renderer so we can change the color when we hover over something we can interact with
        if (GetComponent<SpriteRenderer>() != null)
        {            
            sr = transform.GetComponent<SpriteRenderer>();
            DefaultSprite = sr.sprite;
            originalColor = sr.color;
            usingSprites = true;
        }
        else
        {
            //no sprite renderer attached means that this object isn't using sprites.
            usingSprites = false;

        }

        //setup the timer fill bar
        hasTimerObject = false; //don't assume they have a timer until it's verified by the check
        setupTimerBars(); //this scans the child of this object for a timer bar prefab and sets it up
                
        // do tooltip logic here. 
        if (tooltipGameObject != null)
        {
            tooltipGameObject.SetActive(false); //if it's there and you want to use it we're turning it off to hide it
            tooltipOnHover = true;
        }
        else //basically if you have tooltip set to true and you don't have a gameobject 
        {
            tooltipOnHover = false;
        }

        //allow button to be activated
        canActivate = true; 

        //set the button to not activated
        isActivated = false;

        //set color of button to the Sprite's original color
        if (usingSprites && (changeColorOnActivate || changeColorOnHover))  originalColor = sr.color;
      
	}

    void Update()
    {
        //check if you need to rotate anything and if not, forget about it
        if (rotateObjectWithCamera || rotateTooltipAndTimerOnHover)
        {
            if (rotateObjectWithCamera)
            {
                yToBe = cam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yToBe, transform.rotation.eulerAngles.z);
            }
            else if (rotateTooltipAndTimerOnHover)
            {
                yToBe = cam.transform.rotation.eulerAngles.y;
                if (tooltipOnHover) tooltipGameObject.transform.rotation = Quaternion.Euler(tooltipGameObject.transform.rotation.eulerAngles.x, yToBe, tooltipGameObject.transform.rotation.eulerAngles.z);
                if (requireTimer) timerObject.transform.rotation = Quaternion.Euler(timerObject.transform.rotation.eulerAngles.x, yToBe, timerObject.transform.rotation.eulerAngles.z);
            }
        }

    }


    public void ActivateTimer(BNG_Zapper zap, float time)
    { 
        
        // if changeColor on Hover is true (and is using sprites), use New Color on Hover to change the sprite
        if (changeColorOnHover && usingSprites)
        {
            sr.color = newColorOnHover;
        }


        if (canActivate) //if you can activate the timer (not in cooldown period)
        {
            if (requireTimer)  timerObject.SetActive(true); //turn the timer on

            if (tooltipOnHover) tooltipGameObject.SetActive(true); // if tooltipOnHover is true display it

            if (!isActivated) // if it's not activated yet
            {
                if(requireTimer) scaleTimer(time); //start the timer bar filling up

                if (time >= timeToActivate) // if you have reached the amount of time to activate, activate the button
                {                    
                    resetBar(zap, time); //this resets the bar and let's the zapper know to stop counting up

                    //wait cool down time before registering a hit.
                    canActivate = false;

                    StartCoroutine(WaitToActivate(waitTime));
                }

            }
            else // it is already activated so use the deactivation time
            {
               
                if (requireTimer) scaleTimer(time); //start the timer bar filling up
               

                if (time >= timeToDeactivate) // if you have reached the amount of time to deactivate, deactivate the button
                {
                    resetBar(zap, time); //this resets the bar and let's the zapper know to stop counting up

                    //wait cool down time before registering a hit.
                    canActivate = false;
                    StartCoroutine(WaitToActivate(waitTime));
                }
               
            }

        }
        else //if it shouldn't be activated, don't let the timer count up
        {
            time = 0;

        }

    }

    public void resetBar(BNG_Zapper zap, float time) //this function changes the object's color when activated, hides the timer and tooltip, and resets the timer here and on the zapper
    {
        // if changeColor on Hover is true (and is using sprites), use New Color on Hover to change the sprite
        if ((changeColorOnHover || changeColorOnActivate) && usingSprites)
        {
            if (!isActivated)
            {
                sr.color = originalColor;                
            }
            else if (isActivated && changeColorOnActivate)
            {
                sr.color = newColorOnHover;                
            }
            else
            {
                sr.color = originalColor;                
            }
        }
        
        if (tooltipOnHover && tooltipGameObject != null) tooltipGameObject.SetActive(false);

        if (timerObject != null) //if there is a timer bar, make it go away since it's no longer active, reset the time and tell the Zapper not to count up
        {
            timerObject.SetActive(false);            
        }

        time = 0;
        zap.timeOnButton = 0;

    }



    IEnumerator WaitToActivate(float waitTime) //this handles what happens on Activationthe cooldown period
    {

        //ACTIVATE

        if (!isActivated && CallFunctionOnActivate != "") //if it's not activated and you have a function to call, do it
        {
            if (GameObjectToActivate != null)
            {
                if (oneTimeUse)
                {
                    if(!debugSendMessage) GameObjectToActivate.SendMessage(CallFunctionOnActivate.ToString(), null, SendMessageOptions.DontRequireReceiver);
                    else GameObjectToActivate.SendMessage(CallFunctionOnActivate.ToString(), null, SendMessageOptions.RequireReceiver);
                    transform.gameObject.layer = 2; //change layer to Ignore Raycast so system won't read it anymore, rendering it useless
                } 
                else
                {
                    if (!debugSendMessage) GameObjectToActivate.SendMessage(CallFunctionOnActivate.ToString(), null, SendMessageOptions.DontRequireReceiver);
                    else GameObjectToActivate.SendMessage(CallFunctionOnActivate.ToString(), null, SendMessageOptions.RequireReceiver);
                }
            }
            else
            {
                Debug.LogWarning("You have entered a function to call on " + gameObject.name + " but forgot to set the game object that the function resides. Drag your game object with the correct function into the Game Object to Activate slot.");
            }
        }
        else if (!isActivated)
        {
            Debug.LogWarning("This object, " + gameObject.name + ", does not have a function or game object attached to it and therefore will do nothing. Consider adding a function call and game object.");
        }

        //DEACTIVATE

        if (isActivated && CallFunctionOnDeactivate != "" && !oneTimeUse && !resetOnActivate) // if it's activated and you have a function to deactivate it, do that, also if 1 time use is set to true, don't deactivate
        {
            if (GameObjectToDeactivate != null) //if they have a different game object to deactivate than was used to activate
            {
                if (!debugSendMessage) GameObjectToDeactivate.SendMessage(CallFunctionOnDeactivate.ToString(), null, SendMessageOptions.DontRequireReceiver);
                else GameObjectToDeactivate.SendMessage(CallFunctionOnDeactivate.ToString(), null, SendMessageOptions.RequireReceiver);
            }
            else if (CallFunctionOnActivate != "") // if they have the same or left blank the object to activate, use the game object to activate with the deactivate function
            {
                if (!debugSendMessage) GameObjectToActivate.SendMessage(CallFunctionOnDeactivate.ToString(), null, SendMessageOptions.DontRequireReceiver);
                else GameObjectToActivate.SendMessage(CallFunctionOnDeactivate.ToString(), null, SendMessageOptions.RequireReceiver);
            }
            else if(isActivated)
            {
                Debug.LogWarning("This object, " + gameObject.name + ", is Active, but you forgot to set the game object that the function resides. Drag your game object with the correct function into the Game Object to Deactivate slot.");
            }
        }
        
        // HANDLE ACTIVATION

        if (!isActivated && !resetOnActivate)
        {
            isActivated = true;
            if (usingSprites && changeColorOnActivate) sr.color = newColorOnHover;
        }
        else
        {
            isActivated = false;
            if (usingSprites && (changeColorOnActivate || changeColorOnHover)) sr.color = originalColor;
        }

        // SWITCH SPRITES

        if (usingSprites && ReplacementSprite != null && isActivated) //if you destroy a sprite on the activate function, it won't have a sprite renderer so we prevent errors by checking
        {
            sr.sprite = ReplacementSprite;
        }
        else if (usingSprites && DefaultSprite != null && !isActivated)
        {
            sr.sprite = DefaultSprite;
        }

        // THIS ONLY TRIGGERS AFTER THE COOLDOWN PERIOD

        if (!resetOnActivate)
        {
            yield return new WaitForSeconds(waitTime);
            canActivate = true; //after cooldown period, you can activate the button again
        }
        else
        {
            if (transform.gameObject.activeInHierarchy) //object has been activated and is still visible on screen
            {
                //print("Active and visible: " + transform.gameObject.name);
                yield return new WaitForSeconds(waitTime);
                canActivate = true; //after cooldown period, you can activate the button again
                
            }
            else if (transform.gameObject.activeSelf && !transform.gameObject.activeInHierarchy) //item is still activated but it's parent is not
            {
                //print("Active but not visible: " + transform.gameObject.name);
                canActivate = true;
                
            }
            else //item is not visible or active
            {
                canActivate = true;
                //print("NOT Active AND  not visible: " + transform.gameObject.name);
            }
           
        }

    }

    void setupTimerBars()
    {
        toT = transform.Find("HorizontalFillBar");

        if (toT == null)
        {
            toT = transform.Find("HorizontalFillBarForMenu"); //if there is no horizontal fill bar, check for the menu kind of fill bar first
        }

        if (toT != null && toT.gameObject.activeInHierarchy)
        {
            hasTimerObject = requireTimer = true;
            //it is horizontal fill
            timerObject = toT.gameObject;
            toF = timerObject.transform.Find("fill");
            timerObjectFill = toF.gameObject;
            timerObjectFill.transform.localScale = new Vector3(0, 1, 1);
            timerObject.SetActive(false);
        }
        else
        {
            toT = this.transform.Find("VerticalFillBar");

            if (toT != null && toT.gameObject.activeInHierarchy)
            {
                hasTimerObject = requireTimer = true;
                //it is vertical fill
                timerObject = toT.gameObject;
                toF = timerObject.transform.Find("fill");
                timerObjectFill = toF.gameObject;
                timerObjectFill.transform.localScale = new Vector3(0, 1, 1);
                timerObject.SetActive(false);
            }
            else
            {
                toT = this.transform.Find("CircularFill");

                if (toT != null && toT.gameObject.activeInHierarchy)
                {
                    hasTimerObject = requireTimer = true;
                    //it is radial fill
                    timerObject = toT.gameObject;
                    toF = timerObject.transform.Find("fill");
                    timerObjectFill = toF.gameObject;
                    timerObjectFill.GetComponent<Renderer>().material.SetFloat("_Cutoff", 0);
                    timerObject.SetActive(false);
                }
                else //no menu objects found
                {
                    hasTimerObject = false; //don't assume they have a timer until it's verified by the check, at this point, it's not there.
                    requireTimer = false;
                }

            }
        }
    }

    void scaleTimer(float time)
    {
        //this stuff is all pretty self explanatory

        if (requireTimer)
        {

            float scale;
            
            if (!isActivated)
            {
                scale = time / timeToActivate; //setup the scale to be a percentage of time/activation time
                //print(scale);
            }
            else
            {
                scale = time / timeToDeactivate; //setup the scale to be a percentage of time/deactivation time
                //print(scale);
            }

            //conditionals for type of fill bar

            if (timerObject.name == "HorizontalFillBar" && timerObject.activeInHierarchy)
            {
                timerObjectFill.transform.localScale = new Vector3(scale, 1, 1);
            }

            else if (timerObject.name == "HorizontalFillBarForMenu" && timerObject.activeInHierarchy) //created specifically for menu items for the demo, same as horizontal fill bar though
            {
                timerObjectFill.transform.localScale = new Vector3(scale, 1, 1);
            }

            else if (timerObject.name == "VerticalFillBar" && timerObject.activeInHierarchy)
            {
                timerObjectFill.transform.localScale = new Vector3(scale, 1, 1);
            }

            else if (timerObject.name == "CircularFill" && timerObject.activeInHierarchy) //the radial fill type is dependend on the type of shader being used and the version of Unity
            {
                float cutoff = 1 - scale;
                //print("Cutoff: " + cutoff);

                timerObjectFill.GetComponent<Renderer>().material.SetFloat("_Cutoff", cutoff);
            }
        }
    }

}
