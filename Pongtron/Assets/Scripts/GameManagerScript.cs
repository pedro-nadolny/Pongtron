using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    private int lifes = 3;
    private int score = 0;
    public GameObject spawnerGameObject;
    public Text scoreText; 
    public RawImage heart1;
    public RawImage heart2;
    public RawImage heart3; 
    public Button startButton;

    public Text startButtonText;

    private BallSpawner spawner;
    
    void Start() {
        spawner = spawnerGameObject.GetComponent<BallSpawner>();
    }

    public void StartGame() {
         lifes = 3;
         score = 0;
         scoreText.text = score.ToString();
         startButton.gameObject.SetActive(false);
         heart3.gameObject.SetActive(true);
         heart2.gameObject.SetActive(true);
         heart1.gameObject.SetActive(true);
         spawner.StartSpawning();
    }

    public void Score() {
        score++;
        scoreText.text = score.ToString();
    }

    public void Damage() {
        lifes--;

        switch(lifes) {
            case 0:
                heart3.gameObject.SetActive(false);
                EndGame();
                break;
            case 1:
                heart2.gameObject.SetActive(false);
                break;
            case 2:
                heart1.gameObject.SetActive(false);
                break;
        }
    }

    private void EndGame() {
        spawner.StopSpawning();
        startButton.gameObject.SetActive(true);
        startButton.OnDeselect(null);

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls) {
            if (ball == null) { continue; }    
            BallScripts ballScript = ball.GetComponent<BallScripts>();
            if (ballScript.Equals(null)) return;
            ballScript.ExplodeWithDelay(false);
        }
    }
}
