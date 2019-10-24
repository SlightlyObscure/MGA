using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public void startGame() {
        SceneManager.LoadScene("GameScene");
    }

    public void backToMain() {
        SceneManager.LoadScene("MainMenu");
    }

    public void quit() {
        //Debug.Log("exit clicked");
        Application.Quit();
    }
}


