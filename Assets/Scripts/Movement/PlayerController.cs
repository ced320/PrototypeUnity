using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public bool gameOver = false;
    public float jumpForce;
    private float upperYBound = 15;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!OutOfBounds()) 
        {
            PlayerMovement();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            //Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            gameOver = true;
            Debug.Log("Collided with: " + collision.gameObject.name);
            //enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= upperYBound)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool OutOfBounds() {
        if(transform.position.y > upperYBound)
        {
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
            gameObject.transform.position = new Vector3(transform.position.x, upperYBound, transform.position.z);
            return true;
        } 
        return false;
    }
}
