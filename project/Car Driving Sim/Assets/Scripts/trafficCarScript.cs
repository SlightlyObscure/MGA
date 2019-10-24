using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public class trafficCarScript : MonoBehaviour {
    public GameObject street;
    public RaycastHit[] colRays;
    public float baseSpeed;

    private float curSpeed;

    private float brkSpeed = 1f;
    private float brkStart = 1f;
    private float brkMax = 10f;
    private float brkSpeedUpMod = 2.0f;
    private float brkSlowDownMod = 1.0f;
    
    private float slowThresh = 10.0f;

    // Start is called before the first frame update
    void Start() {
        colRays = transform.GetComponent<carRayScript>().hits;
        curSpeed = baseSpeed;
    }

    void brakedSpeed(bool braking, float urgency) {
        if(braking) {
            if(brkSpeed < brkMax) {
                brkSpeed *= 1 + (brkSpeedUpMod * urgency * Time.deltaTime);
            }
        }
        else if(brkSpeed > brkStart) {
            brkSpeed *= 1 - (brkSpeedUpMod * brkSlowDownMod * Time.deltaTime);
        }
        
    }

    // Update is called once per frame
    void Update() {
        float slowness = 1.0f;

        if(colRays[0].distance < slowThresh && colRays[0].distance > 0) {
            slowness = colRays[0].distance / slowThresh;
        }
        if(colRays[1].distance < slowThresh && colRays[1].distance > 0) {
            float temp = colRays[1].distance / slowThresh;

            if(temp < slowness) {
                slowness = temp;
            }
        }

        float zMoveDist = 0.0f;

        if(slowness < curSpeed / baseSpeed) {
            if(!(colRays[2].distance < 5.0f && colRays[2].distance > 0)) {
                //if(string.Compare(colRays[2].transform.name, "Player Car") == 0 ) {
                    brakedSpeed(true, (1.0f - slowness));
                    zMoveDist = ( (curSpeed - brkSpeed) - street.transform.GetComponent<streetScroll>().speed) * Time.deltaTime;
                    //Debug.Log(colRays[2].transform.gameObject.name);
                //}
            }
        }
        else if(brkSpeed > brkStart){
            brakedSpeed(false, (1.0f - slowness));
            zMoveDist = ( (curSpeed - brkSpeed) - street.transform.GetComponent<streetScroll>().speed) * Time.deltaTime;
        }
        else {
            zMoveDist = ( curSpeed - street.transform.GetComponent<streetScroll>().speed) * Time.deltaTime;
        }

        transform.position += new Vector3(0.0f, 0.0f, zMoveDist);

        if(transform.position.z < -60.0f) {
            Destroy(this.gameObject);
        }
    }
}
