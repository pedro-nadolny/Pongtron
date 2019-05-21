using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    private Vector3 spawnPosition;

    private GameObject ball;

    void Start() {
        ball = GameObject.FindWithTag("Ball");
        spawnPosition = ball.GetComponent<Rigidbody>().position;
        
    }

    private void RepeatSpawn() {

    }
    
    public void SpawnBall() {
        GameObject newBall = Instantiate(ball, spawnPosition, Quaternion.identity);
        Destroy(ball);
        ball = newBall;
        ball.name = "Ball";
        ball.tag = "Ball";
        ball.GetComponent<MeshRenderer>().enabled = true;
    }
}
