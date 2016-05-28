using UnityEngine;
using System.Collections;

public class CalibratorCommand : MonoBehaviour {

    public static bool isCalibrationMode = false;
    public static float timeElapsedCalibration = 0;
    public GameObject calibrator;
    // Use this for initialization
    void Start()
    {
        isCalibrationMode = false;
        timeElapsedCalibration = 0;
        calibrator = GameObject.Find("Calibrator");
        calibrator.SetActive(isCalibrationMode);
    }

    void Update()
    {
        if (isCalibrationMode)
        {
            timeElapsedCalibration += Time.deltaTime;
            if(timeElapsedCalibration >= 5.0)
            {
                isCalibrationMode = false;
                calibrator.SetActive(isCalibrationMode);
                timeElapsedCalibration = 0;
            }
        }
    }
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelectUp()
    {
        isCalibrationMode = true;
        calibrator.SetActive(isCalibrationMode);
    }

    static public void resetTimer()
    {
        timeElapsedCalibration = 0;
    }
    
}
