using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScripts : MonoBehaviour
{
    public float perpendicularSpeed;
    public float sidewaySpeed;
    public GameObject explosion;

    private GameManagerScript gameManager;

    // Start is called before the first frame update
    private void Start() {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
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
        Destroy(gameObject);
        gameManager.Damage();
    }

    public IEnumerator Explode() {
        yield return new WaitForSeconds(.7f);
        Vector3 currentPosition = GetComponent<Transform>().position;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(Instantiate(explosion, currentPosition, Quaternion.identity), 1.5f);
        Destroy(gameObject);
    } 
}
