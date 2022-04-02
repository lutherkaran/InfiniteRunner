using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Trigger : MonoBehaviour
{
    GameObject go;
    int iRandomSign;
    int iRandomNumber;
    float fRandomOffset;
    [SerializeField]
    private Transform Spawnner;
    private Vector3 vRandomLocation, vRandomOffset;
    int totalNumber;
    enum ArithmeticSigns { KILLALL, ADD, SUBTRACT, DIVIDE, MULTIPLY };
    ArithmeticSigns eArithmeticOperations;
    [SerializeField]
    TextMesh floatingText;
    Bhaya[] bhaya;
    int toSpawn, toRemove;
    private bool bTrigger;

    private void Start()
    {
        FindingRandomNumber();
        DisplayingText();
    }

    private void DisplayingText()
    {
        eArithmeticOperations = (ArithmeticSigns)iRandomSign;
        if (eArithmeticOperations.ToString() == ArithmeticSigns.KILLALL.ToString())
        {
            floatingText.text = eArithmeticOperations.ToString();
        }
        else
        {
            floatingText.text = eArithmeticOperations.ToString() + ": " + iRandomNumber;
        }
        // Debug.Log("Sign: " + eArithmeticOperations.ToString() + " RandomNumber: " + iRandomNumber);
    }

    private void FindingTotalNumberToSpawn()
    {
        switch (eArithmeticOperations)
        {
            case ArithmeticSigns.KILLALL:
                if (bTrigger)
                {
                    for (int i = 0; i < bhaya.Length; i++)
                    {
                        bhaya[i].Death();
                    }
                    bTrigger = false;
                }
                break;
            case ArithmeticSigns.ADD:
                if (bTrigger)
                {

                    toSpawn = (totalNumber + iRandomNumber) - totalNumber;
                    totalNumber += iRandomNumber;
                    for (int i = 0; i < toSpawn; i++)
                    {
                        vRandomLocation = new Vector3(UnityEngine.Random.Range(-6, 6), 0, 0);
                        GameObject.Instantiate(go, Spawnner.position + vRandomLocation, Quaternion.identity, Spawnner);
                    }

                    bTrigger = false;
                }
                break;
            case ArithmeticSigns.SUBTRACT:
                if (bTrigger)
                {
                    if (iRandomNumber > totalNumber)
                    {
                        totalNumber = 0;
                        for (int i = 0; i < bhaya.Length; i++)
                        {
                            bhaya[i].Death();
                        }
                    }
                    else
                    {

                        totalNumber -= iRandomNumber;

                        for (int i = 0; i < iRandomNumber; i++)
                        {
                            bhaya[i].Death();
                        }
                    }
                    bTrigger = false;
                }
                break;
            case ArithmeticSigns.DIVIDE:
                if (bTrigger)
                {
                    toRemove = Mathf.RoundToInt(totalNumber / iRandomNumber);
                    for (int i = 0; i < toRemove; i++)
                    {
                        bhaya[i].Death();
                    }
                    bTrigger = false;
                }
                break;
            case ArithmeticSigns.MULTIPLY:
                if (bTrigger)
                {
                    toSpawn = (totalNumber * iRandomNumber) - totalNumber;
                    totalNumber *= iRandomNumber;
                    if (bTrigger == true)
                    {
                        for (int i = 0; i < toSpawn; i++)
                        {
                            vRandomLocation = new Vector3(UnityEngine.Random.Range(-6, 6), 0, 0);
                            GameObject.Instantiate(go, Spawnner.position + vRandomLocation, Quaternion.identity, Spawnner);
                        }
                    }
                    bTrigger = false;
                }
                break;
        };
    }

    private void FindingRandomNumber()
    {
        iRandomSign = UnityEngine.Random.Range(0, 5);
        iRandomNumber = UnityEngine.Random.Range(1, 20);
    }

    private void Caching()
    {
        bhaya = GameObject.FindObjectsOfType<Bhaya>();
        totalNumber = GameObject.FindObjectsOfType<Bhaya>().Length;
        go = Resources.Load<GameObject>("Prefabs/NPC");

    }

    private void OnTriggerEnter(Collider other)
    {

        Caching();
        if (other.gameObject.tag == "Player")
        {
            /*Debug.Log("Triggered");
            */
            bTrigger = true;
            FindingTotalNumberToSpawn();
        }
    }



}
