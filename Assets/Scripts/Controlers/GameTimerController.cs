using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerController : MonoBehaviour
{
    [Header("Start Time")]
    [SerializeField, Min(0)] private int minutes;
    [SerializeField, Range(0, 59)] private int seconds;

    [Header("GameObjects")]
    [SerializeField] private Image rightFill;
    [SerializeField] private Image leftFill;
    [SerializeField] private TextMeshProUGUI countdownTimer;

    [Header("Countdown Timer Extras")]
    [SerializeField] private int targetCountdownStart;
    [SerializeField] private float transitionBuffer;
    [SerializeField] private float transitionTime;
    [SerializeField] private RectTransform activePos;

    private float maxSeconds;
    private float currentSeconds;
    private Coroutine masterTimer;
    private Coroutine finalCountdown;

    private void Start()
    {
        maxSeconds = ((float)minutes * 60f) + (float)seconds;
        rightFill.fillAmount = 1f;
        leftFill.fillAmount = 1f;
        countdownTimer.text = "4:20";
        StartGameTimer();
    }

    public void StartGameTimer()
    {
        masterTimer = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        currentSeconds = maxSeconds;

        while(currentSeconds > 0)
        {
            currentSeconds -= Time.deltaTime;

            if(currentSeconds < targetCountdownStart + transitionBuffer)
            {
                if(finalCountdown == null) finalCountdown = StartCoroutine(SpawnFinalCountdown());

                int newSeconds = currentSeconds > 0 ? (int)currentSeconds + 1 : 0;

                if (newSeconds > 9)
                {
                    countdownTimer.text = "0:" + newSeconds;
                }
                else
                {
                    countdownTimer.text = "0:0" + newSeconds;
                }
            }

            float fillAmount = currentSeconds / maxSeconds;

            rightFill.fillAmount = fillAmount;
            leftFill.fillAmount = fillAmount;

            yield return null;
        }

        rightFill.fillAmount = 0f;
        leftFill.fillAmount = 0f;

        if(finalCountdown != null)  StopCoroutine(finalCountdown);
        StopCoroutine(masterTimer);
    }

    private IEnumerator SpawnFinalCountdown()
    {
        Vector3 startPos = countdownTimer.GetComponent<RectTransform>().anchoredPosition;
        float counter = 0f;

        while(counter < transitionTime)
        {
            float t = counter / transitionTime;

            countdownTimer.alpha = t;
            countdownTimer.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, activePos.anchoredPosition, t);

            counter += Time.deltaTime;

            yield return null;

        }
        if (finalCountdown != null) StopCoroutine(finalCountdown);
    }
}
