using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    float fSpeed = 1f;
    [SerializeField]
    Rigidbody rb;
    bool bJump = false;
    [SerializeField]
    bool bCanJump = false;
    bool bGameStart = false;
    bool bPlayerDied = false;
    [SerializeField]
    float fForce;
    Vector3 move;
    [SerializeField]
    Animator anim;
    GameObject go;
    int fRandom;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (!bPlayerDied)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bGameStart = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !bJump)
            {
                bJump = true;
            }
            if (bGameStart)
            {
                Movement();
            }
        }

    }

    private void Movement()
    {

        // this.transform.Translate(Vector3.forward * fSpeed * Time.deltaTime);
        float fHorizontal = Input.GetAxisRaw("Horizontal");
        float fVertical = Input.GetAxisRaw("Vertical");
        if (fVertical >= 0)
        {
            move = new Vector3(fHorizontal * fSpeed * Time.deltaTime, 0f, fVertical * fSpeed * Time.deltaTime);
        }
        /* if (fVertical == 0 && (fHorizontal > 0 || fHorizontal < 0))
         {
             move = new Vector3(fHorizontal * fSpeed * Time.deltaTime, 0f, 0f);
         }*/

        //Debug.Log("Move-Mag:" + move.magnitude);
        anim.SetFloat("fSpeed", move.magnitude * fSpeed, 0.1f, Time.deltaTime);
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
        {

            anim.SetBool("bJump", true);
            rb.AddForce(Vector3.up * fForce * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        bJump = false;
        bCanJump = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            bCanJump = true;
            anim.SetBool("bJump", false);

        }
        if (collision.collider.CompareTag("Object"))
        {

            Death();
        }

    }

    public override void Death()
    {
        bPlayerDied = true;
        if (bPlayerDied)
            anim.Play("Death");
    }
}
