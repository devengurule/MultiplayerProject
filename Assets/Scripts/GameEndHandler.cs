using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndHandler : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private Image p1Fill;
    [SerializeField] private Image p2Fill;
    [SerializeField] private GameObject p1Win;
    [SerializeField] private GameObject p2Win;
    [SerializeField] private GameObject[] buttons;

    [SerializeField] private float fillSpeed;
    [SerializeField] private float preFillWaitDuration;
    [SerializeField] private float postFillWaitDuration;

    private Coroutine fillCoroutine;

    private void OnEnable()
    {
        p1Fill.fillAmount = 0;
        p2Fill.fillAmount = 0;

        foreach (GameObject button in buttons) button.SetActive(false);

        GameTimerController.timerEnded += OnGameTimerEnded;
    }

    private void OnDisable()
    {
        GameTimerController.timerEnded -= OnGameTimerEnded;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }

    public void Main()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnGameTimerEnded()
    {
        endGameUI.SetActive(true);
        if(fillCoroutine == null) fillCoroutine = StartCoroutine(StartFillBar());
    }

    private IEnumerator StartFillBar()
    {
        CoinCounterController coinCounter = GameController.instance.GetComponent<CoinCounterController>();

        int totalCount = coinCounter.p1Counter + coinCounter.p2Counter;
        
        float p1FillTarget = (float)coinCounter.p1Counter / (float)totalCount;
        float p2FillTarget = (float)coinCounter.p2Counter / (float)totalCount;

        float preCounter = 0;
        float postCounter = 0;

        while (preCounter < preFillWaitDuration)
        {
            preCounter += Time.deltaTime;

            yield return null;
        }

        while (p1Fill.fillAmount < p1FillTarget || p2Fill.fillAmount < p2FillTarget)
        {
            float addAmount = fillSpeed * Time.deltaTime;

            if (p1Fill.fillAmount < p1FillTarget)
            {
                p1Fill.fillAmount += addAmount;
            }

            if (p2Fill.fillAmount < p2FillTarget)
            {
                p2Fill.fillAmount += addAmount;
            }

            yield return null;
        }

        p1Fill.fillAmount = p1FillTarget;
        p2Fill.fillAmount = p2FillTarget;

        while(postCounter < postFillWaitDuration)
        {
            postCounter += Time.deltaTime;

            yield return null;
        }

        if(coinCounter.p1Counter > coinCounter.p2Counter)
        {
            p1Win.SetActive(true);
        }
        else
        {
            p2Win.SetActive(true);
        }

        foreach (GameObject button in buttons) button.SetActive(true);

        StopCoroutine(fillCoroutine);
    }
}
