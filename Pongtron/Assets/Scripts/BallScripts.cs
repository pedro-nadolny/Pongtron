using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScripts : MonoBehaviour
{
    public float perpendicularSpeed;
    public float sidewaySpeed;
    public GameObject explosion;

    private Rigidbody body;

    // Start is called before the first frame update
    private void Start() {
        Vector2 randomSidewaySpeed = Random.insideUnitCircle * sidewaySpeed;
        GetComponent<Rigidbody>().velocity = new Vector3(-perpendicularSpeed, randomSidewaySpeed.x, randomSidewaySpeed.y);
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "PlayerGoal":
                SufferedGoal();
                break;
        }     
    }

    private void SufferedGoal() {
        print("Suffered Goal!");
        GameObject.FindWithTag("BallSpawner").GetComponent<BallSpawner>().SpawnBall();
    }

    public void Explode() {
        Vector3 currentPosition = GetComponent<Rigidbody>().position;
        GetComponent<MeshRenderer>().enabled = false;
        GameObject explosionObject = Instantiate(explosion, currentPosition, Quaternion.identity);
        Destroy(explosionObject, 1.5f);
    } 
}
