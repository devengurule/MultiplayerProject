using System;
using System.Collections;
using UnityEngine;

public class StunController : MonoBehaviour
{
    [SerializeField] private float lightStunDuration;
    [SerializeField] private float heavyStunDuration;
    [SerializeField] private ParticleSystem heavyStunParticles;
    [SerializeField] private int heavyParticlesDelayCount;

    public bool isStunned { get; private set; }
    private Coroutine stunnedTimerCoroutine;
    public event Action OnLightStun;
    public event Action OnHeavyStun;

    public void LightStun()
    {
        if (!isStunned)
        {
            OnLightStun?.Invoke();
            GetComponent<InputHandler>().enabled = false;
            stunnedTimerCoroutine = StartCoroutine(StunTimer(lightStunDuration, false));
        }
    }

    public void HeavyStun()
    {
        if (!isStunned)
        {
            OnHeavyStun?.Invoke();
            GetComponent<InputHandler>().enabled = false;
            heavyStunParticles.Play();
            stunnedTimerCoroutine = StartCoroutine(StunTimer(heavyStunDuration, true));
        }
    }

    private void OnDisable()
    {
        if (stunnedTimerCoroutine != null)
        {
            StopCoroutine(stunnedTimerCoroutine);
        }
    }

    private IEnumerator StunTimer(float duration, bool heavy)
    {
        float counter = duration;

        isStunned = true;

        while (counter > 0)
        {
            counter -= Time.deltaTime;

            yield return null;
        }

        if (heavy)
        {
            heavyStunParticles.Stop();

            while (heavyStunParticles.particleCount > heavyParticlesDelayCount)
            {
                yield return null;
            }

            GetComponent<Health>().ResetHealth();
        }

        isStunned = false;
        GetComponent<InputHandler>().enabled = true;
        StopCoroutine(stunnedTimerCoroutine);
    }
}
