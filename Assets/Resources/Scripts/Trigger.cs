using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    GameObject go;
    int fRandom;
    [SerializeField]
    private Transform Spawnner;
    private Vector3 vRandomOffset;

    private void Start()
    {
        fRandom = UnityEngine.Random.Range(5, 5);

        go = Resources.Load<GameObject>("Prefabs/NPC");
    }
    private void OnTriggerEnter(Collider other)
    {
        vRandomOffset = new Vector3(UnityEngine.Random.Range(-7, 7), 0, 0);
        if (other.gameObject.tag == "Player")
        {

            for (int i = 0; i < fRandom; i++)
            {

                GameObject.Instantiate(go, Spawnner.position + vRandomOffset, Quaternion.identity, Spawnner);
            }
        }
    }

}
