using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    public float perpendicularSpeed;
    public float sidewaySpeed;
    public GameObject explosion;
    public GameObject ballIndicator; 
    public bool willExplode = false;
    private GameManagerScript gameManager;
    private Rigidbody body;
    private Vector3 oldVelocity;
    private GameObject indicatorInstance;

    // Start is called before the first frame update
    private void Start() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        Vector2 randomSidewaySpeed = Random.insideUnitCircle * sidewaySpeed;
        body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(-perpendicularSpeed, randomSidewaySpeed.x, randomSidewaySpeed.y);
        Vector3 indicatorPosition = new Vector3(body.position.x, 0, 0);
        indicatorInstance = Instantiate(ballIndicator, indicatorPosition, Quaternion.identity);
    }

    private void FixedUpdate() {
        oldVelocity = body.velocity;
        indicatorInstance.GetComponent<Rigidbody>().velocity = new Vector3(-perpendicularSpeed, 0, 0);
    }

    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag) {
            case "PlayerGoal":
                SufferedGoal();
                break;
        }     
    }

    private void SufferedGoal() {
        if(gameObject) Destroy(gameObject);
        if (indicatorInstance) Destroy(indicatorInstance);
        gameManager.Damage();
    }

    public void DisableCollisionDetectionFor(float seconds) {
        EnumaratorDisableCollisionDetectionFor(seconds);
    }

    private IEnumerator EnumaratorDisableCollisionDetectionFor(float seconds) {
        GetComponent<Rigidbody>().detectCollisions = false;
        yield return new WaitForSeconds(seconds); 
        GetComponent<Rigidbody>().detectCollisions = true;
    }

    public void ExplodeWithDelay(bool delay) {
        if (willExplode) return; 
        willExplode = true;
        StartCoroutine(EnumeratorExplodeWithDelay(delay));
    }

    private IEnumerator EnumeratorExplodeWithDelay(bool delay) {
        if (delay) yield return new WaitForSeconds(.7f); 
        Vector3 currentPosition = GetComponent<Transform>().position;
        GetComponent<MeshRenderer>().enabled = false;
        Instantiate(explosion, currentPosition, Quaternion.identity);
        if (gameObject) Destroy(gameObject);
        if (indicatorInstance) Destroy(indicatorInstance);
    } 

    public void ReflectBallWithNormal(Vector3 normal) {
        body.velocity = Vector3.Reflect(oldVelocity, normal);
    }
}