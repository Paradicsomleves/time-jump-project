using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Volume timeStepEffect;

    RenderTexture screenshot;
    public RawImage image;

    DestroyedChange destoryedChange;
    public static GameManager instance;

    public bool isPast;
    public float cooldownForReturn;
    WaitForSeconds cooldown;
    Coroutine coroutine;

    bool isCoroutineRunning;
    public float stepEffectIn = 0.1f;
    public float stepEffectOut = 0.1f;

    public delegate void TimeJump();
    public static event TimeJump Jumping;

    //public Quest quest;
    //public QuestGiver questGiver;
    //public bool questIsActive;
    private void Awake()
    {
        instance = this;
        screenshot = new RenderTexture(Screen.width, Screen.height, 16);
        cooldown = new WaitForSeconds(cooldownForReturn);
    }
    void Start()
    {
        destoryedChange = FindObjectOfType<DestroyedChange>();
        isPast = false;
        timeStepEffect = player.GetComponentInChildren<Volume>();
        image.color = new Vector4(0,0,0,0);
    }

    private void Update()
    {
        isPast = !destoryedChange.IsPast;

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            if (isCoroutineRunning == false)
            {
                StartCoroutine(ActivateTimeStep(stepEffectIn, stepEffectOut));
            }

        }

    }


    IEnumerator ActivateTimeStep(float fadingSpeedIn, float fadingSpeedOut)
    {
        isCoroutineRunning = true;
        float weight;
        weight = 0f;

        while (weight <= 1f)
        {
            weight += Time.deltaTime/fadingSpeedIn;
            timeStepEffect.weight = weight;
            Debug.Log(weight);
            yield return weight;
        }

        if (Jumping != null)
        {
            StartCoroutine(StillImageFade());
            Jumping();
        }

        while (weight >= 0f)
        {
            weight -= Time.deltaTime/fadingSpeedOut;
            timeStepEffect.weight = weight;
            Debug.Log(weight);
            yield return null;
        }

        isCoroutineRunning = false;
        

        if (isPast)
        {
            StartCoroutine("ReturnAfterCooldown");
        } else
        {
            StopCoroutine("ReturnAfterCooldown");
        }

        yield return null;
    }

    IEnumerator StillImageFade()
    {
        Camera.main.targetTexture = screenshot;
        Camera.main.Render();
        Camera.main.targetTexture = null;
        image.texture = screenshot;
        image.color = Color.white;
        float fade = 1;
        while (image.color.a >= 0)
        {

            fade -= Time.deltaTime;
            image.color = new Vector4(1, 1, 1, fade);
            yield return null;
        }
    }

    IEnumerator ReturnAfterCooldown()
    {
        yield return cooldown;
        StartCoroutine(ActivateTimeStep(4f, stepEffectOut));
    }
}
