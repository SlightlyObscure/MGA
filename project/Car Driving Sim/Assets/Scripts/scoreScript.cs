using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour {
    private float fScore = 0.0f;

    public int score = 0;
    public GameObject player;
    public Text sText;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(!player.transform.GetComponent<playerCollision>().gameOver) {
            fScore += Time.deltaTime * 3;
            score = (int) fScore;
        }
        sText.text = "Score: " + score;
    }
}
