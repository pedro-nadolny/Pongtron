using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "Ball":
                StartCoroutine(BallHit(other.gameObject)); 
                break;
        }    
    }

    private IEnumerator BallHit(GameObject ball) {
        print("Ball Hit!");
        yield return new WaitForSeconds(.7f);
        ball.GetComponent<BallScripts>().Explode();
        // yield return new WaitForSeconds(0.3f);
        GameObject.FindWithTag("BallSpawner").GetComponent<BallSpawner>().SpawnBall();
    }
}
