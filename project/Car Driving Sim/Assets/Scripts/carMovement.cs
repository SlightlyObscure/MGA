using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class carMovement : MonoBehaviour {
    private Vector3 laneWidth = new Vector3(3.5f, 0.0f, 0.0f);
    private float switchProg = 0.0f;
    private float startSpeed = 0.3f;
    private float speed = 0.3f;
    private float speedUpMod = 0.9f;
    private float speedMax = 1.0f;
    private float speedMin = 0.3f;
    private float slowDownMod = 5.0f;

    public bool lockMove = false;

    // Update is called once per frame
    void Update() {
        if(!lockMove) {
            carVals vals = this.transform.GetComponent<carVals>();

            if(!vals.isSwitching) {
                if (vals.curLane != vals.destLane) {
                    //Debug.Log("change lane");

                    if(vals.curLane < vals.destLane) {
                        this.transform.GetComponent<carVals>().switchDir = 1;
                    }
                    else {
                        this.transform.GetComponent<carVals>().switchDir = -1;
                        
                    }
                    this.transform.GetComponent<carVals>().isSwitching = true;
                }
            }
            else {
                if (vals.switchIsCancel) {
                    //Debug.Log("calcel switch");
                    this.transform.GetComponent<carVals>().curLane += vals.switchDir;
                    this.transform.GetComponent<carVals>().switchDir *= -1;
                    switchProg = laneWidth.x - switchProg;
                    speed = startSpeed;

                    this.transform.GetComponent<carVals>().switchIsCancel = false;
                }

                if (switchProg <= laneWidth.x) {
                    if((switchProg / laneWidth.x) < 0.8) {
                        if(speed < speedMax) {
                            speed *= 1 + (speedUpMod * Time.deltaTime);
                        }
                    }
                    else {
                        if(speed > speedMin && (switchProg / laneWidth.x) < 0.95) {
                            speed *= 1 - (speedUpMod * slowDownMod * Time.deltaTime);
                        }
                        else if(speed > 0.1f && (switchProg / laneWidth.x) > 0.95) {
                            speed *= 1 - (speedUpMod * slowDownMod * Time.deltaTime);
                        }
                    }
                    Vector3 moveDist = laneWidth * vals.switchDir * speed * Time.deltaTime;
                    transform.position += moveDist;
                    switchProg += Math.Abs(moveDist.x);
                    //Debug.Log(switchProg);
                }
                else {
                    transform.position += new Vector3((switchProg - laneWidth.x), 0.0f, 0.0f) * -vals.switchDir;
                    switchProg = 0;
                    speed = startSpeed;
                    this.transform.GetComponent<carVals>().isSwitching = false;
                    this.transform.GetComponent<carVals>().curLane = vals.destLane;

                    //Debug.Log("prog: " + switchProg);
                    //Debug.Log("diff: " + (switchProg - laneWidth.x));
                    //Debug.Log("pos: " + transform.position.x);
                }
            }
        }
    }
}
