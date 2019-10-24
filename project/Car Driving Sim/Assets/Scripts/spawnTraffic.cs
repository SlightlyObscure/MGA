using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTraffic : MonoBehaviour {
    private float laneWidth = 3.5f;
    private Vector3 pos;
    private float spawnOffset = 8.0f;

    private float timePassed = 0.0f;
    private float timeToWorsen = 0.0f;
    private float spawnIntrvl = 10.0f;
    private float spawnIn = 0.0f;
    private float spawnDelay = 10.0f;

    public GameObject regCar;
    public GameObject tallCar;
    public GameObject street;

    // Start is called before the first frame update
    void Start() {
        pos = transform.position;

        //spawnCar(1, 2, 8.0f);

        for(int initSpawns = -3; initSpawns < 9; initSpawns++) {
            if(initSpawns != 0) {
                maybeSpawn(1, initSpawns * spawnOffset);
                maybeSpawn(2, initSpawns * spawnOffset);
                if(initSpawns > 2) {
                    maybeSpawn(3, initSpawns * spawnOffset);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        timePassed += Time.deltaTime;
        timeToWorsen += Time.deltaTime;
        spawnIn -= Time.deltaTime;

        

        if(timePassed > spawnDelay && spawnIn < 0) {
            spawnCar(randomTypeGen(), Random.Range(1, 4), 100.0f);
            spawnIn = spawnIntrvl;
        }

        if(timeToWorsen > 10.0f) {
            spawnIntrvl *= 0.95f;
            timeToWorsen = 0.0f;
        }
    }

    void spawnCar(int type, int lane, float zPos) {
        GameObject car = null;
        float baseSpeed = 0.0f;

        if(type == 1) {
            car = Instantiate(regCar, new Vector3((lane-2) * laneWidth, 1.0f, zPos), Quaternion.identity);
            baseSpeed = Random.Range(10.0f, 14.0f);
        }
        else if(type == 2) {
            car = Instantiate(tallCar, new Vector3((lane-2) * laneWidth, 1.0f, zPos), Quaternion.identity);
            baseSpeed = Random.Range(7.0f, 11.0f);
        }
        car.transform.GetComponent<carVals>().curLane = 1;
        car.transform.GetComponent<carVals>().destLane = 1;
        car.transform.GetComponent<trafficCarScript>().street = this.street;
        car.transform.GetComponent<trafficCarScript>().baseSpeed = baseSpeed;
    }

    int randomTypeGen() {
        int rand = Random.Range(1, 9);
        if(rand == 1) {
            return 2;
        }
        else {
            return 1;
        }
    }

    void maybeSpawn(int lane, float zPos) {
        if(Random.Range(0, 4) == 0) {
            spawnCar(randomTypeGen(), lane, zPos);
        }
    }

    
}
