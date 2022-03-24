using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bhaya : MonoBehaviour
{
    [SerializeField]
    float fSpeed = 1f;
    [SerializeField]
    Rigidbody rb;
    bool bBhayaDied = false;
    bool bBhayaFight = false;
    [SerializeField]
    Animator anim;
    string[] tags = { "Object", "Enemy" };
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
    private void OnCollisionEnter(Collision collision)
    {

        if (tags.Contains(collision.gameObject.tag))
        {
            if (collision.gameObject.tag == "Enemy")
            {
                bBhayaFight = true;
                if (bBhayaFight)
                {
                    anim.Play("BhayaFight");
                    fSpeed = 0;

                }

                bBhayaDied = true;
                anim.SetBool("bDied", bBhayaDied);
                //StartCoroutine(Died());
            }
            else if (collision.gameObject.tag == "Object")
            {

                bBhayaDied = true;
                fSpeed = 0;
                anim.SetBool("bDied", bBhayaDied);
            }

        }
    }
    IEnumerator Died()
    {
        yield return new WaitForSeconds(5f);
        bBhayaDied = true;
        anim.SetBool("bDied", bBhayaDied);
    }

}
