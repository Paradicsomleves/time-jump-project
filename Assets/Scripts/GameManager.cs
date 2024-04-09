using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Volume timeStepEffect;

    RenderTexture screenshot;
    public RawImage image;
    public float fade;
    float postProcessWeight;

    DestroyedChange destoryedChange;
    public static GameManager instance;

    public bool isPast;
    public float cooldownForReturn;
    [Range(0f, 1f)][Tooltip("On '0' backfading will happen gradually. On '1', the backfading will be instant after the cooldown")]
    public float rampPPTimeMultiplier;
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

    }

    void Start()
    {
        destoryedChange = FindObjectOfType<DestroyedChange>();
        isPast = false;
        image.color = new Vector4(0, 0, 0, 0);
        postProcessWeight = 0f;
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
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        
        yield return StartCoroutine(RampingOfJump(true, true, fadingSpeedIn));

        if (Jumping != null)
        {
            Jumping();
        }

        yield return StartCoroutine(RampingOfJump(false, true, fadingSpeedOut));

        if (isPast)
        {
            coroutine = StartCoroutine(ReturnAfterCooldown());
        }

        isCoroutineRunning = false;

    }

    IEnumerator StillImageFade()
    {
        float _fade = 1f;
        Camera.main.targetTexture = screenshot;
        Camera.main.Render();
        Camera.main.targetTexture = null;
        image.texture = screenshot;
        image.color = Color.white;

        while (image.color.a >= 0)
        {

            _fade -= Time.deltaTime / fade;
            image.color = new Vector4(1, 1, 1, _fade);
            yield return null;
        }
    }

    IEnumerator ReturnAfterCooldown()
    {
        Debug.Log("Started Return Sequence");
        float time = 0f;
        while ((cooldownForReturn * rampPPTimeMultiplier) >= time)
        {
            time += Time.deltaTime;
            //Debug.Log(time);
            yield return null;
        }
        yield return StartCoroutine(RampingOfJump(true, false, cooldownForReturn * (1 - rampPPTimeMultiplier)));

        if (isPast && Jumping != null)
        {
            Jumping();
            Debug.Log("Jumped from cooldown");
            StartCoroutine(RampingOfJump(false, false, stepEffectOut));
        }
    }

    IEnumerator RampingOfJump(bool isRampingUp, bool isUserControlled, float fadingSpeed)
    {
        if (isRampingUp)
        {
            while (postProcessWeight <= 1f)
            {
                if (fadingSpeed <= 0f)
                {
                    yield break;
                }
                postProcessWeight += Time.deltaTime / fadingSpeed;
                timeStepEffect.weight = postProcessWeight;
                if (!isPast && !isUserControlled)
                {
                    break;
                }

                yield return null;
            }
            StartCoroutine(StillImageFade());
        }
        else
        {

            while (postProcessWeight >= 0f)
            {
                
                postProcessWeight -= Time.deltaTime / fadingSpeed;
                timeStepEffect.weight = postProcessWeight;
                yield return null;
            }
        }
    }
}
