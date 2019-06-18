using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScripts : MonoBehaviour
{
    public float perpendicularSpeed;
    public float sidewaySpeed;
    public GameObject explosion; 
    private GameManagerScript gameManager;
    private bool willExplode = false;
    private Rigidbody body;
    private Vector3 oldVelocity;

    // Start is called before the first frame update
    private void Start() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        Vector2 randomSidewaySpeed = Random.insideUnitCircle * sidewaySpeed;
        body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(-perpendicularSpeed, randomSidewaySpeed.x, randomSidewaySpeed.y);
    }

    private void FixedUpdate() {
        oldVelocity = body.velocity;
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "PlayerGoal":
                SufferedGoal();
                break;
        }     
    }

    private void SufferedGoal() {
        Destroy(gameObject);
        gameManager.Damage();
    }

    public void ExplodeWithDelay(bool delay) {
        if (willExplode) return; 
        willExplode = true;
        StartCoroutine(ExplodeWithDelayEnumerator(delay));
    }

    private IEnumerator ExplodeWithDelayEnumerator(bool delay) {
        if (delay) yield return new WaitForSeconds(.7f); 
        Vector3 currentPosition = GetComponent<Transform>().position;
        GetComponent<MeshRenderer>().enabled = false;
        Instantiate(explosion, currentPosition, Quaternion.identity);
        if (gameObject != null) { 
            Destroy(gameObject);
        }
    } 

    public void ReflectBallWithNormal(Vector3 normal) {
        body.velocity = Vector3.Reflect(oldVelocity, normal);
    }
}
