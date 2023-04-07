using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
    public float Speed;
    public float Turnspeed;
    private Rigidbody rb;
    public float Grevitymul;

    bool isGrounded = true;
    //bool boostready = true;


    public float booststart = 0f;
    public float boostCooldown = 2f; // 2 = two seconds 



    // start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // update is called once per frame
    void FixedUpdate()
    {
        Turn();
        Move();
        Fall();
    }


    private void OnCollisionExit(Collision collision)
    {
        rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * Speed * 20);  // Speed  * 200 war eigentlich ganz witzig 
        new WaitForSeconds(0.3F);   // WIP
        isGrounded = false;        // WIP
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        isGrounded = true;        // WIP
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded)  // WIP
        {
           
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * Speed * 10);
        }
        else if (Input.GetKey(KeyCode.S) && isGrounded)  // WIP
        {
            
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -Speed * 10);
        }
       
        if (Input.GetKey(KeyCode.Q) && Time.time > booststart + boostCooldown)
        {
            booststart = Time.time;
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * Speed * 200);
         
        }

        Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
        locVel = new Vector3(0, locVel.y, locVel.z);
        rb.velocity = new Vector3(transform.TransformDirection(locVel).x, rb.velocity.y, transform.TransformDirection(locVel).z);
    }

    void Turn()
    {
        if (Input.GetKey(KeyCode.D) && isGrounded)   // WIP
        {
            rb.AddTorque(Vector3.up * Turnspeed * 10);
        }
        else if (Input.GetKey(KeyCode.A) && isGrounded)   // WIP
        {
            rb.AddTorque(-Vector3.up * Turnspeed * 10);
        }
    }

    void Fall()
    {
        rb.AddForce(Vector3.down * Grevitymul * 10);
    }
}