using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManagerScript gameManager;

    private void Start() {
        gameManager = gameManagerObject.GetComponent<GameManagerScript>();
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Ball":
                BallHit(other.gameObject); 
                break;
        }    
    }

    private void BallHit(GameObject ball) {
        gameManager.Score();
        StartCoroutine(ball.GetComponent<BallScripts>().Explode());
    }
}
