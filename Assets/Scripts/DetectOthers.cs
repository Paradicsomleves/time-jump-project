using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DetectOthers : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject head;
    [SerializeField] MultiAimConstraint aim;
    Interact interact;

    [SerializeField] LayerMask layerMask;

    public bool canTurnHead;
    public bool foundTarget;
    float t;

    Vector3 lookAtTarget;
    Vector3 endPos;
    Vector3 startPos;


    [Range(1f, 10f)]
    [SerializeField] float headSpeed = 5;

    private void Start()
    {
        interact = GetComponent<Interact>();
        canTurnHead = true;
        StartCoroutine(LookingAtStuff());
        t = 0;
    }

    private void Update()
    {
        if (target.transform.position != endPos)
        {
            t += headSpeed * Time.deltaTime;
            target.transform.position = Vector3.Lerp(startPos, endPos, t);
            //Debug.Log("fut az áthelyezés" + (t));
        }
        else
        {
            t = 0;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("LookTarget"))
    //    {
    //        Debug.Log("LOOKTARGET");
    //        foundTarget = true;
    //        endPos = other.transform.position;
    //    }
    //    else if (other.CompareTag("Interact"))
    //    {
    //        foundTarget = true;
    //        endPos = other.transform.position;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("LookTarget") || other.CompareTag("Interact"))
    //    {
    //        foundTarget = false;
    //    }
    //}

    IEnumerator LookingAtStuff()
    {
        RaycastHit hit;

        Vector3 facing;

        int timesTried = 0;
        int timesTreshold = 50;

        while (true)
        {
            if (canTurnHead)
            {
                while (aim.weight != 1f)
                {
                    aim.weight += Mathf.Lerp(0f, 1f, headSpeed * Time.deltaTime);
                    Debug.Log("aim weight is not yet 1, it's actually: " + aim.weight);
                    yield return null;
                }
                facing = new Vector3(Randomize(-1f, 1f), Randomize(0.2f, 1f));
                facing = (transform.forward + facing).normalized;


                while (!Physics.Raycast(this.transform.position, facing, out hit, 50, layerMask))
                {
                    facing = new Vector3(Randomize(-1f, 1f), Randomize(0.2f, 1f));
                    facing = (transform.forward + facing).normalized;

                    Physics.Raycast(this.transform.position, facing, out hit, 50, layerMask);
                    Debug.Log("Looking for stuff...");

                    timesTried++;


                    if (timesTried > timesTreshold)
                    {
                        while (SetWeight(0f) != 0f)
                        {
                            yield return null;
                        }
                    }

                    yield return null;
                }

                timesTried = 0;

                while (SetWeight(1f) != 1f)
                {
                    yield return null;
                }

                
                Debug.Log("I see something " + hit.point + "");

                if (!interact.foundTarget)
                {
                    endPos = hit.point;
                } else
                {
                    endPos = interact.targetPos;
                }

                Debug.Log("End position: " + endPos);
            }
            else
            {
                while (aim.weight <= 0f)
                {
                    aim.weight = headSpeed * Time.deltaTime;
                    // Debug.Log("fut az áthelyezés");
                    yield return null;
                }
            }

            startPos = target.transform.position;
            yield return new WaitForSeconds(2f);
        }

        //isCoroutineRunning = false;
    }

    float Randomize(float min, float max)
    {
        float randomNum = UnityEngine.Random.Range(min, max);
        return randomNum;
    }

    float SetWeight(float weight)
    {
        if (aim.weight > weight+0.01f)
        {
            aim.weight -= headSpeed * Time.deltaTime;
        }
        else if (aim.weight < weight-0.01f)
        {
            aim.weight += headSpeed * Time.deltaTime;
        }
        else
        {
             aim.weight = weight;
        }
        return aim.weight;

    }
}
