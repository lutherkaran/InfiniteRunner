using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool bEnemyDied = false;
    bool bEnemyFight = false;
    [SerializeField]
    Animator anim;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("NPC"))
        {
            bEnemyFight = true;
            if (bEnemyFight)
                anim.Play("EnemyFight");
            bEnemyDied = true;
            anim.SetBool("bDied", bEnemyDied);
            // StartCoroutine(Died());
        }
    }
    IEnumerator Died()
    {
        yield return new WaitForSeconds(5f);
        bEnemyDied = true;
        anim.SetBool("bDied", bEnemyDied);
    }

}
