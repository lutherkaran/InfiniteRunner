using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float fSpeed = 1f;
    [SerializeField]
    Rigidbody rb;
    bool bJump = false;
    [SerializeField]
    bool bCanJump = false;
    [SerializeField]
    float fForce;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * fSpeed * Time.deltaTime);
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && !bJump)
        {
            bJump = true;
        }

    }

    private void Movement()
    {
        float fHorizontal = Input.GetAxis("Horizontal");
        move = new Vector3(fHorizontal * fSpeed * Time.deltaTime, 0f, 0f);
        transform.position += move;

    }
    private void FixedUpdate()
    {
        if (bJump)
        {
            Jump();
        }
    }
    private void Jump()
    {
        if (bCanJump)
            rb.AddForce(Vector3.up * fForce * Time.fixedDeltaTime, ForceMode.Impulse);
        bJump = false;
        bCanJump = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            bCanJump = true;
        }
        else
        {
            bCanJump = false;
        }
    }
}
