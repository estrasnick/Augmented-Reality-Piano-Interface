using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MetronomeText : MonoBehaviour {

    int BPM;
    Text text;

	// Use this for initialization
	void Start () {
        BPM = 100;
        text = this.GetComponent<Text>();
        Update();
	}
	
	// Update is called once per frame
    void Update()
    {
        text.text = BPM + "\nBPM";
    }

	void IncrementBPM () {
        BPM++;
	}

    void DecrementBPM()
    {
        if (BPM > 0)
        {
            BPM--;
        }
    }
}
