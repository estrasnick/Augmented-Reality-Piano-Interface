using UnityEngine;
using System.Collections;

public class CalibrationCommands : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    public static readonly float multiplier = 1000;
    private void moveABit(Vector3 coords)
    {
        CalibratorCommand.resetTimer();
        GameObject.Find("MainApp").transform.Translate(coords / multiplier);
    }

    private void rotateABit(Vector3 coords)
    {
        CalibratorCommand.resetTimer();
        GameObject.Find("MainApp").transform.Rotate(coords / multiplier);
    }

    private void resizeABit(int direction)
    {
        CalibratorCommand.resetTimer();
        GameObject.Find("MainApp").transform.localScale += ((float)direction / multiplier) * new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void OnSelectUp(){ moveABit(new Vector3(0, 1, 0));  }
    void OnSelectDown() { moveABit(new Vector3(0, -1, 0)); }
    void OnSelectLeft() { moveABit(new Vector3(-1, 0, 0)); }
    void OnSelectRight() { moveABit(new Vector3(1, 0, 0)); }
    void OnSelectForward() { moveABit(new Vector3(0, 0, 1)); }
    void OnSelectBackward() { moveABit(new Vector3(0, 0, -1)); }
    void OnSelectYawL() { rotateABit(new Vector3(0, 3, 0));  }
    void OnSelectYawR() { rotateABit(new Vector3(0, -3, 0)); }

    void OnSelectZoomIn() { resizeABit(-1); }

    void OnSelectZoomOut() { resizeABit(1); }
}
