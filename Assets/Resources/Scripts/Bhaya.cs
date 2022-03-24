using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bhaya : MonoBehaviour
{
    [SerializeField]
    float fSpeed = 1f;
    [SerializeField]
    Rigidbody rb;
    bool bBhayaDied = false;
    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bBhayaDied)
        {
            Movement();

        }

    }

    private void Movement()
    {
        anim.SetFloat("fSpeed", fSpeed, 0.1f, Time.deltaTime);
        this.transform.Translate(Vector3.forward * fSpeed * Time.deltaTime);
    }

}
