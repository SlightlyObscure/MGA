using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carRayScript : MonoBehaviour {
    private float rayLen = 10.0f;
    private int layerMask = 1 << 8;
    

    private Vector3 pos;
    private Vector3 forwards = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 left = new Vector3(-1.0f, 0.0f, 0.0f);
    private Vector3 backwards = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 fr, br, fl, bl;
    private Vector3 front = new Vector3(0.0f, 0.0f, 1.5f);
    private Vector3 back = new Vector3(0.0f, 0.0f, -1.5f);
    private Vector3 rSide = new Vector3(1.0f, 0.0f, 0f);
    private Vector3 lSide = new Vector3(-1.0f, 0.0f, 0f);

    public RaycastHit[] hits = new RaycastHit[9];
    public int numOfCasts = 9;

    // Start is called before the first frame update
    void Start() {
        fr = forwards + right;
        br = backwards + right;
        fl = forwards + left;
        bl = backwards + left;
    }

    // Update is called once per frame
    void Update() {
        pos = transform.position;

        for(int i = 0; i < numOfCasts; i++) {
            hits[i].distance = 11.0f;
        }

        hits[0] = castRay(pos + front + lSide, forwards);
        hits[1] = castRay(pos + front + rSide, forwards);
        hits[2] = castRay(pos + back, backwards);
        hits[3] = castRay(pos + rSide, right);
        hits[4] = castRay(pos + lSide, left);
        hits[5] = castRay(pos + front + rSide, fr);
        hits[6] = castRay(pos + back + rSide, br);
        hits[7] = castRay(pos + front + lSide, fl);
        hits[8] = castRay(pos + back + lSide, bl);
        
        

    }

    RaycastHit castRay(Vector3 rayPos, Vector3 rayDir) {
        RaycastHit hit;

        if(Physics.Raycast(rayPos, rayDir, out hit, rayLen, layerMask)) {
            //Debug.Log("Did Hit");
            //Debug.Log(hit.distance);
        }
        Debug.DrawRay(rayPos, rayDir * rayLen, Color.green);

        return hit;
    }
}
