using UnityEngine;
using System.Collections;

public class GazeUI_HideShowWindow : MonoBehaviour
{

    public GameObject windowToShowOrHide;

    private bool isOpen;


    // Use this for initialization
    void Start()
    {

        if (windowToShowOrHide.activeInHierarchy)
        {
            isOpen = true;
        }

    }

    public void doWindow()
    {

        if (isOpen) closeWindow();
        else openWindow();

    }

    void closeWindow()
    {
        windowToShowOrHide.SetActive(false);
    }

    void openWindow()
    {
        windowToShowOrHide.SetActive(true);
    }
}
