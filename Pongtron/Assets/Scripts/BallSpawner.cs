using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {

    private Vector3 spawnPosition;

    public GameObject ball;

    private bool keepSpawning = false;

    void Start() {
        spawnPosition = ball.GetComponent<Transform>().position;
    }

    public IEnumerator StartAsyncSpawning() {
        keepSpawning = true; 
        do {
            float randomTimeInterval = Random.Range(1, 100)/100.0f + 0.666f;
            SpawnBall();
            yield return new WaitForSeconds(randomTimeInterval);
        } while(keepSpawning); 
    }

    public void StartSpawning() {
        StartCoroutine(StartAsyncSpawning());
    }

    public void StopSpawning() {
        keepSpawning = false; 
    }
    
    private void SpawnBall() {
        GameObject newBall = Instantiate(ball, spawnPosition, Quaternion.identity);
        ball.name = "Ball";
        ball.tag = "Ball";
    }
}
