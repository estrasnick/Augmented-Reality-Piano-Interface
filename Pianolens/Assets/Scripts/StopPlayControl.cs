using UnityEngine;
using System.Collections;

public class StopPlayControl : MonoBehaviour {

    public Sprite playingSprite;
    public Sprite pausedSprite;
    private SpriteRenderer spriteRenderer;
    private BNG_ZapperAction zapper;

	// Use this for initialization
	void Start () {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        zapper = this.GetComponentInChildren<BNG_ZapperAction>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Timing.IsPaused && spriteRenderer.sprite != pausedSprite)
        {
            spriteRenderer.sprite = pausedSprite;
        }
        else if (!Timing.IsPaused && spriteRenderer.sprite != playingSprite)
        {
            spriteRenderer.sprite = playingSprite;
        }
    }
}
