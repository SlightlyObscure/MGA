using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetScroll : MonoBehaviour {
    public float speed = 1.0f;

    private float offset = 0.0f;
    private float speedUpMod = 0.5f;
    private float speedMax = 15.0f;
    private float startPos;
    private float objLen = 60.0f;

    // Start is called before the first frame update
    void Start() {
        startPos = transform.position.z;
    }

    // Update is called once per frame
    void Update() {
        offset += speed * Time.deltaTime;

        if(speed < speedMax) {
            speed *= 1 + (speedUpMod * Time.deltaTime);
        }

        if(offset < objLen) {
            transform.position -= new Vector3(0.0f, 0.0f, (speed * Time.deltaTime));
            
        }
        else {
            transform.position += new Vector3(0.0f, 0.0f, objLen);
            offset = 0;
        }
    }
}
