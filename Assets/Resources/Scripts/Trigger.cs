using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    GameObject go;
    int iRandomNumber;
    float fRandomOffset;
    [SerializeField]
    private Transform Spawnner;
    private Vector3 vRandomLocation, vRandomOffset;

    private void Start()
    {
        iRandomNumber = UnityEngine.Random.Range(2, 10);
        go = Resources.Load<GameObject>("Prefabs/NPC");

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            for (int i = 0; i < iRandomNumber; i++)
            {
                vRandomLocation = new Vector3(UnityEngine.Random.Range(-6, 6), 0, 0);
                GameObject.Instantiate(go, Spawnner.position + vRandomLocation, Quaternion.identity, Spawnner);
                // float fRandomScale = Random.Range(0.5f, 1f);
                // go.transform.localScale = new Vector3(go.transform.localScale.x * fRandomScale, go.transform.localScale.y * fRandomScale, go.transform.localScale.z * fRandomScale);
            }
        }
    }



}
