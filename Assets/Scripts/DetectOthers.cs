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

    [SerializeField] LayerMask layerMask;

    public bool canTurnHead;
    bool isCoroutineRunning;
    public bool foundTarget;
    float t;

    Vector3 lookAtTarget;
    Vector3 endPos;
    Vector3 startPos;


    [Range(1f, 10f)]
    [SerializeField] float headSpeed = 5;

    private void Start()
    {
        canTurnHead = true;
        StartCoroutine(LookingAtStuff());
        t = 0;
    }

    private void FixedUpdate()
    {
        if (target.transform.position != endPos)
        {
            t += headSpeed * Time.deltaTime;
            target.transform.position = Vector3.Lerp(startPos, endPos, t);
            Debug.Log("fut az áthelyezés" + (t));
        }
        else
        {
            t = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LookTarget"))
        {
            foundTarget = true;
            isCoroutineRunning = false;
            endPos = other.transform.position;
        }
        else if (other.CompareTag("Interact"))
        {
            foundTarget = true;
            isCoroutineRunning = false;
            endPos = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LookTarget") || other.CompareTag("Interact"))
        {
            foundTarget = false;
        }
    }

    IEnumerator LookingAtStuff()
    {
        isCoroutineRunning = true;
        RaycastHit hit;

        Vector3 facing;

        while (true)
        {
            if (canTurnHead)
            {
                while (aim.weight != 1f)
                {
                    aim.weight = Mathf.Lerp(aim.weight, 1f, headSpeed * Time.deltaTime);
                    // Debug.Log("fut az áthelyezés");
                    yield return null;
                }
                facing = new Vector3(Randomize(-1f, 1f), Randomize(0.2f, 1f));
                facing = (transform.forward + facing).normalized;


                while (!Physics.Raycast(this.transform.position, facing, out hit, 50, layerMask))
                {
                    facing = new Vector3(Randomize(-1f, 1f), Randomize(0.2f, 1f));
                    facing = (transform.forward + facing).normalized;

                    Physics.Raycast(this.transform.position, facing, out hit, 50, layerMask);
                    //Debug.Log(facing);

                    yield return null;
                }
                Debug.Log("I see something " + hit.point + "");

                if (!foundTarget)
                {
                    endPos = hit.point;
                }

                Debug.Log("End position: " + endPos);

            }
            else
            {
                while (aim.weight != 0f)
                {
                    aim.weight = Mathf.Lerp(aim.weight, 0f, headSpeed * Time.deltaTime);
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

    void DefaultFacing()
    {
        if (aim.weight > 0f)
        {
            aim.weight -= headSpeed * Time.deltaTime;
        }
    }
}
