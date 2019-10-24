using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour {
    private float GOCountDown = 0;
    private bool overScreen = false;

    public GameObject sparkSystem;
    public GameObject goScreen;
    public AudioSource explosionNoise;
    public bool gameOver = false;

    // Update is called once per frame
    void Update() {
        RaycastHit[] hits = this.transform.GetComponent<carRayScript>().hits;
        int numOfCasts = this.transform.GetComponent<carRayScript>().numOfCasts;

        if(!gameOver) {
            for(int i = 0; i < numOfCasts; i++) {
                if(hits[i].distance < 0.1f && hits[i].distance > 0.0f) {
                    //Debug.Log("Crash");
                    this.transform.GetComponent<carMovement>().lockMove = true;
                    explosionNoise.Play();
                    gameOver = true;
                } 
            }
        }
        else {
            sparkSystem.GetComponent<ParticleSystem>().Play();
            if(GOCountDown < 1.0f) {
                GOCountDown += Time.deltaTime;
            }
            else if(!overScreen) {
                //Debug.Log("test");
                goScreen.SetActive(true);
                overScreen = true;
            }
        }
    }
}
