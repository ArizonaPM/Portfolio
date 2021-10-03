using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private bool isGrounded;

    public int score;

    public Rigidbody rb;

    public UIController ui;

    // Update is called once per frame
    void Update()
    {
        //control horizontal and vertical movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //set velocity on given inputs
        rb.velocity = new Vector3(x * moveSpeed, rb.velocity.y, z * moveSpeed);
        Vector3 vel = rb.velocity;
        vel.y = 0;


        //sets the player to face the direction they are heading
        if (rb.velocity.x != 0 ||  rb.velocity.z != 0)
            transform.forward = vel; 

        //gets players jump.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //calls Game Over if player falls off the edge
        if (transform.position.y < -10)
            GameOver();
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {  
          isGrounded = false;
    }



    //resets the scene if a player gets hit or falls of the edge.
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int amount)
    {
        score += amount;
        ui.SetScoreText(score);
    }
}
