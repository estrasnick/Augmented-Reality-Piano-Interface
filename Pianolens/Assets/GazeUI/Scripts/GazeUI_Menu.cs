using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GazeUI_Menu : MonoBehaviour
{
    public GameObject calibratorButton;
    public GameObject calibrator;

    public Text sheetMusicText;

    public GameObject introScreen;

    public GameObject mainMenu;
    public GameObject primaryMenu;
    public GameObject songChoiceMenu;
    public GameObject optionsMenu;

    private List<int> currentMenuChoices = new List<int>();

    private List<GameObject> Menus;

    // Use this for initialization
    void Start()
    {
        Menus = new List<GameObject>() { primaryMenu, songChoiceMenu, optionsMenu };

        currentMenuChoices.Add(1); //autoplay is on
        currentMenuChoices.Add(1); //calibrate is on

        activateMenu(Menus);

    }

    #region drop down activation functions

    //you have to set functions for each drop down item b/c we can't pass in a reference to the object through the SendMessage function that ZapperAction uses (too complicated)

    void loadSongChoiceMenu()
    {
        openMenu(songChoiceMenu);
        closeMenu(primaryMenu);

        resetActivationFor(GameObject.Find("menuItems_BackToMainMenuFromSongChoiceMenu"));
    }

    void closeSongChoiceMenu()
    {
        closeMenu(songChoiceMenu);
        openMenu(primaryMenu);

        resetActivationFor(GameObject.Find("menuItems_SongChoice"));
        resetActivationFor(GameObject.Find("menuItems_Options"));
    }

    void loadOptionsMenu()
    {
        openMenu(optionsMenu);
        closeMenu(primaryMenu);

        resetActivationFor(GameObject.Find("menuItems_OptionsMenuBackToMainBtn"));

    }

    void closeOptionsMenu()
    {
        closeMenu(optionsMenu);
        openMenu(primaryMenu);

        resetActivationFor(GameObject.Find("menuItems_Options"));
    }

    void closeAllMenus()
    {
        for (int i = 0; i < Menus.Count; i++)
        {
            closeMenu(Menus[i]);
        }

    }

    #endregion


    #region Button Activation Functions

    void setSong1()
    {
        sheetMusicText.text = "Ode to Joy";
        Song.SetCurrentSong(InitializeSong.OdeToJoy);
        closeSongChoiceMenu();
        mainMenu.SetActive(false);
    }
    void setSong2()
    {
        sheetMusicText.text = "Clair de Lune";
        Song.SetCurrentSong(InitializeSong.song);
        closeSongChoiceMenu();
        mainMenu.SetActive(false);
    }

    //same with toggles :)
    void turnAutoplayOff() { print("Turning Autoplay Off"); currentMenuChoices[0] = 0; }
    void turnAutoplayOn() { print("Turning Autoplay On"); currentMenuChoices[0] = 1; }

    void turnCalibrationOff() { calibrator.SetActive(false); calibratorButton.SetActive(false); currentMenuChoices[1] = 0; }
    void turnCalibrationOn() { calibrator.SetActive(true); calibratorButton.SetActive(true); currentMenuChoices[1] = 1; }

    // and this just makes something visible or not
    void showAboutScreen()
    {
        introScreen.SetActive(true);
        mainMenu.SetActive(false);
    }

    void showMainMenu()
    {
        mainMenu.SetActive(true);
    }

    #endregion


    #region Activation Functions
    void activateMenu(List<GameObject> Menus) //this goes and gets all the children of sub menus and hides them
    {

        foreach (GameObject menu in Menus)
        {
            foreach (Transform menuItem in menu.transform)
            {

                if (menuItem.transform.parent.name == Menus[0].gameObject.name)
                {
                    //if inactive set to active
                    if (!menuItem.transform.gameObject.activeInHierarchy)
                    {
                        menuItem.transform.gameObject.SetActive(true);
                    }
                }
                else
                {
                    //set inactive if active for others but primary
                    if (menuItem.transform.gameObject.activeInHierarchy)
                    {
                        menuItem.transform.gameObject.SetActive(false);
                    }
                }

            }
        }
    }

    void openMenu(GameObject menu)
    {


        //find out what menu is open, turn it off, but save for turning on later.

        foreach (Transform menuItem in menu.transform)
        {
            //if inactive set to active
            if (!menuItem.transform.gameObject.activeInHierarchy)
            {
                menuItem.transform.gameObject.SetActive(true);
            }
        }

    }

    void closeMenu(GameObject menu)
    {
        foreach (Transform menuItem in menu.transform)
        {
            //if inactive set to active
            if (menuItem.transform.gameObject.activeInHierarchy)
            {
                menuItem.transform.gameObject.SetActive(false);
            }
        }
    }

    void resetActivationFor(GameObject go)
    {
        //print("RESET THIS GAME OBJECT: " + go);
        if (go == null)
        {
            Debug.LogWarning("Try checking your reference that you're passing into this function 'resetActivationFor'. Needs to find a specific game object.");
            return;
        }
        else
        {
            if (go.GetComponent<BNG_ZapperAction>() != null)
            {
                BNG_ZapperAction za = go.GetComponent<BNG_ZapperAction>();
                za.isActivated = false;
                za.canActivate = true;

                if (go.GetComponent<SpriteRenderer>() != null)
                {
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    sr.color = Color.white;
                }
            }

        }

    }

    void setActivationFor(GameObject go)
    {
        //print("RESET THIS GAME OBJECT: " + go);
        if (go == null)
        {
            Debug.LogWarning("Try checking your reference that you're passing into this function 'resetActivationFor'. Needs to find a specific game object.");
            return;
        }
        else
        {
            if (go.GetComponent<BNG_ZapperAction>() != null)
            {
                BNG_ZapperAction za = go.GetComponent<BNG_ZapperAction>();

                if (go.GetComponent<SpriteRenderer>() != null)
                {
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    sr.color = za.newColorOnHover;
                }

            }

        }

    }

    #endregion
}
