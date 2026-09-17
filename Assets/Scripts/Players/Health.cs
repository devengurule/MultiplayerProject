using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Vector2 vignettePowerMinMax;
    [SerializeField] private Material vignetteMaterial;
    private int health;
    private float currentVignettePower;
    private float currentVignetteAlpha;
    private bool isInvulnerable;
    private Coroutine alphaChange;
    private Coroutine powerChange;

    private void Start()
    {
        health = GameController.instance.maxPlayerHealth;

        currentVignetteAlpha = 0;
        vignetteMaterial.SetFloat("_Alpha", currentVignetteAlpha);

        currentVignettePower = vignettePowerMinMax.y;
        vignetteMaterial.SetFloat("_Power", currentVignettePower);
    }

    private void OnDisable()
    {
        vignetteMaterial.SetFloat("_Alpha", 0);
        vignetteMaterial.SetFloat("_Power", vignettePowerMinMax.y);
    }

    public void ChangeHealth(int amount)
    {
        if (GetComponent<StunController>().isStunned) return;
        if (isInvulnerable) return;

        health += amount;
        UpdateVignette();

        isInvulnerable = true;

        if (health <= 0)
        {
            GetComponent<StunController>().HeavyStun();
        }
        else
        {
            GetComponent<StunController>().LightStun();
        }
    }

    private void UpdateVignette()
    {
        powerChange = StartCoroutine(ChangePower(GetPowerTarget(), 0.5f));

        if (health == GameController.instance.maxPlayerHealth && currentVignetteAlpha != 0)
        {
            alphaChange = StartCoroutine(ChangeAlpha(0, 1));
        }
        else if(health < GameController.instance.maxPlayerHealth && currentVignetteAlpha != 1)
        {
            alphaChange = StartCoroutine(ChangeAlpha(1, 1));
        }
    }

    private IEnumerator ChangeAlpha(float target, float changeDuration)
    {
        float counter = 0;

        while(counter < changeDuration)
        {
            counter += Time.deltaTime;

            float percentChange = (counter / changeDuration);
            float direction = Mathf.Sign(target - vignetteMaterial.GetFloat("_Alpha"));

            currentVignetteAlpha = direction < 0 ? (percentChange * direction) + 1: percentChange * direction;

            vignetteMaterial.SetFloat("_Alpha", currentVignetteAlpha);

            yield return null;
        }
        currentVignetteAlpha = target;
        vignetteMaterial.SetFloat("_Alpha", currentVignetteAlpha);
        StopCoroutine(alphaChange);
    }

    private IEnumerator ChangePower(float target, float changeDuration)
    {
        float counter = 0;

        while (counter < changeDuration)
        {
            counter += Time.deltaTime;

            float percentChange = 1 - (counter / changeDuration);

            currentVignettePower = (percentChange * (vignetteMaterial.GetFloat("_Power") - target)) + target;

            vignetteMaterial.SetFloat("_Power", currentVignettePower);

            yield return null;
        }
        currentVignettePower = target;
        vignetteMaterial.SetFloat("_Power", currentVignettePower);
        StopCoroutine(powerChange);
    }

    private float GetPowerTarget()
    {
        if(health == GameController.instance.maxPlayerHealth)
        {
            return vignettePowerMinMax.y;
        }

        float healthPercent = (float)health / (float)(GameController.instance.maxPlayerHealth - 1);

        float powerRange = vignettePowerMinMax.y - vignettePowerMinMax.x;

        float target = (healthPercent * powerRange) + vignettePowerMinMax.x;

        return target;
    }

    public void ResetHealth()
    {
        health = GameController.instance.maxPlayerHealth;
        UpdateVignette();
    }

    public void ResetVulnerability()
    {
        isInvulnerable = false;
    }
}
