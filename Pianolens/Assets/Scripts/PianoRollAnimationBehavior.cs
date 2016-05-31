using UnityEngine;
using System.Collections;

public class PianoRollAnimationBehavior : MonoBehaviour {

    int measureToEndOn;
    float currentMeasure;
    void setup(int measureToEndOn, float currentMeasure)
    {

    }

	// Use this for initialization
	void Start () {
        LeanTween.move(gameObject, getTarget(), timeLeft);
	}

}
