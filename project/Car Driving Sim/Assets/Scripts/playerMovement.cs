using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    private float accSpeed = 0.5f;
    private float accStart = 0.5f;
    private float accMax = 2.0f;
    private float accSpeedUpMod = 0.9f;
    private float accSlowDownMod = 3.0f;

    private float brkSpeed = 1f;
    private float brkStart = 1f;
    private float brkMax = 20f;
    private float brkSpeedUpMod = 2.0f;
    private float brkSlowDownMod = 1.0f;

    private float minZ, maxZ;

    void Start() {
        minZ = transform.position.z;
        maxZ = minZ + 43.0f;
    }

    void zMove(bool input, bool isAcc, float speed, float start, float max, float speedUpMod, float slowDownMod, int direction) {
        if(input) {
            if(speed < max) {
                speed *= 1 + (speedUpMod * Time.deltaTime);
            }
            transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime * direction);
        }
        else {
            if(speed > start) {
                //accSpeed = accStart;
                speed *= 1 - (speedUpMod * slowDownMod * Time.deltaTime);
                transform.position += new Vector3(0.0f, 0.0f, speed * Time.deltaTime * direction);
            }
        }
        if(isAcc) {
            accSpeed = speed;
        }
        else {
            brkSpeed = speed;
        }

        if(transform.position.z < minZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
        else if(transform.position.z > maxZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }
    }

    // Update is called once per frame
    void Update() {
        carVals vals = this.transform.GetComponent<carVals>();

        if (Input.GetKey("a") && (vals.curLane > 1 || vals.isSwitching)) {
            if(!vals.isSwitching) {
                this.transform.GetComponent<carVals>().destLane -= 1;
            }
            else if(vals.destLane > vals.curLane) {
                this.transform.GetComponent<carVals>().destLane -= 1;
                this.transform.GetComponent<carVals>().switchIsCancel = true;
            }
        }
        else if(Input.GetKey("d") && (vals.curLane < 3 || vals.isSwitching)) {
            if(!vals.isSwitching) {
                this.transform.GetComponent<carVals>().destLane += 1;
            }
            else if(vals.destLane < vals.curLane) {
                this.transform.GetComponent<carVals>().destLane += 1;
                this.transform.GetComponent<carVals>().switchIsCancel = true;
            }
        }

        if(!this.transform.GetComponent<carMovement>().lockMove) {
            zMove(Input.GetKey("w"), true, accSpeed, accStart, accMax, accSpeedUpMod, accSlowDownMod, 1);

            zMove(Input.GetKey("s"), false, brkSpeed, brkStart, brkMax, brkSpeedUpMod, brkSlowDownMod, -1);
        }
    }
}
