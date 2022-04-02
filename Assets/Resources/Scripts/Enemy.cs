using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit, IGetClosestTarget
{
    bool bEnemyDied = false;
    bool bEnemyFight = false;
    [SerializeField]
    Animator anim;
    GameObject[] go;
    List<Bhaya> npcList;
    List<Transform> transforms;
    public void Start()
    {
        npcList = new List<Bhaya>();
        transforms = new List<Transform>();
    }


    public void Update()
    {

        npcList.AddRange(FindObjectsOfType<Bhaya>());
        if (npcList != null)
        {
            foreach (Bhaya bhaya in npcList)
            {
                Transform target = CloseTarget(bhaya.gameObject.transform);
                this.transform.LookAt(target);
            }
            npcList.Clear();
            transforms.Clear();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("NPC"))
        {
            bEnemyFight = true;
            if (bEnemyFight)
                anim.Play("EnemyFight");
            Death();
            // StartCoroutine(Died());
        }
    }

    public Transform CloseTarget(Transform enemy)
    {
        transforms.Add(enemy);

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform trans in enemy)
        {
            float dist = Vector3.Distance(trans.position, currentPos);
            if (dist < minDist)
            {
                tMin = trans;
                minDist = dist;
            }
        }
        return tMin;
    }

    public override void Death()
    {
        bEnemyDied = true;
        anim.SetBool("bDied", bEnemyDied);
        GameObject.Destroy(this.gameObject, 2);
    }
}
