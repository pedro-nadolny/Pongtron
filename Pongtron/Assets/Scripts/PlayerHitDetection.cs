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
                BallHit(other.gameObject, other.contacts[0]); 
                break;
        }    
    }

    private void BallHit(GameObject ball, ContactPoint contactPoint) {
        if (ball == null) return; 
        BallScripts ballScripts = ball.GetComponent<BallScripts>();
        if (ballScripts.Equals(null) || ballScripts.willExplode) return; 
        gameManager.Score();
        ballScripts.ExplodeWithDelay(true);
        ballScripts.ReflectBallWithNormal(Vector3.right);
        ballScripts.DisableCollisionDetectionFor(.25f);
    }
}
